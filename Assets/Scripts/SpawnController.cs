using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {
    [SerializeField] private List<EnemyWavesConfig> wavesConfigs;
    [SerializeField] private float initialWaitTime;
    [SerializeField] private float cadenceBetweenWaves;
    [SerializeField] private bool repeatAllWaves;

    private List<EnemyController> allEnemyInstances = new List<EnemyController>();

    private void Start() {
        if (repeatAllWaves) {
            StartCoroutine(EnemySpawnerEternalCoroutine());
        } else {
            StartCoroutine(EnemySpawnerCoroutine());
        }

        GameController.Instance.OnEnemyDied += OnEnemyDied;
        GameController.Instance.Player.OnBombUsed += OnBombUsed;
    }

    private void OnBombUsed() {
        DestroyAllEnemies();
    }

    private void OnDestroy() {
        if (GameController.Instance != null) {
            GameController.Instance.OnEnemyDied -= OnEnemyDied;
            GameController.Instance.Player.OnBombUsed -= OnBombUsed;
        }
    }

    private void OnEnemyDied(GameObject corpse) {
        Debug.LogFormat("SpawnController, I have detected one of my little spawns have died T_T ... name ={0}", corpse.name);
        var enemyController = corpse.GetComponent<EnemyController>();
        if (enemyController != null) {
            allEnemyInstances.Remove(enemyController);
        }
    }

    private IEnumerator EnemySpawnerEternalCoroutine() {
        while (true) {
            yield return StartCoroutine(EnemySpawnerCoroutine());
        }
    }

    private IEnumerator EnemySpawnerCoroutine() {
        yield return new WaitForSeconds(initialWaitTime);
        foreach (var wave in wavesConfigs) {
            foreach (var enemy in wave.enemies) {
                Vector3 enemyPosition = Vector3.zero;
                if (enemy.useSpecificXPosition) {
                    enemyPosition = enemy.spawnReferencePosition;
                } else {
                    enemyPosition = new Vector3(Random.Range(-enemy.spawnReferencePosition.x, enemy.spawnReferencePosition.x), enemy.spawnReferencePosition.y, enemy.spawnReferencePosition.z);
                }
                SpawnEnemy(enemy.enemyPrefab, enemy.config, enemyPosition, enemy.rotation);
                yield return new WaitForSeconds(wave.cadence);
            }
            yield return new WaitForSeconds(cadenceBetweenWaves);
        }
    }

    public void SpawnEnemy(EnemyController enemyPrefab, EnemyConfig config, Vector3 enemyPosition, Quaternion rotation) {
        var enemyInstance = Instantiate(enemyPrefab, enemyPosition, rotation);
        enemyInstance.config = config;

        allEnemyInstances.Add(enemyInstance);
    }

    public void DestroyAllEnemies() {
        for (int i = 0; i < allEnemyInstances.Count; i++) {
            var enemy = allEnemyInstances[i];
            if (enemy == null) {
                continue;
            }
            enemy.DestroyAllShootedShoots();
            var explosion = enemy.GetComponent<Instantiator>();
            if (explosion != null) {
                explosion.DoInstantiate();
            }
            var explosions = enemy.GetComponentInChildren<MultipleInstantiator>();
            if (explosions != null) {
                explosions.InstantiateInSequence();
            }
            Destroy(enemy.gameObject);
        }
        allEnemyInstances.Clear();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public static GameController Instance;

    public delegate void EnemyDied(GameObject corpse);
    public event EnemyDied OnEnemyDied;
    public delegate void ScoreChanged(int updatedScore);
    public event ScoreChanged OnScoreChanged;
    public delegate void PickupPicked(PickupController pickup);
    public event PickupPicked OnPickupPicked;

    [SerializeField] private PlayerController player;
    [SerializeField] private PopupController popupController;
    [HideInInspector]
    public PlayerController Player {
        get {
            return player;
        }
    }

    private int _playerScore;
    public int PlayerScore {
        get {
            return _playerScore;
        }
        private set {
            _playerScore = value;
            if (OnScoreChanged != null) {
                OnScoreChanged.Invoke(_playerScore);
            }
        }
    }

    public bool IsGamePaused = false;

    private void Awake() {
        Instance = this;
    }

    public void OnDie(GameObject deadObject, int score = 0) {
        PlayerScore += score;
        // TODO: Enemies should manage its received shoots through health controller.
        StatisticsManager.SetStatisticValue(StatisticId.PlayerSuccessfulShoots, 1, true);

        player.AddToPowerLevel(1);
        StatisticsManager.SetStatisticValue(StatisticId.PlayerKills, 1, true);

        if (OnEnemyDied != null) {
            OnEnemyDied.Invoke(deadObject);
        }

        var enemyController = deadObject.GetComponent<EnemyController>();
        if (enemyController != null && enemyController.IsBoss) {
            popupController.OpenPopup("WinPopup");
        }
    }

    public void OnPickupPickedUp(PickupController pickup) {
        PlayerScore += pickup.config.score;
        player.UnlockSpecial(pickup.config);
        OnPickupPicked?.Invoke(pickup);
    }

    public void OnPlayerDie() {
        popupController.OpenPopup("GameOverPopup");
    }

    public void OpenPopup(string popupName) {
        if (popupController == null) {
            return;
        }
        popupController.OpenPopup(popupName);
    }
    public void CloseLastPopup() {
        if (popupController == null) {
            return;
        }
        popupController.CloseCurrentPopup();
    }
}

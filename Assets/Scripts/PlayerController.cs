using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary {
    public float xMinimum, xMaximum, yMinimum, yMaximum;
}

public class PlayerController : MonoBehaviour {
    public Mover moverComponent;
    public float speed;
    public Boundary boundary;
    [SerializeField] private List<Shooter> shooters;
    [SerializeField] private PlayerConfig config;
    [SerializeField] private SpecialsController specialsController;
    [SerializeField] private Collider2D playerCollider;
    [SerializeField] private OnTriggerEnterDo triggerer;

    public delegate void PowerChanged(int currentPower, int totalPower);
    public event PowerChanged OnPowerChanged;

    public delegate void BombUsed();
    public event BombUsed OnBombUsed;

    private int powerLevel;
    private int unlockedCannons = 1;

    public int AmountOfBombs = 0;

    private void Start() {
        moverComponent.speed = speed;
        InputProvider.OnHasShoot += OnHasShoot;
        InputProvider.OnDirection += OnDirection;
        InputProvider.OnBombPressed += OnBombPressed;
    }

    private void OnBombPressed() {
        if (AmountOfBombs <= 0) {
            return;
        }

        AmountOfBombs--;
        OnBombUsed?.Invoke();
    }

    private void OnDirection(Vector3 direction) {
        moverComponent.direction = direction;
    }

    private void OnHasShoot() {
        for (int i = 0; i < unlockedCannons; i++) {
            var shooter = shooters[i];
            shooter.DoShoot();
            StatisticsManager.SetStatisticValue(StatisticId.PlayerShoots, 1, true);
        }
    }

    public void AddToPowerLevel(int powerToAdd) {
        powerLevel += powerToAdd;
        var powerConfig = config.GetPowerConfig(powerLevel);
        unlockedCannons = powerConfig.cannonAmount;

        if (OnPowerChanged != null) {
            OnPowerChanged.Invoke(powerLevel, config.GetMaxPowerValue());
        }
    }

    public void OnPlayerDie() {
        GameController.Instance.OnPlayerDie();
    }

    public void UnlockSpecial(PickupConfig pickupConfig) {
        Debug.LogFormat("UnlockSpecial pickup type {0}", pickupConfig.type);
        specialsController.UnlockSpecial(pickupConfig);
        if (pickupConfig.type == PickupType.Shield) {
            EnableCollider(false);
        }
        if (pickupConfig.type == PickupType.Bomb) {
            AmountOfBombs++;
        }
    }

    public void EnableCollider(bool shouldEnable) {
        playerCollider.enabled = shouldEnable;
        triggerer.IsEnabled = shouldEnable;
    }

    public int GetCurrentPowerLevel() {
        return powerLevel;
    }
    public int GetMaxPowerLevel() {
        return config.GetMaxPowerValue();
    }

    void Update() {
        float x = Mathf.Clamp(transform.position.x, boundary.xMinimum, boundary.xMaximum);
        float y = Mathf.Clamp(transform.position.y, boundary.yMinimum, boundary.yMaximum);
        transform.position = new Vector3(x, y);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerController : MonoBehaviour {
    [SerializeField] private Image fillImage;
    [SerializeField] private Text powerLevel;

    private void Start() {
        UpdateFillImagePercentage(GameController.Instance.Player.GetCurrentPowerLevel(), GameController.Instance.Player.GetMaxPowerLevel());
        GameController.Instance.Player.OnPowerChanged += OnPowerChanged;
    }

    private void OnPowerChanged(int currentPower, int totalPower) {
        UpdateFillImagePercentage(currentPower, totalPower);
    }

    public void UpdateFillImagePercentage(int current, int total) {
        if (powerLevel != null) {
            powerLevel.text = string.Format("{0}/{1}", current, total);
        }
        fillImage.fillAmount = (float)current / (float)total;
    }

    private void OnDestroy() {
        if (GameController.Instance != null && GameController.Instance.Player != null) {
            GameController.Instance.Player.OnPowerChanged -= OnPowerChanged;
        }
    }
}

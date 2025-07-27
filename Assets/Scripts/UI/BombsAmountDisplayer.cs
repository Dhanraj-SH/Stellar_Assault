using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombsAmountDisplayer : MonoBehaviour {
    [SerializeField] private string displayFormat = "x {0}";

    private Text _amountText;
    private Text amountText {
        get {
            if (_amountText == null) {
                _amountText = GetComponent<Text>();
            }
            return _amountText;
        }
    }

    private void Start() {
        DisplayAmountOfBombs();
        GameController.Instance.OnPickupPicked += OnPickupPicked;
        GameController.Instance.Player.OnBombUsed += Player_OnBombUsed;
    }

    private void Player_OnBombUsed() {
        DisplayAmountOfBombs();
    }

    private void OnPickupPicked(PickupController pickup) {
        if (pickup.config.type != PickupType.Bomb) {
            return;
        }
        DisplayAmountOfBombs();
    }

    private void DisplayAmountOfBombs() {
        amountText.text = string.Format(displayFormat, GameController.Instance.Player.AmountOfBombs);
    }
}

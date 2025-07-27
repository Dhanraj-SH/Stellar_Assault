using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAccuracyDisplayer : MonoBehaviour
{
    [SerializeField] private Text valueText;

    private void OnEnable() {
        SetAccuracyInText();
    }

    public void SetAccuracyInText() {
        float successfulShoots = StatisticsManager.GetStatisticValue(StatisticId.PlayerSuccessfulShoots);
        float totalShoots = StatisticsManager.GetStatisticValue(StatisticId.PlayerShoots);
        float accuracy = totalShoots <= 0 ? 0 : (successfulShoots / totalShoots);
        valueText.text = string.Format("{0:P1}", accuracy);
    }
}

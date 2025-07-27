using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatisticValueDisplayer : MonoBehaviour
{
    [SerializeField] private Text valueText;
    [SerializeField] private StatisticId statisticId;

    private void OnEnable() {
        SetStatisticValueInText();
    }

    public void SetStatisticValueInText() {
        valueText.text = StatisticsManager.GetStatisticValue(statisticId).ToString();
    }
}

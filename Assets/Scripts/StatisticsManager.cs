using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatisticId {
    Undefined,
    PlayerShoots,
    EnemyShoots,
    PlayerSuccessfulShoots,
    PlayerKills
}

public static class StatisticsManager {
    private static Dictionary<StatisticId, int> statisticsValues = new Dictionary<StatisticId, int>();

    public static int GetStatisticValue(StatisticId id) {
        if (!statisticsValues.ContainsKey(id)) {
            statisticsValues.Add(id, 0);
        }
        return statisticsValues[id];
    }

    /// <summary>
    /// Sets or add a specified value to the indicated statistic.
    /// </summary>
    /// <param name="id">The statistic to set or add.</param>
    /// <param name="value">The value to set or add.</param>
    /// <param name="accum">True to add, false to set (overwrite)</param>
    public static void SetStatisticValue(StatisticId id, int value, bool accum = false) {
        if (!statisticsValues.ContainsKey(id)) {
            statisticsValues.Add(id, value);
        } else {
            if (accum) {
                statisticsValues[id] += value;
            } else {
                statisticsValues[id] = value;
            }
        }
    }
}

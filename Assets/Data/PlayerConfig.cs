using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Player Config", menuName = "Player/Player Config", order = 0)]
public class PlayerConfig : ScriptableObject {
    [Serializable]
    public class PowerConfig {
        public int powerValue;
        public int cannonAmount;
    }

    public List<PowerConfig> powerConfigs;

    public PowerConfig GetPowerConfig(int powerValue) {
        foreach (var config in powerConfigs) {
            if (config.powerValue >= powerValue) {
                return config;
            }
        }
        return powerConfigs[powerConfigs.Count - 1];
    }

    public int GetMaxPowerValue() {
        var config = powerConfigs[powerConfigs.Count - 1];
        return config.powerValue;
    }
}

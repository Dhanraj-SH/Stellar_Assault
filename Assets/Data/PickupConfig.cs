using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickupType {
    None,
    Laser,
    Shield,
    Coin,
    Bomb
}

[CreateAssetMenu(fileName = "New PickupConfig", menuName = "Player/Pickups", order = 1)]
public class PickupConfig : ScriptableObject {
    public PickupType type;
    public int score;
    public float durationInSeconds;
}

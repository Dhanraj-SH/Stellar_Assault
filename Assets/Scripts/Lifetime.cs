using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifetime : MonoBehaviour
{
    public float lifetimeTime;

    private void Start() {
        Destroy(gameObject, lifetimeTime);
    }
}

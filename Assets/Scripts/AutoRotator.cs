using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotator : MonoBehaviour {
    public Vector3 rotationAxis;
    public float rotationSpeed;

    void Update() {
        transform.Rotate(rotationAxis * (rotationSpeed * Time.deltaTime));
    }
}

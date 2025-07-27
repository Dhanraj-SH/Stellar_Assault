using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoLookAt : MonoBehaviour {
    private Transform target;

    private void Start() {
        if (GameController.Instance == null || GameController.Instance.Player == null) {
            return;
        }
        target = GameController.Instance.Player.transform;
    }

    private void Update() {
        if (target == null) {
            return;
        }
        // Follow the player or any target (LookAt2D)
        var targetPosition = target.position;

        Vector3 direction = targetPosition - transform.position;
        direction.Normalize();

        float rot_z = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }
}

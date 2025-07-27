using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpecialsController : MonoBehaviour {
    [SerializeField] private GameObject laser;
    [SerializeField] private GameObject shield;
    [SerializeField] private UnityEvent actionsOnFinish;

    private Coroutine laserCoroutine;
    private Coroutine shieldCoroutine;

    public void UnlockSpecial(PickupConfig config) {
        Debug.LogFormat("SpecialsController: UnlockSpecial pickup type {0}", config.type);
        switch (config.type) {
            case PickupType.Laser:
                if (laserCoroutine != null) {
                    StopCoroutine(laserCoroutine);
                }
                laser.SetActive(true);
                laserCoroutine = StartCoroutine(DisableAfterSeconds(laser, config.durationInSeconds));
                break;
            case PickupType.Shield:
                if (shieldCoroutine != null) {
                    StopCoroutine(shieldCoroutine);
                }
                shield.SetActive(true);
                shieldCoroutine = StartCoroutine(DisableAfterSeconds(shield, config.durationInSeconds, () => {
                    if (actionsOnFinish != null) {
                        actionsOnFinish.Invoke();
                    }
                }));
                break;
            default:
                break;
        }
    }

    private IEnumerator DisableAfterSeconds(GameObject objectToDisable, float time, System.Action onFinish = null) {
        yield return new WaitForSeconds(time);
        objectToDisable.SetActive(false);

        if (onFinish != null) {
            onFinish.Invoke();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleInstantiator : MonoBehaviour
{
    [SerializeField] private List<Instantiator> instantiators;
    [SerializeField] private float delayBetweenInstantiators;
    [SerializeField] private bool randomizeOffset;
    [SerializeField] private const float positionOffset = 2f;

    public int InstantiatorsCount {
        get {
            return instantiators.Count;
        }
    }

    public void InstantiateInSequence() {
        StartCoroutine(InstantiatorSequence());
    }

    public void InstantiateByIndex(int index) {
        if (index < 0 || index >= instantiators.Count) {
            Debug.LogErrorFormat("Can't instantiate with index {0}. Indicate an index between 0 and {1}", index, instantiators.Count);
            return;
        }
        var instantiator = instantiators[index];
        if (randomizeOffset) {
            float xRand = Random.Range(-positionOffset, positionOffset);
            float yRand = Random.Range(-positionOffset, positionOffset);
            Vector3 randomOffset = new Vector3(transform.position.x + xRand, transform.position.y + yRand, transform.position.z);
            instantiator.DoInstantiate(randomOffset);
        } else {
            instantiator.DoInstantiate();
        }
    }

    private IEnumerator InstantiatorSequence() {
        foreach (var instantiator in instantiators) {
            instantiator.DoInstantiate();
            yield return new WaitForSeconds(delayBetweenInstantiators);
        }
    }
}

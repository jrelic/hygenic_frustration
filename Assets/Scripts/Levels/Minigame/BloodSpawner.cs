using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BloodSpawner : MonoBehaviour
{
    public GameObject[] hands;

    private GameObject currentActiveHand;

    private void OnEnable()
    {
        currentActiveHand = null;
        StartCoroutine(IterateHands());
    }

    private IEnumerator IterateHands()
    {
        while(true)
        {
            if(currentActiveHand != null)
            {
                currentActiveHand.SetActive(false);
            }

            var nextHand = hands[Random.Range(0, hands.Length)];
            nextHand.SetActive(true);
            currentActiveHand = nextHand;

            yield return new WaitForSeconds(0.5f);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UrineSpawner : MonoBehaviour
{
    public GameObject urinePrefab;

    public Vector2 SpawnRange;

    private Transform[] spawnPoints { get; set; }

    private List<GameObject> spawnedUrine { get; set; }

    private void Awake()
    {
        spawnPoints = GetComponentsInChildren<Transform>();
        spawnedUrine = new List<GameObject>();
    }

    private void OnEnable()
    {
        StartCoroutine(SpawnUrine());
    }

    private IEnumerator SpawnUrine()
    {
        while(true)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(SpawnRange.x, SpawnRange.y));
            var spawnPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
            spawnedUrine.Add(Instantiate(urinePrefab, spawnPoint.transform.position, Quaternion.identity, transform.parent));
        }
    }

    private void OnDisable()
    {
        for (int i = spawnedUrine.Count-1; i >= 0; i--)
        {
            if (spawnedUrine[i] != null)
            {
                Destroy(spawnedUrine[i].gameObject);
            }
        }
    }
}

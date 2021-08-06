using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsController : MonoBehaviour
{
    public GameObject[] powerUpsPrefabs;

    private void Start()
    {
        int numRandom = Random.Range(0, powerUpsPrefabs.Length);
        Instantiate(powerUpsPrefabs[numRandom], transform.position, transform.rotation);
    }
}

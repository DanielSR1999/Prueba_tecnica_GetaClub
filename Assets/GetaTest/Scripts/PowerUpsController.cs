using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsController : MonoBehaviour
{
    public GameObject[] powerUpsPrefabs;

    private void Start()
    {
        RandomInstantiate();
    }
    public void RandomInstantiate()
    {
        int numRandom = Random.Range(0, powerUpsPrefabs.Length);
        GameObject newPowerUp = Instantiate(powerUpsPrefabs[numRandom], transform.position, transform.rotation);
        newPowerUp.transform.SetParent(gameObject.transform);    
    }
}

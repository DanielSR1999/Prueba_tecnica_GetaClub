using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateObstacles : MonoBehaviour
{
    public GameObject[] _itemsPrefabs;
    public Transform[] spawners;
    public bool instantiate = false;

    private void Start()
    {
        if (instantiate)
            RandomInstantiate();
        else
            return;
    }
    public void RandomInstantiate()
    {
        if (instantiate)
        {
            Outline[] prefabsChildren = GetComponentsInChildren<Outline>();

            if (prefabsChildren != null)
            {
                foreach (Outline prefabs in prefabsChildren)
                {
                    Destroy(prefabs.gameObject);
                }
            }

            if (_itemsPrefabs != null)
            {
                for (int i = 0; i < spawners.Length; i++)
                {
                    int randomNum = Random.Range(0, _itemsPrefabs.Length);
                    GameObject newObject = Instantiate(_itemsPrefabs[randomNum], spawners[i].position, Quaternion.identity);
                    newObject.transform.SetParent(gameObject.transform);
                }


            }
        }   
    }
}

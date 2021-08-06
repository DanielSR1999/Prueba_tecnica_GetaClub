using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(KartController))]
public class CollisionController : MonoBehaviour
{
    public string clockTag = "Clock";
    public string rayTag = "Ray";
    [SerializeField]
    Timer timer;
    [SerializeField]
    int extraTime;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(rayTag))
        {
            GetComponent<KartController>().EnableTurbo();
            other.gameObject.SetActive(false);
        }
        else if(other.gameObject.CompareTag(clockTag))
        {
            timer.AddTime(extraTime);
            other.gameObject.SetActive(false);
        }
    }
}

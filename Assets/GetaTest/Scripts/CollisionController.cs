using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(KartController))]
public class CollisionController : MonoBehaviour
{
    public string clockTag = "Clock";
    public string rayTag = "Ray";
    public string oilTag = "Oil";
    public string jumpTag = "Jump";
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
        else if (other.gameObject.CompareTag(oilTag))
        {
            GetComponent<KartController>().NoKarControl();
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        else if (other.gameObject.CompareTag(jumpTag))
        {
            GetComponent<KartController>().canJump = true;
            other.gameObject.SetActive(false);
        }
    }
}

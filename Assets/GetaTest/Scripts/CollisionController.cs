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
    public string floorTag = "Floor";
    public string trackTag = "Track";
    public string environmentTag = "Environment";
    [SerializeField] Timer timer;
    [SerializeField] int extraTime;
    KartController kartController;
    [SerializeField] Spawner spawner;
    private void Start()
    {
        kartController = GetComponent<KartController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(rayTag))
        {
            kartController.EnableTurbo();
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag(clockTag))
        {
            timer.AddTime(extraTime);
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag(oilTag))
        {
            kartController.NoKarControl();
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        else if (other.gameObject.CompareTag(jumpTag))
        {
            kartController.canJump = true;
            other.gameObject.SetActive(false);
        }
        switch (kartController.mode)
        {
            case KartController.gameMode.trails:
                {
                    if (other.gameObject.CompareTag(floorTag))
                    {
                        kartController.canJump = true;
                    }
                    else if (other.gameObject.CompareTag(trackTag))
                    {
                        other.GetComponent<BoxCollider>().enabled = false;
                        spawner.RecicleTrack(other.gameObject,other.GetComponent<BoxCollider>(),true);
                    }
                    else if (other.gameObject.CompareTag(environmentTag))
                    {
                        other.GetComponent<BoxCollider>().enabled = false;
                        GameObject objectToMove = other.gameObject.transform.parent.gameObject;

                        spawner.RecicleTrack(objectToMove, other.GetComponent<BoxCollider>(),false);
                    }
                    break;
                }
        }
        

    }
}

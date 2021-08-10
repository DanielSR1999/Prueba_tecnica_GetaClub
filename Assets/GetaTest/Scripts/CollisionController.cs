using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public string obstacleTag = "Obstacle";
    [SerializeField] Timer timer;
    [SerializeField] int extraTime;
    KartController kartController;
    [SerializeField] Spawner spawner;
    [SerializeField] Text scoreText;
    [SerializeField] int scoreValue;
    [SerializeField] int valueToAddScore;
    private void Start()
    {
        kartController = GetComponent<KartController>();
        if(scoreText!=null)
        {
            scoreText.text = "Puntuación " + scoreValue.ToString();
            kartController.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(obstacleTag))
        {
            GetComponent<KartController>().speed = 0;
            GetComponent<KartController>().StopAllCoroutines();
            
            GetComponent<Rigidbody>().velocity = Vector3.zero;

            timer.StopTimer();
            
        }
    }
    private void OnTriggerEnter(Collider other)
    { 
        switch (kartController.mode)
        {
            case KartController.gameMode.trails:
                {
                    if (other.gameObject.CompareTag(floorTag))
                    {
                        kartController.canJump = true;
                        kartController.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
                    }
                    else if (other.gameObject.CompareTag(trackTag))
                    {
                        other.GetComponent<BoxCollider>().enabled = false;
                        other.GetComponent<InstantiateObstacles>().RandomInstantiate();
                        spawner.RecicleTrack(other.gameObject,other.GetComponent<BoxCollider>(),true);
                    }
                    else if (other.gameObject.CompareTag(environmentTag))
                    {
                        other.GetComponent<BoxCollider>().enabled = false;
                        GameObject objectToMove = other.gameObject.transform.parent.gameObject;

                        spawner.RecicleTrack(objectToMove, other.GetComponent<BoxCollider>(),false);
                    }
                    else if (other.gameObject.CompareTag(jumpTag))
                    {
                        scoreValue += valueToAddScore;
                        scoreText.text = "Puntuación: " + scoreValue.ToString();
                        Destroy(other.gameObject);
                    }
                    
                    break;
                }
            case KartController.gameMode.racing:
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
                    break;
                }
        }
        

    }
}

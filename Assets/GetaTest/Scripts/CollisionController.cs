using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(KartController))]
public class CollisionController : MonoBehaviour
{
    [Header("Tags")]
    public string clockTag = "Clock";
    public string rayTag = "Ray";
    public string oilTag = "Oil";
    public string jumpTag = "Jump";
    public string floorTag = "Floor";
    public string trackTag = "Track";
    public string environmentTag = "Environment";
    public string obstacleTag = "Obstacle";

    [Header("Time")]
    [SerializeField] Timer timer;
    [SerializeField] int extraTime;
     
    KartController kartController;
    [Header("RailsMode")]
    [SerializeField] Spawner spawner;
    [SerializeField] Text scoreText;
    [SerializeField] int scoreValue;
    [SerializeField] int valueToAddScore;
    [SerializeField] Canvas results;
    [SerializeField] Text resultsUI;
    [SerializeField] Text recordText;
    [SerializeField] string newRecordMessage = "¡Felicidades! Has alcanzado un nuevo record";
    [SerializeField] string noRecordMessage = "¡Lo siento! No has podido superar tu record";
    public static string recordRailsModeID = "recordRails";
    [SerializeField] SoundsController soundsController;
    bool gameFinished = false;
    bool coinCollision = false;

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
            if(!gameFinished)
            {
                gameFinished = true;
                GetComponent<KartController>().speed = 0;
                GetComponent<KartController>().StopMovement();
                GetComponent<KartController>().enabled = false;

                GetComponent<Rigidbody>().velocity = Vector3.zero;
                results.enabled = true;
                soundsController.backgroundMusic.pitch = soundsController.neutralPitch;

                int currentRecord = PlayerPrefs.GetInt(recordRailsModeID, 0);

                if (scoreValue > currentRecord)
                {
                    resultsUI.text = newRecordMessage;
                    recordText.text = "Nuevo record: " + scoreValue.ToString();
                    soundsController.PlayWin();
                    Debug.Log("Nuevo record");
                    PlayerPrefs.SetInt(recordRailsModeID, scoreValue);
                    enabled = false;
                }
                else if (scoreValue <= currentRecord)
                {
                    resultsUI.text = noRecordMessage;
                    recordText.text = "Record: " +PlayerPrefs.GetInt(recordRailsModeID,0);
                    soundsController.PlayLoss();
                    Debug.Log("perdi");
                    enabled = false;
                }
            }      
        }   
    }
    private void OnTriggerExit(Collider other)
    {
        switch (kartController.mode)
        {
            case KartController.gameMode.trails:
                {
                    if (other.gameObject.CompareTag(jumpTag))
                    {
                        coinCollision = false;
                    }
                    break;
                }
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
                        if(!coinCollision)
                        {
                            coinCollision = true;
                            Destroy(other.gameObject);
                            scoreValue += valueToAddScore;
                            scoreText.text = "Puntuación: " + scoreValue.ToString();
                        }
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

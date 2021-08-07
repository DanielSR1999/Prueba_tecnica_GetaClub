using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpointsControllers : MonoBehaviour
{
    public List<GameObject> checkpoints;
    [SerializeField]
    string checkpointTag;
    [SerializeField]
    string finishColliderTag;
    [SerializeField]
    int currentCheckpoint = 0;
    private void Start()
    {
        foreach(GameObject _checkpoints in checkpoints)
        {
            _checkpoints.GetComponent<BoxCollider>().enabled = false;
        }
        checkpoints[0].GetComponent<BoxCollider>().enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag==checkpointTag)
        {
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
            currentCheckpoint++;

            if(currentCheckpoint<checkpoints.Count)
            {
                checkpoints[currentCheckpoint].GetComponent<BoxCollider>().enabled = true;
            }  
        }
        else if(other.gameObject.tag==finishColliderTag)
        {
            if(currentCheckpoint>=checkpoints.Count)
            {
                Timer timerController = GameObject.Find("TimerController").GetComponent<Timer>();
                if(timerController.secondsRemaining>0)
                {
                    Debug.Log("Win");
                    timerController.Win();
                    other.gameObject.GetComponent<BoxCollider>().enabled = false;
                }
            }
        }
    }
  
}

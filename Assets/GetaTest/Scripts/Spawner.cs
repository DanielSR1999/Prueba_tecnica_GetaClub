using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject[] trackPieces;
    public float lastTrackPieceZPosition;
    public float lastEnvironmentZPosition;
    public float spaceIntoEnvironments = 400f;
    [SerializeField] float trackSpace;
    private void Start()
    {
        lastTrackPieceZPosition = trackPieces[trackPieces.Length-1].transform.position.z;
        lastEnvironmentZPosition = 400;
    }

    public void RecicleTrack(GameObject trackToMove,BoxCollider collider,bool track)
    {
        StartCoroutine(enableCollider(trackToMove, collider, track));
    }
    IEnumerator enableCollider(GameObject objectToMove, BoxCollider collider, bool track)
    {
        if(track)
        {
            lastTrackPieceZPosition += trackSpace;
            objectToMove.transform.position = Vector3.forward * lastTrackPieceZPosition;
        }
        else
        {
            lastEnvironmentZPosition += spaceIntoEnvironments;
            objectToMove.transform.position = Vector3.forward * lastEnvironmentZPosition;
        }
        yield return new WaitForSeconds(0.25f);
        collider.enabled = true;
        
    }
    
}

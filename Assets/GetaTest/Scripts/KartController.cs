using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class KartController : MonoBehaviour
{
    public float speed;
    public float turboIncrement;
    public enum currentObstacle { none,obstacle}
    public currentObstacle _currentObstacle;

    private void Update()
    {
        float movement = Input.GetAxis("Vertical");
        float direction = Input.GetAxis("Horizontal");

    }
}

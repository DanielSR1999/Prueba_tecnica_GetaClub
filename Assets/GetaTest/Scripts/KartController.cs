using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public enum Axel
{
    front, rear
}

[Serializable]
public struct wheel
{
    public GameObject model;
    public WheelCollider wheelCollider;
    public Axel axel;
}

[RequireComponent(typeof(Rigidbody))]
public class KartController : MonoBehaviour
{
    public float yInput, xInput;
    public List<wheel> wheels;
    Rigidbody _rigidbody;
    public Vector3 centerOfMassTransform;

    [Header("KarMovement")]
    public float maxAceleration = 20.0f;
    public float turnSensitivity = 1.0f;
    public float maxSteerAngle = 45.0f;
    public float desAceleration = 80f;
    
    [Header("Speed")]
    Vector3 initialArrowRotation;
    public float speed;
    public RawImage speedmeterArrowImage;
    public float rotationArrowMultiplier;
    public float smoothArrow;

    [Header("Turbo")]
    public float turboDuration;
    public bool turboEnable = false;
    [Range(1, 2.5f)]
    public float speedIncrement;
    float velocityMultiplier = 1f;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.centerOfMass = centerOfMassTransform;
        initialArrowRotation = speedmeterArrowImage.rectTransform.rotation.eulerAngles;
        StartCoroutine(ShowSpeed());
    }

    private void Update()
    {
        GetInputs();
        if (Input.GetMouseButtonDown(0)) 
        {
            StartCoroutine(SetTurbo());
        }
    }
    void GetInputs()
    {
        yInput = Input.GetAxis("Vertical");
        xInput = Input.GetAxis("Horizontal");
    }
    private void LateUpdate()
    {
        Move();
        Turn();
        AnimateWheels();
    }
    

    IEnumerator SetTurbo()
    {
        turboEnable = true;
        velocityMultiplier = speedIncrement;
        yield return new WaitForSeconds(turboDuration);
        velocityMultiplier = 1;
        turboEnable = false;
    }
    IEnumerator ShowSpeed()
    {
        yield return new WaitForSeconds(smoothArrow);
        float promedioRPM = 0;

        for(int i=0;i<wheels.Count;i++)
        {
            promedioRPM += wheels[i].wheelCollider.rpm;
        }
        promedioRPM = promedioRPM / wheels.Count;

        speed = Mathf.Abs((2 * Mathf.PI * wheels[0].wheelCollider.radius) * promedioRPM * 60 / 1000);
        speedmeterArrowImage.rectTransform.rotation = Quaternion.Euler(initialArrowRotation.x, initialArrowRotation.y, initialArrowRotation.z + (speed * rotationArrowMultiplier));
        StartCoroutine(ShowSpeed());
    }
    private void Move()
    {
        foreach(wheel _wheels in wheels)
        {
            _wheels.wheelCollider.motorTorque = yInput * maxAceleration *velocityMultiplier * 500 * Time.deltaTime;
        }
        if(Input.GetAxis("Vertical")==0)
        {
            foreach (wheel _wheels in wheels)
            {
                if (_wheels.axel == Axel.rear)
                {
                    _wheels.wheelCollider.brakeTorque = desAceleration;
                }
            }
        }
        else
        {
            foreach (wheel _wheels in wheels)
            {
                if (_wheels.axel == Axel.rear)
                {
                    _wheels.wheelCollider.brakeTorque = 0;
                }
            }
        }
    }
    private void Turn()
    {
        foreach (wheel _wheels in wheels)
        {
            if(_wheels.axel==Axel.front)
            {
                float _steerAngle = xInput * turnSensitivity * maxSteerAngle;
                _wheels.wheelCollider.steerAngle = Mathf.Lerp(_wheels.wheelCollider.steerAngle, _steerAngle, 0.5f);
            }       
        }
    }

    void AnimateWheels()
    {
        foreach(wheel _wheels in wheels)
        {
            Quaternion _rotation;
            Vector3 _position;
            _wheels.wheelCollider.GetWorldPose(out _position, out _rotation); //si giramos las llantas rotan lateralmente
            _wheels.model.transform.position = _position;
            _wheels.model.transform.rotation = _rotation;
        }
        for(int i=0;i<wheels.Count;i++)
        {
            wheels[i].model.transform.Rotate(new Vector3(wheels[i].wheelCollider.rpm / 60 * 360 * Time.deltaTime, 0, 0)); //rotamos las llantas verticalmente
        }
    }
}

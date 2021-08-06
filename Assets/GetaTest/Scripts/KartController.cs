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
    public float speed;
    public float maxSpeed;
    public Texture speedmeterArrowImage;
    public float smoothArrow;

    [Header("GUI Speedmeter Parameters")]
    public float pivotXPosition;
    public float pivotYPosition;
    public float speedMeterGUIPositionX;
    public float speedMeterGUIPositionY;
    public float speedMeterGUIScale;

    [Header("Turbo")]
    [SerializeField]
    float turboDuration;
    [SerializeField]
    bool turboEnable = false;
    [Range(1, 5f)]
    [SerializeField]
    float speedIncrement;
    float velocityMultiplier = 1f;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.centerOfMass = centerOfMassTransform;
        StartCoroutine(ShowSpeed());
    }

    private void Update()
    {
        GetInputs();
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
    
    public void EnableTurbo()
    {
        StartCoroutine(SetTurbo());
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
        
        float _speed =  Mathf.Abs((2 * Mathf.PI * wheels[0].wheelCollider.radius) * wheels[0].wheelCollider.rpm * 60 / 1000);
        speed = Mathf.Clamp(_speed, 0, maxSpeed);
        StartCoroutine(ShowSpeed());
    }
    private void OnGUI()
    {
        float speedMeterAngle = speed * 180 / maxSpeed;
        float speedmeterAngleCampled = Mathf.Clamp(speedMeterAngle, 0, 180);
        GUIUtility.RotateAroundPivot(speedmeterAngleCampled, new Vector2(Screen.width - pivotXPosition, Screen.height - pivotYPosition));
        GUI.DrawTexture(new Rect(Screen.width - speedMeterGUIPositionX, Screen.height - speedMeterGUIPositionY, speedMeterGUIScale, speedMeterGUIScale), speedmeterArrowImage);
    }
    private void Move()
    {
        foreach(wheel _wheels in wheels)
        {
            if(speed<maxSpeed)
            {
                _wheels.wheelCollider.motorTorque = yInput * maxAceleration * velocityMultiplier * 500 * Time.deltaTime;
            }
            else
            {
                _wheels.wheelCollider.motorTorque = 0;
            }

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

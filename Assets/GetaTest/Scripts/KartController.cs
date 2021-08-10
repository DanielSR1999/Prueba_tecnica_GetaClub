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
    public enum gameMode { racing, trails}
    public gameMode mode;
    [SerializeField] Timer timer;

    [Header("Rails Controller")]
    public bool keyHit = false;
    public float currentLerpTime = 0;
    public float lerpTime = 1f;
    public enum rail { left,center,right}
    public rail currentRail;
    public Transform centerRail;
    public Transform leftRail;
    public Transform rightRail;
    public bool moving = false;
    public float speedMovement;

    float yInput, xInput;
    public List<wheel> wheels;
    Rigidbody _rigidbody;  [SerializeField]
    Vector3 centerOfMassTransform; 

    [Header("Sounds")] [SerializeField]
    SoundsController soundsController;
    public float newPitchValue;

    [Header("KarMovement")]
    public float maxAceleration = 20.0f;
    public float turnSensitivity = 1.0f;
    public float maxSteerAngle = 45.0f;
    public float desAceleration = 80f;
    
    [Header("Speed")]
    public float speed;
    public float maxSpeed;
    public Image speedmeterArrowImage;
    public float smoothArrow;

    [Header("Turbo")]
    [SerializeField] float turboDuration;
    [SerializeField]  bool turboEnable = false;
    [Range(1, 5f)][SerializeField] float speedIncrement;
    float velocityMultiplier = 1f;
    [SerializeField] ParticleSystem turboParticle;

    [Header("Drift")]
    [Range(0.5f,5f)]
    public float _DriftDuration;
    public bool _drifting = false;
    public float _extremumSlip;
    public float _extremumValue;
    WheelFrictionCurve defaultWheelRearConfig;

    [Header("NoControlParameters")][SerializeField]
    float noControlTime;
    public bool _wheelOil = false;
    public float extremumSlip;
    public float extremumValue;
    public float rotatorKarValue;

    [Header("KarJump")]
    public float jumpForce;
    public bool canJump = false;

    private void Start()
    {
        Time.timeScale = 1;
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.centerOfMass = centerOfMassTransform;
        StartCoroutine(ShowSpeed());

        defaultWheelRearConfig = wheels[3].wheelCollider.sidewaysFriction;
        defaultWheelRearConfig.extremumSlip = wheels[3].wheelCollider.sidewaysFriction.extremumSlip;
        defaultWheelRearConfig.extremumValue = wheels[3].wheelCollider.sidewaysFriction.extremumValue;

        StartCoroutine(ControlMotorCarSound());
        
    }
    IEnumerator ControlMotorCarSound()
    {
        yield return new WaitForSeconds(0.25f);
        float pitchAdd = speed / (maxSpeed*2); //con ete cálculo haremos que lo maximo que pueda aumentar el pitch al llegar a velocidad máxima sea 1
        float newPitch = Mathf.Clamp(pitchAdd, 0f, 0.5f);
        soundsController.ControlCarMotor(newPitch +soundsController.neutralPitch);
        StartCoroutine(ControlMotorCarSound());
    }
    public void Jump()
    {
        if(mode==gameMode.racing)
        {
            if (canJump)
            {
                _rigidbody.AddForce(Vector3.up * jumpForce * _rigidbody.mass);
                canJump = false;
            }
            else
                return;
        }
        else //si estamos jugando por movimiento por carriles
        {
            if(canJump)
            {
                _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
                _rigidbody.AddForce(Vector3.up * jumpForce * _rigidbody.mass);
                canJump = false;
            }
           
        }
    }
    public void NoKarControl()
    {
        if(!_wheelOil)
        {
            _wheelOil = true;
            transform.Rotate(Vector3.down * Mathf.SmoothStep(transform.rotation.y, rotatorKarValue,0.5f));
            soundsController.Drifting.Play();

            foreach (wheel _wheels in wheels)
            {
                if (_wheels.axel == Axel.rear)
                {
                    WheelFrictionCurve sideWayFriction = _wheels.wheelCollider.sidewaysFriction;
                    sideWayFriction.extremumSlip = extremumSlip;
                    sideWayFriction.extremumValue = extremumValue;
                    _wheels.wheelCollider.sidewaysFriction = sideWayFriction;
                }
            }
            StartCoroutine(returnKarControl());
        }
    }
    IEnumerator returnKarControl()
    {
        yield return new WaitForSeconds(noControlTime);
        _wheelOil = false;
        soundsController.Drifting.Stop();

        foreach (wheel _wheels in wheels)
        {
            if (_wheels.axel == Axel.rear)
            {
                WheelFrictionCurve sideWayFriction = _wheels.wheelCollider.sidewaysFriction;
                sideWayFriction.extremumSlip = defaultWheelRearConfig.extremumSlip;
                sideWayFriction.extremumValue = defaultWheelRearConfig.extremumValue;
                _wheels.wheelCollider.sidewaysFriction = sideWayFriction;
            }
        }
    }
    public void Drift()
    {
        if(mode==gameMode.racing)
        {
            if (!_drifting)
            {
                _drifting = true;
                foreach (wheel _wheels in wheels)
                {
                    if (_wheels.axel == Axel.rear)
                    {
                        WheelFrictionCurve sideWayFriction = _wheels.wheelCollider.sidewaysFriction;
                        sideWayFriction.extremumSlip = _extremumSlip;
                        sideWayFriction.extremumValue = _extremumValue;
                        _wheels.wheelCollider.sidewaysFriction = sideWayFriction;
                        soundsController.Drifting.Play();
                    }
                }
                StartCoroutine(returnNoDrift());
            }
            else
                return;
        }       
    }
    public void DisableMovement()
    {
        foreach(wheel _wheels in wheels)
        {
            _wheels.wheelCollider.brakeTorque = desAceleration;
        }
        enabled = false;
    }
    IEnumerator returnNoDrift()
    {
        yield return new WaitForSeconds(_DriftDuration);
        _drifting = false;
        soundsController.Drifting.Stop();

        foreach (wheel _wheels in wheels)
        {
            if (_wheels.axel == Axel.rear)
            {
                WheelFrictionCurve sideWayFriction = _wheels.wheelCollider.sidewaysFriction;
                sideWayFriction.extremumSlip = defaultWheelRearConfig.extremumSlip;
                sideWayFriction.extremumValue = defaultWheelRearConfig.extremumValue;
                _wheels.wheelCollider.sidewaysFriction = sideWayFriction;
            }
        }
    }
    private void Update()
    {
        GetInputs();
    }
    void GetInputs()
    {
        yInput = Input.GetAxis("Vertical");
        xInput = Input.GetAxis("Horizontal");

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Drift();
        }
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            Jump();
        }
    }
    private void LateUpdate()
    {
        Move();

        if(mode==gameMode.racing)
        {
            Turn();
        }
        else
        {
            MoveThroughRails();
        }
        AnimateWheels();
    }
    
    void MoveThroughRails()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) 
        {
            switch(currentRail)
            {
                case rail.center:
                    {
                        StartCoroutine(moveToRail(leftRail,rail.left));
                        break;
                    }
                case rail.right:
                    {
                        StartCoroutine(moveToRail(centerRail, rail.center));
                        break;
                    }
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            switch (currentRail)
            {
                case rail.center:
                    {
                        StartCoroutine(moveToRail(rightRail, rail.right));
                        break;
                    }
                case rail.left:
                    {
                        StartCoroutine(moveToRail(centerRail, rail.center));
                        break;
                    }
            }
        }
    }
    IEnumerator moveToRail(Transform target, rail _rail)
    {
        bool keyHit = true; 

        while(keyHit)
        {
            currentLerpTime += Time.deltaTime;
            if(currentLerpTime>=lerpTime)
            {
                keyHit = false;
                currentLerpTime = 0;
                currentRail = _rail;
            }
            float smooth = currentLerpTime / lerpTime;
            transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x,transform.position.y,transform.position.z), smooth);
            transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, smooth);
            yield return null;
        }
    }
    public void SetGameData(bool Won,int secondsRemaining)
    {
        int gamesPlayed = PlayerPrefs.GetInt(SaveData.GamesPlayedDataID);
        PlayerPrefs.SetInt(SaveData.GamesPlayedDataID, gamesPlayed+=1);

        if(Won)
        {
            int gameWins = PlayerPrefs.GetInt(SaveData.GameWinsDataID);
            PlayerPrefs.SetInt(SaveData.GameWinsDataID, gameWins += 1);
        }
        if(secondsRemaining>PlayerPrefs.GetInt(SaveData.GameRecordDataID))
        {
            PlayerPrefs.SetInt(SaveData.GameRecordDataID, secondsRemaining);
        }
    }
    public void EnableTurbo()
    {
        StartCoroutine(SetTurbo());
    }
    IEnumerator SetTurbo()
    {
        turboEnable = true;
        velocityMultiplier = speedIncrement;
        turboParticle.Play();
        yield return new WaitForSeconds(turboDuration);
        velocityMultiplier = 1;
        turboEnable = false;
        turboParticle.Stop();
    }
    IEnumerator ShowSpeed()
    {
        yield return new WaitForSeconds(smoothArrow);      
        float _speed =  Mathf.Abs((2 * Mathf.PI * wheels[0].wheelCollider.radius) * wheels[0].wheelCollider.rpm * 60 / 1000);
        speed = Mathf.Clamp(_speed, 0, maxSpeed);
        float speedMeterAngle = speed * 180 / maxSpeed;
        float speedmeterAngleCampled = Mathf.Clamp(speedMeterAngle, 0, 180);

        if(speedmeterArrowImage!=null)
        {
            speedmeterArrowImage.rectTransform.rotation = Quaternion.Euler(Vector3.forward * -speedmeterAngleCampled);
        }
        
        StartCoroutine(ShowSpeed());
    }
    private void Move()
    {
        if(mode==gameMode.racing)
        {
            foreach (wheel _wheels in wheels)
            {
                if (speed < maxSpeed)
                {
                    _wheels.wheelCollider.motorTorque = yInput * maxAceleration * velocityMultiplier * 500 * Time.deltaTime;
                }
                else
                {
                    _wheels.wheelCollider.motorTorque = 0;
                }
            }
            if (Input.GetAxis("Vertical") == 0)
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
        else if(mode==gameMode.trails)
        {
            if(yInput>0)
            {
                if (!moving)
                {
                    StartCoroutine(moveToMaxSpeed());   
                }
                
            }
        }
    }
   
    IEnumerator movementRail()
    {
        while(moving)
        {
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _rigidbody.velocity.y, speedMovement);
            speed = maxSpeed;
            yield return null;
        }
    }
    IEnumerator moveToMaxSpeed()
    {
        moving = true;
        timer.CallTimer();
        StartCoroutine(ShowSpeed());
        float lerpTime = 2;
        float currentLerpTime = 0;

        bool _keyHit = true;

        while(_keyHit)
        {
            currentLerpTime += Time.deltaTime;
            if(currentLerpTime>=lerpTime)
            {
                _keyHit = false;
                currentLerpTime = 0;
                StartCoroutine(movementRail());
            }
            float smooth = currentLerpTime / lerpTime;
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _rigidbody.velocity.y, Mathf.Lerp(0, speedMovement,smooth));
            yield return null;
        }
        
    }
    private void Turn()
    {
        foreach (wheel _wheels in wheels)
        {
            if (_wheels.axel == Axel.front)
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

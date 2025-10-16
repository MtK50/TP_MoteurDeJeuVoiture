using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class TPCarController : MonoBehaviour
{
    private GameManager gm;
    private enum ECarAxel
    {
        Front, Rear
    }

    [Serializable]
    private struct Wheel
    {
        public GameObject WheelMesh;
        public WheelCollider WheelCollider;
        public ECarAxel Axel;
    }
    private enum  ECurrentCameraState
    {
        Normal, Reversed
    }


    [SerializeField] private List<Wheel> wheels;

    [Header("Settings")]
    [SerializeField] private float maxAcceleration = 30.0f;
    [SerializeField] private float brakeAcceleration = 50.0f;
    [SerializeField] private float maxSteerAngle = 30.0f;

    private float moveInput;
    private float steerInput;

    [Header("Particules System")]
    [SerializeField] private ParticleSystem echapement;
    [SerializeField] private float rateOverTimeMin = 6f;
    [SerializeField] private float rateOverTimeMax = 30f;

    [Header("Stop Lights")]
    [SerializeField] private List<Light> stopLights;

    [Header("Audio")]
    [SerializeField] private AudioSource honkAS;

    private ECurrentCameraState currentCameraState = ECurrentCameraState.Normal;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        if (gm == null)
        {
            Debug.LogError("GameManager not found in the scene.");
        }

    }

   
    void Update()
    {
        GetInputs();
        AnimateWheels();
    }

    void LateUpdate()
    {
        Move();
        Steer();
        Brake();
        HandBrake();
    }

    private void GetInputs()
    {
        moveInput = Input.GetAxis("Vertical");
        steerInput = Input.GetAxis("Horizontal");

        ParticleSystem.EmissionModule emission = echapement.emission;
        if (moveInput > 0)
        {
            emission.rateOverTime = rateOverTimeMax;
            echapement.Play();
        }
        if (moveInput < 0)
        {
            emission.rateOverTime = rateOverTimeMin;
            echapement.Play();
        }
        if (moveInput == 0)
        {
            echapement.Stop();
        }
    }

    void Move()
    {
        //Exercice 4.1: appliquer un moment cin�tique en fonction de moveInput et de maxAcceleration
        foreach (Wheel wheel in wheels)
        {
            wheel.WheelCollider.motorTorque = moveInput * maxAcceleration;
        }

        
    }

    void Steer()
    {
        //Exercice 4.1: appliquer un angle de direction aux roues avant bas� sur steerInput et maxSteerAngle
        foreach (Wheel wheel in wheels)
        {
            if (wheel.Axel == ECarAxel.Front)
            {
                wheel.WheelCollider.steerAngle = steerInput * maxSteerAngle;
            }
        }
    }

    void Brake()
    {
        //Exercice 4.1: appliquer un couple de freinage bas� sur brakeAcceleration, lorsqu'on appuies sur espace, sinon laisser ce couple � 0
        if (moveInput < 0)
        {
            foreach (Wheel wheel in wheels)
            {
                wheel.WheelCollider.brakeTorque = brakeAcceleration;
            }
            foreach (Light light in stopLights)
            {
                light.enabled = true;
            }
        }
        else
        {
            foreach (Wheel wheel in wheels)
            {
                wheel.WheelCollider.brakeTorque = 0f;
            }
            foreach (Light light in stopLights)
            {
                light.enabled = false;
            }
        }
    }

    private void HandBrake() {
        if (Input.GetKey(gm.handbrakeKey))
        {
            foreach (Wheel wheel in wheels)
            {
                if (wheel.Axel == ECarAxel.Rear)
                {
                    wheel.WheelCollider.brakeTorque = 0f;
                }
            }
        }
    }

    void AnimateWheels()
    {
        foreach (Wheel wheel in wheels)
        {
            Quaternion rot;
            Vector3 pos;
            wheel.WheelCollider.GetWorldPose(out pos, out rot);
            wheel.WheelMesh.transform.position = pos;
            wheel.WheelMesh.transform.rotation = rot;
        }
    }

    public void Honk()
    {
        honkAS.Play();
    }

    public void ReverseCamera()
    {

        Camera cc = gm.mainCamera;
        // C'est degueulasse, j'ai honte mais flemme de galerer encore + désolé
        if (cc != null)
        {
            if (currentCameraState == ECurrentCameraState.Normal)
            {
                currentCameraState = ECurrentCameraState.Reversed;
                cc.transform.localRotation = Quaternion.Euler(10f, -180f, 0f);
                cc.transform.localPosition = new Vector3(0f, 2f, 3.74f);
            }
            else
            {
                currentCameraState = ECurrentCameraState.Normal;
                cc.transform.localRotation = Quaternion.Euler(10f, 0f, 0f);
                cc.transform.localPosition = new Vector3(0f, 2f, -3.74f);
            }

        }
    }
}


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
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


    [SerializeField]
    private List<Wheel> wheels;

    [Header("Settings")]
    [SerializeField]
    private float maxAcceleration = 30.0f;
    [SerializeField]
    private float brakeAcceleration = 50.0f;
    [SerializeField]
    private float maxSteerAngle = 30.0f;

    float moveInput;
    float steerInput;

    void Start()
    {

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
    }

    private void GetInputs()
    {
        moveInput = Input.GetAxis("Vertical");
        steerInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            transform.position = Vector3.zero;
        }
    }

    void Move()
    {
        //Exercice 4.1: appliquer un moment cin�tique en fonction de moveInput et de maxAcceleration
        foreach(Wheel wheel in wheels)
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
        if (Input.GetKey(KeyCode.Space) || moveInput == 0)
        {
            foreach (Wheel wheel in wheels)
            {
                wheel.WheelCollider.brakeTorque = brakeAcceleration;
            }
        }
        else
        {
            foreach (Wheel wheel in wheels)
            {
                wheel.WheelCollider.brakeTorque = 0f;
            }
        }
    }

    void AnimateWheels()
    {
        foreach(Wheel wheel in wheels)
        {
            Quaternion rot;
            Vector3 pos;
            wheel.WheelCollider.GetWorldPose(out pos, out rot);
            wheel.WheelMesh.transform.position = pos;
            wheel.WheelMesh.transform.rotation = rot;
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public TPCarController player;
    [SerializeField] public Camera mainCamera;
    private Transform plT;
    [SerializeField] private Transform spawnpoint;

    [Header("Key Inputs")]
    [SerializeField] public KeyCode respawnKey;
    [SerializeField] public KeyCode resetCarKey;
    [SerializeField] public KeyCode honkKey;
    [SerializeField] public KeyCode reverseCameraKey;
    [SerializeField] public KeyCode handbrakeKey;


    void Awake()
    {
        if (player == null)
        {
            player = FindObjectOfType<TPCarController>();
        }
        plT = player.transform;
        if(mainCamera == null)
        {
            mainCamera = FindObjectOfType<Camera>();
        }
    }

    void Update()
    {
        CheckPlayerInputs();
    }
    
    private void CheckPlayerInputs()
    {
        if (Input.GetKeyDown(respawnKey))
        {
            plT = spawnpoint;
        }
        if (Input.GetKeyDown(resetCarKey))
        {
            plT.localPosition = new Vector3(plT.localPosition.x, plT.localPosition.y + 0.5f, plT.localPosition.z);
            plT.eulerAngles = new Vector3(0f, plT.localRotation.y, 0f);
        }
        if (Input.GetKeyDown(honkKey))
        {
            player.Honk();
        }
        if (Input.GetKeyDown(reverseCameraKey))
        {
            player.ReverseCamera();
        }
        

    }
}

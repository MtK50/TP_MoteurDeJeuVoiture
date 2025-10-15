using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public TPCarController player;
    private Transform plT;
    [SerializeField] private Transform spawnpoint;

    [Header("Key Inputs")]
    [SerializeField] private KeyCode respawnKey;
    [SerializeField] private KeyCode resetCarKey;
    [SerializeField] private KeyCode honkKey;
    [SerializeField] private KeyCode reverseCameraKey;


    void Awake()
    {
        if (player == null)
        {
            player = FindObjectOfType<TPCarController>();
        }
        plT = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerInputs();
    }
    
    private void CheckPlayerInputs()
    {
        if (Input.GetKeyDown(respawnKey))
        {
            plT = spawnpoint;
            // plT.SetLocalPositionAndRotation(spawnpoint.position, spawnpoint.rotation);
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

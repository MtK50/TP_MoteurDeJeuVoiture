using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class UIText : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI uiText;
    // Start is called before the first frame update
    void Start()
    {
        UpdateUIText();
    }

    private void UpdateUIText()
    {
        GameManager gm = FindObjectOfType<GameManager>();
        if (gm != null)
        {
            string instructions = $"'{gm.respawnKey}' to Respawn\n" +
                                  $"'{gm.resetCarKey}' to Reset Car\n" +
                                  $"'{gm.honkKey}' to Honk\n" +
                                  $"'{gm.reverseCameraKey}' to Reverse Camera\n" +
                                  $"'{gm.handbrakeKey}' for Handbrake\n" +
                                  $"Use Arrow Keys or 'WASD' to Drive";
            uiText.text = instructions;
        }
        else
        {
            Debug.LogError("GameManager not found in the scene.");
        }
    }
}

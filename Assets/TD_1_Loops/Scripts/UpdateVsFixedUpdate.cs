using UnityEngine;

enum UpdateSpeed {
    Fast120 = 120,
    Medium60 = 60,
    Slow24 = 24,
}
public class UpdateVsFixedUpdate : MonoBehaviour
{
    [SerializeField] UpdateSpeed speed = UpdateSpeed.Medium60;

    // Exercice 1.1 observez l'ordre d'appel d'Update, de LateUpdate et de FixedUpdate, en fonction de différents framerates
    void Start()
    {
        QualitySettings.vSyncCount = 0;


        //Force le nombre de FPS à 60
        Application.targetFrameRate = (int)speed;

        // Test 1
        // Application.targetFrameRate = 120;


        // Test 2
        // Application.targetFrameRate = 24;
    }

    void Update()
    {
        Debug.Log("Update");
    }

    void LateUpdate()
    {
        Debug.Log("LateUpdate");
    }

    private void FixedUpdate()
    {
        Debug.Log("FixedUpdate");
    }
}

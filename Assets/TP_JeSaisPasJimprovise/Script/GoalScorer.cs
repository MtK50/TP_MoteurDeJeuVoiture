using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScorer : MonoBehaviour
{

    [SerializeField] private List<ParticleSystem> confettis;
    [Range(0f, 10f)]
    [SerializeField]  const float confettiDuration = 2f;

    void Start()
    {
        foreach (var confetti in confettis)
        {
            confetti.Stop();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball") || other.CompareTag("Player"))
        {
            Debug.Log("Goal Scored!");
            StartCoroutine(PlayConfettiForDuration(confettiDuration));
        }
    }

    private IEnumerator PlayConfettiForDuration(float duration)
    {
        foreach (var confetti in confettis)
        {
            confetti.Play();
        }
        yield return new WaitForSeconds(duration);
        foreach (var confetti in confettis)
        {
            confetti.Stop();
        }
    }
}

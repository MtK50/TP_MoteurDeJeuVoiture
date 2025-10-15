using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PressurePlate : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Animator animator;

    private bool pressed;
    private float pressTime;
    [SerializeField] private float pressDuration = 1.5f;

    // Exercice 1.3: d�commentez ce code
    public void OnPointerClick(PointerEventData eventData)
    {
        StartCoroutine(PressAndRelease());
    }

    private IEnumerator PressAndRelease()
    {
        Debug.Log("333");
        pressed = true;
        // pressTime = Time.time;
        animator.SetBool("Pressed", true);
        yield return new WaitForSeconds(pressDuration);
        pressed = false;
        animator.SetBool("Pressed", false);
    }

    // void Update()
    // {
    //     if (pressed)
    //     {
    //         Debug.Log("Oui");
    //         StartCoroutine(PressAndRelease());
    //         // if (Time.time > pressTime + pressDuration)
    //         // {

    //         // }
    //     }
    // }

    //  waitPressDuration()
    // {
    //     yield return new WaitForSeconds(pressDuration);

    // }


    // Exercice 1.2: d�commentez ce code
    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.SetBool("Pressed", true);
    }

    // Exercice 1.2: d�commentez ce code
    public void OnPointerExit(PointerEventData eventData)
    {
        animator.SetBool("Pressed", false);
    }
}

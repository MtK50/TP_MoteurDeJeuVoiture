using UnityEngine;
using UnityEngine.EventSystems;

public class ConeRaycaster : MonoBehaviour
{
    [SerializeField] private LayerMask m_LayerMask;

    void Update()
    {
        //Exercice 2.3 si on fait un clic gauche, faire un raycast depuis la souris, et si on touche un cone, appliquer une force au rigidbody du Cone
        // if (Input.GetMouseButtonDown(0))
        if(Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //TODO: appliquer une force au point de contact avec le Cone
            if(Physics.Raycast(ray, out RaycastHit hit, 15f, m_LayerMask))
            {
                hit.rigidbody.AddExplosionForce(1500f, hit.transform.position, 10f); 
                Debug.Log("Oui");
            }
        }
        
    }
}

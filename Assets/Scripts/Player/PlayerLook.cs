using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera mainCamera; //À assigner dans l'inspecteur.

    private void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Default")))
        {
            //Affiche la position du point de contact dans la Scene View
            //Debug.DrawRay(hit.point, hit.normal, Color.red);


            Vector3 lookPoint = hit.point;
            lookPoint.y = transform.position.y;

            //Debug.DrawLine(transform.position, lookPoint, Color.blue);
            transform.LookAt(lookPoint);
        }
    }
}

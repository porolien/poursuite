using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quaternions : MonoBehaviour
{
    public Transform cible;

    void Start()
    {
        Vector3 direction = cible.position - transform.position;
        transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
    }

    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * 5, Color.blue);
        Debug.DrawRay(cible.position, cible.forward * 5, Color.red);

        // On veut pivoter l'objet de 20 degrés par seconde
        float angularSpeed = 20 * Time.deltaTime;

        // On prend la rotation de l'objet, et on lui applique 
        // une rotation sur l'axe Y (Vector3.up)
        transform.rotation *= Quaternion.Euler(0, angularSpeed, 0);
    }
}

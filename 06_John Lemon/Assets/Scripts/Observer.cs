using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CapsuleCollider))]

public class Observer : MonoBehaviour
{
    public Transform player;//Queremos observar al Player, buscaremos su posición a través del transform

    private bool isPlayerInRange;//¿Está player en el rango de visión?

    public GameEnding gameEnding;//Para capturar el script que controla el fin de la partida

    private void OnTriggerEnter(Collider other)
    {
        //Si el objeto que entra en la zona de visión es el player
        if (other.transform == player)
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Si el player sale de la zona de visión
        if (other.transform == player)
        {
            isPlayerInRange = false;
        }
    }

    private void Update()
    {
        //En cada frame se verifica si el player es visible de forma directa
        //o hay otro objeto entre ambos que impide esta visión
        //Para ello utilizaremos un rayo y verificaremos si choca con algo
        if (isPlayerInRange)
        {
            //Dirección del rayo: desde la posición del punto de visión hasta la del el player (resta)
            //Se aplica un Vector3.up para sumar un metro en el eje Y, debido a que en este caso Player tiene su
            //origen (Pivot) a la altura del centro de los pies
            Vector3 direction = player.position - transform.position + Vector3.up;
            
            //Se dibuja el rayo desde este objeto en la dirección calculada hacia player
            Ray ray = new Ray(transform.position, direction);
            //Hacemos visible el rayo si tenemos activados los Gizmos
            Debug.DrawRay(transform.position, direction, Color.yellow,
                Time.deltaTime);

            //Declaramos un RaycastHit que pasaremos por referencia al siguiente método para que éste le
            //dé valor, una información sobre el posible objeto con el que choque el rayo
            RaycastHit raycastHit;
            
            //Comprobaremos si el rayo intercepta algún objeto en su trayectoria, obteniendo información sobre él
            if (Physics.Raycast(ray, out raycastHit))
            {
                //Si el rayo ha chocado con un objeto (con su collider) y ese objeto es el player
                if (raycastHit.collider.transform == player)
                {
                    //Debug.Log("Cazado!!!!");
                    gameEnding.CatchPlayer();
                }
            }
        }
    }

    //Mostramos gizmos con la posición de los pointofview de cada gárgola y una línea hacia el player
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.1f);
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position, player.position + Vector3.up);
    }
}

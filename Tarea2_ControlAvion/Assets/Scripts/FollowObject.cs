using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
   [SerializeField, Tooltip("Objeto al que seguir")]
   private GameObject objectFollow;

   private Vector3 offset = new Vector3(0f, 10f, -40f);
   
   /*Alternativa para que la cámara rote también siguiendo al objeto
    private Vector3 playerPreviousPosition = Vector3.zero;
   private float distance = 30f;//Distancia de la cámara al objeto */
   private void Update()
   {

      transform.position = objectFollow.transform.position + offset;
      /*Alternativa para que la cámara rote también siguiendo al objeto
      //Se calcula cuánto se ha desplazado el objeto a seguir (posición actual - posición anterior)
      Vector3 offset = objectFollow.transform.position - playerPreviousPosition;//Vector de desplazamiento del objeto

      if (offset.magnitude < 0.01f) //Si el objeto en este frame se hubiera movido muy poco
      {
         return;
      }//No haremos nada, para evitar ciertos efectos extraños de la cámara en ordenadores muy potentes

      offset.Normalize();//Normaliza el vector obtenido a tamaño 1
      //La cámara se moverá en la misma proporción que se ha movido el objeto
      transform.position = objectFollow.transform.position - offset * distance;//Aunque manteniendo siempre la distancia
      //La cámara ha de mirar siempre hacia el objeto (irá rotando si éste rota)
      transform.LookAt(objectFollow.transform.position);
      //Actualizamos la posición del objeto
      playerPreviousPosition = objectFollow.transform.position; */
   }
}

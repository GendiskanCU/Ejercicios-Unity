using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    //Propiedades
    [Range(0, 20), SerializeField] [Tooltip("Velocidad lineal máxima actual del coche")]
    private float speed = 5.0f;//Valor por defecto 5
    
    [Range(0, 75), SerializeField] [Tooltip("Velocidad de giro máxima actual del coche")]
    private float turnSpeed = 60.0f;//Velocidad de giro
    
    private float horizontalInput, verticalInput;//Para capturar la posición de teclas de movimiento
    
    
    


    // Update is called once per frame
    void Update()
    {
        verticalInput = Input.GetAxis("Vertical");
        // Tenemos que mover el vehículo hacia adelante, en el eje z, con una velocidad multiplicada por el factor
        //de movimiento dado por la pulsación del usuario en el eje vertical
        transform.Translate(speed * Time.deltaTime * Vector3.forward * verticalInput);

        horizontalInput = Input.GetAxis("Horizontal");//Se captura el valor de pulsación del mando en el eje horizontal
        //Gira a izquierda o derecha según turnSpeed sea negativo o positivo
        //transform.Translate(turnSpeed * Time.deltaTime * Vector3.right * horizontalInput);//Solo se moverá iz/de
        transform.Rotate(turnSpeed * Time.deltaTime * Vector3.up * horizontalInput);//Rotará a iz/de
    }

   
}

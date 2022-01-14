using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField, Range(5f, 25f), Tooltip("Velocidad de avance de la avioneta")]
    private float forwardSpeed = 5.0f;

    [SerializeField, Range(5f, 50f), Tooltip("Velocidad de rotación de la avioneta")]
    private float rotationSpeed = 5.0f;

    private float inputHorizontal, inputVertical;
    
    private void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.P))//Tecla P aumenta la velocidad
            IncreaseForwardSpeed();
        if(Input.GetKeyDown(KeyCode.O))//Tecla O disminuye la velocidad
            DecreaseForwardSpeed();
        
        //La avioneta debe ir avanzando a la velocidad indicada
        transform.Translate(forwardSpeed * Time.deltaTime * Vector3.forward);

        //Rotación de la avioneta izquierda/derecha
        inputHorizontal = Input.GetAxis("Horizontal");
        transform.Rotate(rotationSpeed * Time.deltaTime * Vector3.up * inputHorizontal);
        
        //Rotación de la avioneta arriba/abajo (flecha abajo, subirá/ flecha arriba, bajará
        inputVertical = Input.GetAxis("Vertical");
        transform.Rotate(rotationSpeed * Time.deltaTime * Vector3.right * inputVertical);

    }

    private void IncreaseForwardSpeed()
    {
        if (forwardSpeed < 25f)
            forwardSpeed+= 5f;
    }

    private void DecreaseForwardSpeed()
    {
        if (forwardSpeed > 5f)
            forwardSpeed-= 5f;
    }
}

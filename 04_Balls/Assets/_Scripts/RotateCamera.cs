using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public float rotationSpeed = 10.0f;//Velocidad de rotación

    private float horizontalInput;
    
    
    // Update is called once per frame
    void Update()
    {
        //Rotaremos a izquierda/derecha, en el eje y al pulsar las teclas o botones del eje horizontal
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);
    }
}

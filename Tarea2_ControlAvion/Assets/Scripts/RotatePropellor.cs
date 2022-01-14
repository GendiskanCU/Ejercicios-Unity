using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePropellor : MonoBehaviour
{
    private float rotationVelocity = 2000f; //Velocidad rotación del objeto
    
    void Update()
    {
        transform.Rotate(rotationVelocity * Time.deltaTime * Vector3.forward);        
    }
}

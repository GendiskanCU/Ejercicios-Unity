using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed = 40.0f;


    // Update is called once per frame
    void Update()
    {
        //Movemos el objeto hacia delante a la velocidad dada
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}

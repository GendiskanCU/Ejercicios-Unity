using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]

public class RepeatBackground : MonoBehaviour
{
    private Vector3 startPos;//Posición inicial del fondo

    private float repeatWidth;//Anchura del fondo (de su collider en realidad)
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        
        //Obtenemos el tamaño del collider en x, que es el eje que interesa, y lo dividimos por 2 para obtener el valor
        //exacto en el que se habrá alcanzado punto en que la imagen de fondo debe recolocarse
        repeatWidth = GetComponent<BoxCollider>().size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        //Si se alcanza el punto de repetición recolocamos la imagen de fondo
        if (startPos.x - transform.position.x > repeatWidth)
            transform.position = startPos;
    }
}

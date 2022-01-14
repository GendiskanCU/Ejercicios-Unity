using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//DESTRUIRÁ TODOS LOS OBJETOS INSTANCIADOS CUANDO QUEDEN FUERA DE PANTALLA

public class DestroyOutOfBounds : MonoBehaviour
{
    
    private float topBound = 30.0f;//Frontera superior de la cámara
    private float lowerBound = -6.0f;//Frontera inferior de la cámara
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.z > topBound)//Los proyectiles superan el borde superior
            Destroy(gameObject);

        if (transform.position.z < lowerBound)//Algún enemigo supera el borde inferior
        {
            Debug.Log("GAME OVER");//Se pierde la partida
            Destroy(gameObject);

            Time.timeScale = 0f;//Detenemos el juego
        }
    }
}

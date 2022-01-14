using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public GameObject dogPrefab;

    private float timeElapse = 0f;//Tiempo transcurrido desde la salida del último perro

    private float intervalTime = 1.0f;//Solo se podrá enviar un perro por segundo
    // Update is called once per frame
    void Update()
    {
        timeElapse += Time.deltaTime;
        
        // On spacebar press, send dog
        if (Input.GetKeyDown(KeyCode.Space) && timeElapse >= intervalTime)
        {
            timeElapse = 0f;//Reiniciamos el tiempo desde la salida del último perro
            Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);
        }
    }
}

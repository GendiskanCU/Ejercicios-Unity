using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private float forwardInput;

    public float moveForce = 1.0f;//Fuerza de movimiento de la bola

    private GameObject focalPoint;//Para poder saber hacia dónde apunta el punto focal

    private bool hasPowerUp;//Para saber cuándo el player ha recogido un powerup
    
    public float powerUpForce = 1.0f;//Fuerza del powerup de repulsión

    public float powerUpTime = 7.0f;//Tiempo de duración del powerup

    public GameObject[] powerUpIndicators;//Indicadores de estado del powerup
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();//Capturamos el rigidbody
        focalPoint = GameObject.Find("Focal Point");//Capturamos el punto focal
    }

    // Update is called once per frame
    void Update()
    {
        //La bola solo se va a mover en un eje, el Z
        //Cuando pulsemos el botón o tecla del eje vertical
        forwardInput = Input.GetAxis("Vertical");
        //Aplicamos una fuerza sobre ella para darle el movimiento
        //Pero no haremos que el movimiento se efectúe con respecto a las coordenadas globales:
        //_rigidbody.AddForce(Vector3.forward * moveForce * forwardInput);
        //Sino que esa fuerza la vamos a aplicar con respecto a la dirección a la que apunte el punto focal
        //ya que es el que marca también la rotación de la cámara. Así, la bola avanzará hacia donde miremos
        _rigidbody.AddForce(focalPoint.transform.forward * moveForce * forwardInput);

        //Hacemos que los anillos indicadores de estado del powerup se muevan junto al player, aunque un poco por debajo
        foreach (GameObject indicator in powerUpIndicators)
        {
            indicator.transform.position = transform.position + (0.5f * Vector3.down);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))//Cuando el player recoja un powerUp
        {
            hasPowerUp = true;
            Destroy(other.gameObject);
            
            StartCoroutine(PowerUpCountDown());//Arrancamos la corutina que pondrá fin al efecto del pwup
        }

        if (other.gameObject.name.CompareTo("KillZone") == 0)//Si el jugador cae y entra en la zona de muerte
        {
            SceneManager.LoadScene("Prototype 4");//Se recarga la escena
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerUp)//Cuando el player choque con un enemigo y esté activo el powerUp
        {
            //Haremos rebotar al enemigo en dirección contraria, aplicando una fuerza de repulsión
            Rigidbody enemyRibigbody = collision.gameObject.GetComponent<Rigidbody>();//Capturamos el rigidbody del enemigo
            //Se calcula la dirección de repulsión (posición del enemigo - posición del player)
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;//No hace falta normalizar el vector en este caso, las colisiones siempre van a ser a la misma distancia
            //Aplicamos la fuerza, con efecto de impulso instantáneo
            enemyRibigbody.AddForce(awayFromPlayer * powerUpForce, ForceMode.Impulse);
        }
    }

    IEnumerator PowerUpCountDown()
    {
        foreach (GameObject indicator in powerUpIndicators)
        {
            indicator.gameObject.SetActive(true); //Se activa el indicador de estado (el primero o el siguiente)
            yield return new WaitForSeconds(powerUpTime / powerUpIndicators.Length); //se espera la parte proporcional
            indicator.gameObject.SetActive(false); //Se desactiva el indicador de estado que estaba activo
        }
        //Pasado todo el tiempo de duración del powerup
        hasPowerUp = false;//Desactivamos el efecto powerup
    }
}

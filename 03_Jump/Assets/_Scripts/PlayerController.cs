using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]//Se requiere que el objeto que vaya a tener e script tenga un rigidbody

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;//El rigidbody del player

    public float jumpForce;//Fuerza del salto

    public float gravityMultiplier;//Multiplicador de la gravedad

    public bool isOnTheGround =true;//Para saber si el personaje está tocando el suelo y por tanto puede saltar

    private bool _gameOver;//Para saber si se han producido las circunstancias que provocan el fin de la partida

    public ParticleSystem explosion;//Para capturar el Sistema de partículas hijo del player
    public ParticleSystem runEffect;//Para capturar el otro sistema de partículas hijo del player

    public bool GameOver//Propiedad para que otros scripts puedan acceder al valor de _gameOver
    {
        get => _gameOver;
    }

    private Animator _animator;//Para capturar el componente Animator del player
    private const string SPEED_MULTIPLIER = "Speed multiplier";
    private const string SPEDD_F = "Speed_f";
    private const string JUMP_TRIGGER = "Jump_trig";
    private const string DEATH_B = "Death_b";
    private const string DEATH_TYPE_INT = "DeathType_int";

    private float speedMultiplier = 1.0f;//Multiplicador de la velocidad de movimiento de la animación de correr

    public AudioClip jumpSound, crashSound;//Clips de efectos de sonido
    private AudioSource _audioSource;//Audio Source para poder reproducir los clips de audio
    [Range(0, 1)] public float audioVolume = 1f;//Volumen de los efectos de sonido

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();//Capturamos el rigidbody

        Physics.gravity = gravityMultiplier * new Vector3(0, -9.81f, 0);//Fijamos la gravedad que va a afectar a las físicas

        _animator = GetComponent<Animator>();//Capturamos el Animator
        
        _animator.SetFloat(SPEDD_F, 1);//Fijamos de esta variable para que se muestre la animación de correr desde el principio

        _audioSource = GetComponent<AudioSource>();//Capturamos el AudioSource
    }

    // Update is called once per frame
    void Update()
    {
        speedMultiplier += Time.deltaTime / 10;
        _animator.SetFloat(SPEED_MULTIPLIER, speedMultiplier);//Se aumentará la  velocidad de animación de correr con el paso del tiempo
        
        if (Input.GetKeyDown(KeyCode.Space) && isOnTheGround && !_gameOver)
        {
            _animator.SetTrigger(JUMP_TRIGGER);//Activamos el trigger que inicia la animación de salto
            _audioSource.PlayOneShot(jumpSound, audioVolume);//Se reproduce una vez el sonido de salto, al volumen especificado desde el Inspector
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            runEffect.Stop();//Paramos la partícula que representa el rastro dejado al correr
            isOnTheGround = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !_gameOver)
        {
            runEffect.Play();//Iniciamos la partícula que representa el rastro dejado al correr
            isOnTheGround = true; //Cuando colisione con el suelo podrá saltar de nuevo
        }
        else if (collision.gameObject.CompareTag("Obstacle"))//Si choca con un obstáculo acabará la partida
        {
            _gameOver = true;
            _audioSource.PlayOneShot(crashSound, audioVolume);
            runEffect.Stop();//Paramos la partícula que representa el rastro dejado al correr
            explosion.Play();//Activamos la partícula que representa una explosión
            _animator.SetInteger(DEATH_TYPE_INT, 2);//Elegimos la animación de muerte que se va a mostrar de las dos disponibles
            _animator.SetBool(DEATH_B, true);//Activamos la animación de muerte
            Invoke("RestartGame", 2.5f);//Tras un segundo llamamos al método que restaura el juego
        }
    }

    private void RestartGame()
    {
        speedMultiplier = 1.0f;//Reseteamos la velocidad de movimiento de la animación de correr al valor inicial
        
        SceneManager.LoadSceneAsync("Prototype 3");//Reiniciamos la escena, en modo asíncrono para que no se note tanto la transición (cargará en segundo plano)
    }
}

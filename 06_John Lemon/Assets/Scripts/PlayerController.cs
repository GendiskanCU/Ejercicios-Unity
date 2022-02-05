//Hacemos una redefinición para usar las dos plataformas de móviles conjuntamente
#if UNITY_IOS || UNITY_ANDROID
    #define USING_MOBILE
#endif

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float turnSpeed;//Velocidad de giro del personaje (grados)
    
    private Vector3 movement;//Vector de movimiento del personaje
    private Animator _animator;//Controlador de las animaciones del personaje

    private Rigidbody _rigidbody;

    private Quaternion rotation;//Rotación del personaje

    private AudioSource _audioSource;//Para reproducir el sonido de los pasos
    
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
        
        rotation = Quaternion.identity;//Nos aseguramos de que al principio no haya ninguna rotación
    }

    // Utilizamos el FixedUpdate porque vamos a hacer cálculos que luego se utilizarán sobre las físicas
    // en el evento OnAnimatorMove, que sucede antes que el método Update
    void FixedUpdate()
    {
        //Capturamos la pulsación de los botones de los ejes de movimiento
        //En función de la plataforma de compilación del juego
        #if USING_MOBILE
            float horizontal = Input.GetAxis("Mouse X");
            float vertical = Input.GetAxis("Mouse Y");
            //Cuando el usuario haya pulsado al menos una vez sobre la pantalla
            if (Input.touchCount > 0)
            {
                //Cogemos el valor en x del primero de los toques (posición 0)
                horizontal = Input.touches[0].deltaPosition.x;
                //E igual para el otro eje
                vertical = Input.touches[0].deltaPosition.y;
            }
            
        #else 
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
        #endif
        
        
        //Lo asignamos al vector de movimiento del personaje
        movement.Set(horizontal, 0, vertical);
        //Lo normalizamos para que siempre sea igual de largo
        movement.Normalize();

        //Se comprueba si la pulsación en horizontal o en vertical es aproximada a cero (no se están pulsando)
        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        
        //Cuando haya pulsación en alguno de los dos ejes activaremos la animación de movimiento
        //La cual ya está configurada por el artista para mover al personaje además de animarlo
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        _animator.SetBool("isWalking", isWalking);
        //También reproduciremos el sonido de los pasos cuando el personaje esté en movimiento
        if (isWalking)
        {
            if (!_audioSource.isPlaying) //Si no se está reproduciendo ya el sonido
            {
                _audioSource.Play();
            }
        }
        else
        {
            _audioSource.Stop();
        }
        
        //Utilizamos el método RotateTowards para generar una rotación suave y
        // progresiva hacia la dirección del vector de movimiento:
        //Obteniendo la dirección de giro deseada
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, movement,
            turnSpeed * Time.fixedDeltaTime, 0f);//Usamos el equivalente DeltaTime de Fixed
        //La convertimos en una rotación (Quaternion) usando el método LookRotation
        //El método OnAnimatorMove lo utilizará para rotarlo en cada cambio de la animación
        rotation = Quaternion.LookRotation(desiredForward);
    }

    //Cuando haya que procesar algún cambio en la animación (evento de Unity)
    //Sobreescribe al asociado a la propiedad "Apply Root Motion" del componente Animator
    private void OnAnimatorMove()
    {
        /*Movemos al personaje, teniendo en cuenta que la animación ya lo mueve un poco
        por lo que ahora en vez de usar time.deltatime usamos la cantidad de movimiento que ya aporta
        la animación. Conforme a esto, el movimiento será más rápido o más lento si desde código o
        desde diseño modificamos la velocidad de la propia animación*/
        _rigidbody.MovePosition(_rigidbody.position + movement * _animator.deltaPosition.magnitude);
        
        //Rotamos al personaje
        _rigidbody.MoveRotation(rotation);
    }
}

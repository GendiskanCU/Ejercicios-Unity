using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float turnSpeed;//Velocidad de giro del personaje
    
    private Vector3 movement;//Vector de movimiento del personaje
    private Animator _animator;//Controlador de las animaciones del personaje

    private Rigidbody _rigidbody;

    private Quaternion rotation;//Rotación del personaje
    
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        rotation = Quaternion.identity;//Nos aseguramos de que al principio no haya ninguna rotación
    }

    // Update is called once per frame
    void Update()
    {
        //Capturamos la pulsación de los botones de los ejes de movimiento
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
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
        
        //Utilizamos el método RotateTowards para generar una rotación suave y
        // progresiva hacia la dirección del vector de movimiento:
        //Obteniendo la dirección de giro deseada
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, movement,
            turnSpeed * Time.deltaTime, 0f);
        //La convertimos en una rotación (Quaternion) usando el método LookRotation
        //El método OnAnimatorMove lo utilizará para rotarlo en cada cambio de la animación
        rotation = Quaternion.LookRotation(desiredForward);
        
        
    }

    //Cuando haya que procesar algún cambio en la animación (evento de Unity)
    //Sobreescribe al asociado a la propiedad "Apply Root Motion" del componente Animator
    private void OnAnimatorMove()
    {
        //Movemos al personaje, teniendo en cuenta que la animación ya lo mueve un poco
        //Por lo que ahora en vez de usar time.deltatime usamos la cantidad de movimiento que ya aporta
        //la animación
        _rigidbody.MovePosition(_rigidbody.position + movement * _animator.deltaPosition.magnitude);
        
        //Rotamos al personaje
        _rigidbody.MoveRotation(rotation);
    }
}

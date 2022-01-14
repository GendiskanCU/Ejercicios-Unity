using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof(Rigidbody))]

public class PlayerController : MonoBehaviour
{
    [Range(0, 10.0f)] public float moveSpeed;//Velocidad de movimiento del player

    [Range(0, 180.0f)] public float rotateSpeed;

    [Range(0, 10.0f)] public float force;

    public bool usePhysicsEngine;
    
    private Rigidbody _rigidbody;
    private float verticalInput, horizontalInput;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        MovePlayer();
        KeepPlayerInBounds();
        
    }

    void MovePlayer()
    {
        if (usePhysicsEngine)
        {
            //Movimiento y rotación si se utiliza física:
            //AddForce sobre el rigidbody
            //AddTorque sobre el rigidbody
            _rigidbody.AddTorque(Vector3.forward * force * Time.deltaTime * verticalInput, ForceMode.Force);
            _rigidbody.AddTorque(Vector3.up * force * Time.deltaTime * horizontalInput, ForceMode.Force);
        }
        else
        {
            //Movimiento y rotación si no se utiliza física:
            //Translate sobre el transform
            //Rotate sobre el transform
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime * verticalInput);
            transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime * horizontalInput);
        }
    }

    void KeepPlayerInBounds()
    {
        //TODO: Hay que declarar una variable para fijar los límites
        //Si se alcanzan los límites laterales
        if(Mathf.Abs(transform.position.x) > 24.0f || Mathf.Abs(transform.position.z) > 24.0f)
        {
            _rigidbody.velocity = Vector3.zero; //Anulamos la velocidad que lleve el objeto, parándolo
            //Y se le devuelve a la posición correcta
            if (transform.position.x > 24.0f)
            {
                transform.position = new Vector3(24.0f, transform.position.y, transform.position.z);
            }

            if (transform.position.x < -24.0f)
            {
                transform.position = new Vector3(-24.0f, transform.position.y, transform.position.z);
            }

            if (transform.position.z > 24.0f)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, 24.0f);
            }

            if (transform.position.z < -24.0f)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -24.0f);
            }
        }
    }
}

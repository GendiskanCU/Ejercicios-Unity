using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private Rigidbody _rigidbody;

    public float moveForce = 1.0f;//Intensidad de la fuerza de movimiento

    private GameObject player;//Para capturar al player, al cual debe perseguir
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //El enemigo deberá moverse hacia donde apunte el vector que une el centro del propio
        //enemigo con el centro del player. Para calcular cuál el valor de este vector haremos punto destino - pto origen
        //Vector3 direction = player.transform.position - transform.position;
        //Sin embargo, este vector debemos "normalizarlo" (hacerlo "igual a 1") porque si no, dependiendo de lo
        //lejos o lo cerca que esté el player la fuerza será menor o mayor. Normalizándolo hacemos que ésta siempre sea 1
        //Para ello haremos:
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        _rigidbody.AddForce(lookDirection * moveForce, ForceMode.Force);

        
        if (transform.position.y < -10) //Si el enemigo cae de la plataforma
        {
            Destroy(gameObject);//Lo destruimos
        }
    }
}

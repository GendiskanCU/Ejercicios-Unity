using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Target : MonoBehaviour
{

    private Rigidbody _rigidbody;

    [SerializeField] [Range(12, 15)] private float minForce = 12;
    [SerializeField] [Range(16, 20)] private float maxForce = 16;
    [SerializeField] [Range(2.5f, 4.0f)] private float positionX = 4.0f;
    [SerializeField] private float rangeOfTorque = 10;
    [SerializeField] private ParticleSystem explosionParticle;
    [SerializeField][Range(-50, 50)] private int pointValue;//Puntuación que da cada target
    private int penalizeFall = -5;//Penalización si el objeto cae sin ser atrapado
    
    private float ySpawnPos = -6;

    private GameManager gameManager;//Para poderse comunicar con el GameManager
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        
        //Capturamos el script GameManager
        gameManager = FindObjectOfType<GameManager>();
        
        //Nada más instanciarse les damos una fuerza hacia arriba y un torque aleatorios
        //Y los colocamos también en una posición aleatoria
        
        transform.position = RandomPosition();
        _rigidbody.AddForce(RandomForce(), ForceMode.Impulse);
        _rigidbody.AddTorque(RandomTorque(), RandomTorque(), RandomTorque());

    }
    private void OnMouseDown()//Si el jugador presiona el botón del ratón sobre el target
    {
        Destroy(gameObject);//Destruimos el target
        Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);//Se invoca un efecto
        gameManager.UpdateScore(pointValue);//Actualizamos la puntuación
    }

    private void OnTriggerEnter(Collider other)//Si el target cae en la zona de muerte
    {
        if (other.gameObject.CompareTag("KillZone"))
        {
            Destroy(gameObject);//Se destruye
            //Y hay una penalización en la puntuación (excepto en aquellos target que ya tenían una puntuación negativa)
            if (pointValue > 0)
            {
                gameManager.UpdateScore(penalizeFall);
            }
        }
    }

    /// <summary> Genera un Vector3 aleatorio en el eje X y con una posición fija en el Y </summary>
    /// <returns>Vector3 aleatorio en el eje X y con una posición fija en ySpawnPos</returns>
    private Vector3 RandomPosition()
    {
        return new Vector3(Random.Range(-positionX, positionX), ySpawnPos, 0);
    }
    
    /// <summary> Genera un Vector3 aleatorio hacia arriba </summary>
    /// <returns>Vector3 aleatorio hacia arriba con una fuerza entre minForce y maxForce</returns>
    private Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minForce, maxForce);
    }

    /// <summary> Genera un número aleatorio de torque en el rango establecido </summary>
    /// <returns>Número aleatorio de torque en el rango -rangeOfToque/rangeOfTorque</returns>
    private float RandomTorque()
    {
        return Random.Range(-rangeOfTorque, rangeOfTorque);
    }
 
}

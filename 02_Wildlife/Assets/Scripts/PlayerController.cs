using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float horizontalInput;
    public float speed = 10.0f;

    public float xRange = 15.0f;//Rango de movimiento del personaje en el eje X
    
    public GameObject projectilePrefab;//El prefab que representará el proyectil que puede lanzar
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //El personaje se moverá de derecha a izquierda
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(speed * Time.deltaTime * Vector3.right * horizontalInput);

        if (transform.position.x < -xRange)//Evitaremos que se salga de los límites de la pantalla
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        if (transform.position.x > xRange)
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        
        
        //Acciones del personaje
        if (Input.GetKeyDown(KeyCode.Space))//Cuando se presione Espacio
        {
            //Lanzar un proyectil
            Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
        }
    }
    
    
}

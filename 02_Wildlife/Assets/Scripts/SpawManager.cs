using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//MANAGER QUE INSTANCIARÁ ANIMALES ENEMIGOS DE FORMA ALEATORIA
public class SpawManager : MonoBehaviour
{
    public GameObject[] enemies;//Será un array, ya que puede haber un número variado de enemigos
    private int animalIndex;//Índice del enemigo que se va a spawnear

    private float spawnRangeX = 14.0f;//Rango de spawneo de los enemigos en el eje X
    private float spawnPosZ;//Posición en Z donde spawneará cada enemigo

    [SerializeField, Range(2f, 5f)]
    private float startDelay = 2.0f;//Tiempo hasta que comiencen a spawnear enemigos
    [SerializeField, Range(1f, 3f)]
    private float spawnInterval = 1.5f;//Tiempo de repetición de cada spawneo de enemigos
    
    // Start is called before the first frame update
    void Start()
    {
        spawnPosZ = transform.position.z;//El animal spawneará en Z en la misma posición que el SpawnManager (que en diseño lo hemos puesto por la parte de arriba)
        
        InvokeRepeating("SpawnRandomAnimal",startDelay,spawnInterval);
    }



    private void SpawnRandomAnimal()
    {
        float xRand = Random.Range(-spawnRangeX, spawnRangeX);//Posición aleatoria del nuevo enemigo
        animalIndex = Random.Range(0, enemies.Length);//Enemigo aleatorio dentro de los posibles
                
        //Posición donde aparecerá el próximo enemigo (en x será aleatorio dentro del rango permitido)
        Vector3 spawnPos = new Vector3(xRand, 0, spawnPosZ);

        Instantiate(enemies[animalIndex], spawnPos, 
            enemies[animalIndex].transform.rotation); //con la rotación que le hemos aplicado en diseño
    }
}

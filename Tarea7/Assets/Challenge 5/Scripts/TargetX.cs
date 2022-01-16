using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetX : MonoBehaviour
{
    private GameManagerX gameManagerX;
    public int pointValue;
    public GameObject explosionFx;

    public float timeOnScreen = 1.0f;

    private int spawnX, spawnY;//Celda en X,Y en las que spawnea el target

    void Start()
    {
        //Capturamos el script GameManagerX
        gameManagerX = GameObject.Find("Game Manager").GetComponent<GameManagerX>();
        
        //Guardamos la celda en la que ha spawneado el target, para liberarla tras su destrucción
        spawnX = gameManagerX.SquareX;
        spawnY = gameManagerX.SquareY;

        StartCoroutine(RemoveObjectRoutine()); // begin timer before target leaves screen

    }

    // When target is clicked, destroy it, update score, and generate explosion
    private void OnMouseEnter()
    {
        if (gameManagerX.isGameActive)
        {
            gameManagerX.UpdateBoardGame(0, spawnX, spawnY);//Libera la celda que ocupaba
            Destroy(gameObject);
            gameManagerX.UpdateScore(pointValue);
            Explode();
        }
               
    }

    // If target that is NOT the bad object collides with sensor, trigger game over
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);

        if (other.gameObject.CompareTag("Sensor") && !gameObject.CompareTag("Bad"))
        {
            gameManagerX.GameOver();
        } 

    }

    // Display explosion particle at object's position
    void Explode ()
    {
        Instantiate(explosionFx, transform.position, explosionFx.transform.rotation);
    }

    // After a delay, Moves the object behind background so it collides with the Sensor object
    IEnumerator RemoveObjectRoutine ()
    {
        yield return new WaitForSeconds(timeOnScreen);
        if (gameManagerX.isGameActive)
        {
            transform.Translate(Vector3.forward * 5, Space.World);
        }

    }

}

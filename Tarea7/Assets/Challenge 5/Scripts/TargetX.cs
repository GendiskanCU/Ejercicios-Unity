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

    private GameObject cavasHitScore;//Muestra un cuadro de texto con la puntuación otorgada por el target

    void Start()
    {
        //Capturamos el script GameManagerX
        gameManagerX = GameObject.Find("Game Manager").GetComponent<GameManagerX>();

        cavasHitScore = GameObject.Find("Canvas Hit Score");
        
        //Guardamos la celda en la que ha spawneado el target, para liberarla tras su destrucción
        spawnX = gameManagerX.SquareX;
        spawnY = gameManagerX.SquareY;

        StartCoroutine(RemoveObjectRoutine()); // begin timer before target leaves screen
        StartCoroutine(DecreasePointValue()); //disminuirá la puntuación con el paso del tiempo
    }

    // When target is clicked, destroy it, update score, and generate explosion
    private void OnMouseDown()
    {
        if (gameManagerX.isGameActive)
        {
            ShowScore();
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
        if(gameObject.CompareTag("Bad"))//Si se trata de un target "malo"
        {
            gameManagerX.UpdateBoardGame(0, spawnX, spawnY);//Libera la celda que ocupaba
        }

    }

    //Disminuye el valor del pointValue conforme el target permanece en la escena
    IEnumerator DecreasePointValue()
    {
        while (gameManagerX.isGameActive)
        {
            yield return new WaitForSeconds(timeOnScreen/5);//Cada 5ª parte de tiempo que permanece en escena
            //Se descontará 1 al valor, aunque nunca será menor de 1
            if (pointValue > 1)//Y no afectará a los target que ya tengan puntuación negativa
            {
                pointValue -= 1;
            }
        }
    }

    //Muestra un cuadro de texto con la puntuación otorgada por el target
    private void ShowScore()
    {
        cavasHitScore.transform.position = transform.position;//Colocamos el cuadro en la posición del target
        cavasHitScore.GetComponent<ShowHitScore>().ShowHScore(pointValue);//Llamamos al método que muestra los puntos
    }
}

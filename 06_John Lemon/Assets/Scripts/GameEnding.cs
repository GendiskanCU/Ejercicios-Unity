using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnding : MonoBehaviour
{
    public float fadeDuration = 1.0f;//Duración del "fading" sobre la imagen del canvas
    public float displayImageDuration = 1.0f;//Duración de la imagen una vez mostrada totalmente

    public GameObject player;//El player

    public CanvasGroup exitBackgroundImageCanvasGroup;//CanvasGroup de la imagen de salida en el canvas

    private bool isPlayerAtExit;//Está el player en la salida?

    private float timer;//Contador interno de tiempo
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            isPlayerAtExit = true;//El player está ya en la salida
        }
    }

    private void Update()
    {
        if (isPlayerAtExit)//Mientras el player esté en la salida
        {
            timer += Time.deltaTime;//Aumentamos el contador

            exitBackgroundImageCanvasGroup.alpha =
                Mathf.Clamp(timer/fadeDuration, 0, 1);//Mostramos progresivamente la imagen del canvas

            if (timer > fadeDuration + displayImageDuration) //Cuando se supere el tiempo de mostrar la imagen
            {
                EndLevel();    
            }
        }
    }

    /// <summary>Cerrará el juego</summary>
    private void EndLevel()
    {
        Debug.Log("Fin del juego");
        Application.Quit();
    }
}

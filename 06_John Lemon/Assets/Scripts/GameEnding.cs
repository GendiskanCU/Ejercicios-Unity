using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
    public float fadeDuration = 1.0f;//Duración del "fading" sobre la imagen del canvas
    public float displayImageDuration = 1.0f;//Duración de la imagen una vez mostrada totalmente

    public GameObject player;//El player

    public CanvasGroup exitBackgroundImageCanvasGroup;//CanvasGroup de la imagen de salida del nivel en el canvas

    public CanvasGroup caugthBackgroundImageCanvasGroup;//CanvasGroup de la imagen de ser cazado por enemigos

    private bool isPlayerAtExit;//Está el player en la salida?

    private bool isPlayercaugth;//Han cazado al player?

    private float timer;//Contador interno de tiempo
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            isPlayerAtExit = true;//El player está en la salida
        }
    }

    private void Update()
    {
        if (isPlayerAtExit)//Cuando el player esté en la salida se mostrará la imagen de victoria y termina el juego
        {
            EndLevel(exitBackgroundImageCanvasGroup, false);
        }
        else if(isPlayercaugth)//Si han cazado al player se mostrará la imagen de derrota y reinicia el juego
        {
            EndLevel(caugthBackgroundImageCanvasGroup, true);
        }
    }

    /// <summary>Muestra la imagen de victoria o derrota y cierra o reinicia el juego</summary>
    /// <param name="imageCanvasGroup">CanvasGroup que contiene la imagen a mostrar</param>
    /// <param name="doRestart">true para reiniciar la escena, false para salir del juego</param>
    private void EndLevel(CanvasGroup imageCanvasGroup, bool doRestart)
    {
        
        timer += Time.deltaTime;//Aumentamos el contador

        imageCanvasGroup.alpha =
            Mathf.Clamp(timer/fadeDuration, 0, 1);//Mostramos progresivamente la imagen del canvas

        if (timer > fadeDuration + displayImageDuration) //Cuando se supere el tiempo de mostrar la imagen
        {
            if (!doRestart)
            {
                Debug.Log("Fin del juego");
                Application.Quit();
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        
    }

    /// <summary> Indicará a GameEnding que el player ha sido cazado por un enemigo </summary>
    public void CatchPlayer()
    {
        isPlayercaugth = true;
    }
}

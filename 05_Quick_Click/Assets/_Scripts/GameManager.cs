using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;//Para utilizar los TextMeshPro
using UnityEngine.UI;//Para utilizar los Button de la UI
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random; //Para manejar las escenas

public class GameManager : MonoBehaviour
{
    //Creamos un enumerado para controlar el estado del juego en cada momento
    public enum GameState
    {
        loading,
        inGame,
        paused,
        gameOver
    }

    public GameState gameState;
    
    //Creamos una lista de objetos que se pueden instanciar
    [SerializeField] private List<GameObject> targetPrefabs;

    private float spawnRate = 2.0f;//Tiempo de spawneo de nuevos target

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private Button restartButton;
    [SerializeField] private GameObject titleScreen;//Pantalla de título y selección de dificultad

    private int score;//Puntuación

    private int numberOfLives = 4;//Número de vidas

    public List<GameObject> lives;//Sprites que muestran el número de vidas que nos quedan
    
    private int Score//Variable autocomputada. Evitaremos que el score sea en ningún momento menor de cero
    {
        set { score = Mathf.Clamp(value, 0, 99999); } //score se mantendrá entre 0 y 99999
        get { return score; }
    }


    private void Start()
    {
        //Se nos muestra la máxima puntuación
        ShowMaxScore();
    }

    /// <summary> Instancia un targetPrefab cada spawnRate segundos </summary>
    IEnumerator SpawnTarget()
    {
        while (gameState == GameState.inGame) //Mientras el estado sea "jugando"
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targetPrefabs.Count);
            Instantiate(targetPrefabs[index]);
        }
    }

    /// <summary> Inicia un juego </summary><param name="difficulty">Nivel de dificultad del nuevo juego</param>
    public void StartGame(int difficulty)
    {
        //Ocultamos el panel del título
        titleScreen.gameObject.SetActive(false);

        //Estado actual del juego: jugando
        gameState = GameState.inGame;
        
        //Establecemos el tiempo de spawneo en función de la dificultad elegida
        spawnRate /= difficulty;
        
        //Establecemos el número de vidas en función de la dificultad elegida
        numberOfLives -= difficulty;
        
        //Activamos en la UI el número de iconos correspondiente a las vidas que se tienen
        for (int i = 0; i < numberOfLives; i++)
        {
            lives[i].gameObject.SetActive(true);
        }
        
        //Iniciamos la corutina de spawneo
        StartCoroutine("SpawnTarget");
        
        //Iniciamos la puntuación y la mostramos en la UI
        Score = 0;
        UpdateScore(0);
    }

    /// <summary> Actualiza la puntuación y la muestra en la UI </summary>
    /// <param name="scoreToAdd">Cantidad entera a incrementar/decrementar</param>
    public void UpdateScore(int scoreToAdd)
    {
        Score += scoreToAdd;
        scoreText.text = "SCORE:\n\n" + score;
    }

    /// <summary> Activa el mensaje de GameOver </summary>
    public void GameOver()
    {
        numberOfLives--;//Reduce el número de vidas

        if (numberOfLives >= 0)
        {
            //Apagamos el color del icono que representa la vida que se acaba de perder
            Image livesImage = lives[numberOfLives].GetComponent<Image>(); //Capturamos la imagen del icono
            var tempColor = livesImage.color; //Guardamos su color en una variable temporal
            tempColor.a = 0.3f; //Le bajamos la transparencia al 30%
            livesImage.color = tempColor; //Y la aplicamos al color del icono
        }

        Debug.Log("Vidas restantes: " + numberOfLives);

        if (numberOfLives <= 0)//Cuando se hayan agotado las vidas
        {
            gameState = GameState.gameOver; //Se cambia el estado de la partida
            gameOverText.gameObject.SetActive(true); //Se muestra el mensaje de Game Over
            restartButton.gameObject.SetActive(true); //Se muestra el botón de restablecer
            SetMaxScore(); //Comprueba la máxima puntuación
        }
    }

    /// <summary> Reinicia la escena actual </summary>
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    
    /// <summary> Carga la máxima puntuación en el sistema y la muestra </summary>
    public void ShowMaxScore()
    {
        int maxScore = PlayerPrefs.GetInt("MAX_SCORE", 0);
        scoreText.text = "Max. Score: \n\n" + maxScore;
    }

    /// <summary> Comprueba si se ha superado la máxima puntuación y la guarda en caso afirmativo </summary>
    private void SetMaxScore()
    {
        if(score > PlayerPrefs.GetInt("MAX_SCORE", 0))
        {
            PlayerPrefs.SetInt("MAX_SCORE", score);
        }
    }
}

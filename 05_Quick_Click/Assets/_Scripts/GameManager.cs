using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;//Para utilizar los TextMeshPro
using UnityEngine.UI;//Para utilizar los Button de la UI
using UnityEngine.SceneManagement;//Para manejar las escenas

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

    [SerializeField] private float spawnRate = 1.0f;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private Button restartButton;

    private int score;
    private int Score//Variable autocomputada. Evitaremos que el score sea en ningún momento menor de cero
    {
        set { score = Mathf.Clamp(value, 0, 99999); } //score se mantendrá entre 0 y 99999
        get { return score; }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        //Estado actual del juego: jugando
        gameState = GameState.inGame;
        
        //Iniciamos la corutina de spawneo
        StartCoroutine("SpawnTarget");
        
        //Iniciamos la puntuación y la mostramos en la UI
        Score = 0;
        UpdateScore(0);
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

    /// <summary> Actualiza la puntuación y la muestra en la UI </summary>
    /// <param name="scoreToAdd">Cantidad entera a incrementar/decrementar</param>
    public void UpdateScore(int scoreToAdd)
    {
        Score += scoreToAdd;
        scoreText.text = "PUNTUACION:\n\n" + score;
    }

    /// <summary> Activa el mensaje de GameOver </summary>
    public void GameOver()
    {
        gameState = GameState.gameOver; //Se cambia el estado de la partida
        gameOverText.gameObject.SetActive(true);//Se muestra el mensaje de Game Over
        restartButton.gameObject.SetActive(true);//Se muestra el botón de restablecer
    }

    /// <summary> Reinicia la escena actual </summary>
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    
}

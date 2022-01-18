using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManagerX : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public GameObject gameOverPanel;
    public GameObject titlePanel;
    public GameObject countdownPanel;
    public GameObject followMouse;
    public Slider sliderCountDown;//Representará el tiempo restante de partida
    private AudioSource ambientalMusic;

    public List<GameObject> targetPrefabs;
    
    private int _score;
    public int Score
    {
        set { _score = Mathf.Clamp(value, 0, 999999); }
        get { return _score; }
    }
    
    private float spawnRate = 1.5f;
    public bool isGameActive;

    private int gameTime = 60;//Segundos de duración de cada partida
    

    private float spaceBetweenSquares = 2.5f; 
    private float minValueX = -3.75f; //  x value of the center of the left-most square
    private float minValueY = -3.75f; //  y value of the center of the bottom-most square

    private int[,] boardGame = new int[4, 4]; //El tablero de juego 

    private int _squareX;//Posición X de la celda en la que se instancia el último target
    public int SquareX
    {
        get { return _squareX; }
    }
    
    private int _squareY;//Posición Y de la celda en la que se instancia el último target
    public int SquareY
    {
        get { return _squareY; }
    }


    private void Start()
    {
        ambientalMusic = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        ClearBoardGame();
    }


    // Start the game, remove title screen, reset score, and adjust spawnRate based on difficulty button clicked
    public void StartGame(int difficulty)
    {
        spawnRate /= difficulty;
        isGameActive = true;
        Score = 0;
        UpdateScore(0);
        titlePanel.SetActive(false);
        scoreText.gameObject.SetActive(true);
        countdownPanel.SetActive(true);
        Cursor.visible = false;
        followMouse.SetActive(true);
        UpdateScore(0);
        ambientalMusic.Play();
        StartCoroutine(SpawnTarget());
        StartCoroutine(UpdateSlideCountDown());
    }

    // While game is active spawn a random target
    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targetPrefabs.Count);

            if (isGameActive)
            {
                Instantiate(targetPrefabs[index], RandomSpawnPosition(), targetPrefabs[index].transform.rotation);
            }
            
        }
    }

    // Generate a random spawn position based on a random index from 0 to 3
    Vector3 RandomSpawnPosition()
    {
        bool freeSquareFound = false;
        
        _squareX = RandomSquareIndex();
        _squareY = RandomSquareIndex();
        
        //Generará una celda nueva hasta encontrar una libre
        while (!freeSquareFound)
        {
            if (boardGame[_squareY, _squareX] == 0)
            {
                freeSquareFound = true;
            }
            else
            {
                _squareX = RandomSquareIndex();
                _squareY = RandomSquareIndex();
            }

        }
        
        float spawnPosX = minValueX + (_squareX * spaceBetweenSquares);
        float spawnPosY = minValueY + (_squareY * spaceBetweenSquares);

        Vector3 spawnPosition = new Vector3(spawnPosX, spawnPosY, 0);
        
        //Marca la celda del tablero como ocupada
        UpdateBoardGame(1, _squareX, _squareY);
        
        return spawnPosition;

    }

    // Generates random square index from 0 to 3, which determines which square the target will appear in
    int RandomSquareIndex()
    {
        return Random.Range(0, 4);
    }

    // Update score with value from target clicked
    public void UpdateScore(int scoreToAdd)
    {
        Score += scoreToAdd;
        scoreText.text = "Score:\n" + Score;
    }

    // Stop game, bring up game over text and restart button
    public void GameOver()
    {
        followMouse.SetActive(false);
        Cursor.visible = true;
        gameOverPanel.gameObject.SetActive(true);
        isGameActive = false;
        ambientalMusic.Stop();
    }

    // Restart game by reloading the scene
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //Pone a 0 el tablero de juego (todas las celdas están libres)
    private void ClearBoardGame()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                boardGame[i, j] = 0;
            }
        }
    }
    
    /// <summary> Actualiza el tablero de juego </summary>
    /// <param name="action">0 liberar celda ó 1 ocupar celda</param>
    /// <param name="coordX">Coordenada X de la celda</param>
    /// <param name="coordY">Coordenada Y de la celda</param>
    public void UpdateBoardGame(int action, int coordX, int coordY)
    {
        boardGame[coordY, coordX] = action;
        //Debug.Log("Coordenadas: (" + coordY + "," + coordX + ")-> " + boardGame[coordY, coordX]);
    }

    IEnumerator UpdateSlideCountDown()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(1.0f);
            gameTime--;
            sliderCountDown.value = gameTime;
            if (gameTime <= 0)
            {
                GameOver();
            }

        }
    }

}

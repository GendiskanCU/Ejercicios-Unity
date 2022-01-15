using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//Para acceder a los Button

public class DifficultyButton : MonoBehaviour
{
    private GameManager gameManager;

    [Range(1, 3)] public int difficulty = 1;//Cada botón tendrá un valor de dificultad

    private Button button;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();//Capturamos el GameManager
        
        button = GetComponent<Button>();//Capturamos el propio botón
        button.onClick.AddListener(SetDifficulty);//Llamamos el método que establece la dificultad
    }

    /// <summary> Inicia la partida con la dificultad asociada al botón </summary>
    private void SetDifficulty()
    {
        Debug.Log("El botón " + button.name + " ha sido pulsado");
        
        gameManager.StartGame(difficulty);
    }
}

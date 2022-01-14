using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    //Creamos una lista de objetos que se pueden instanciar
    [SerializeField] private List<GameObject> targetPrefabs;

    [SerializeField] private float spawnRate = 1.0f;

    [SerializeField] private TextMeshProUGUI scoreText;

    private int score;
    private int Score//Variable autocomputada. Evitaremos que el score sea en ningún momento menor de cero
    {
        set { score = Mathf.Clamp(value, 0, 99999); } //score se mantendrá entre 0 y 99999
        get { return score; }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        //Iniciamos la corutina de spawneo
        StartCoroutine("SpawnTarget");
        
        //Iniciamos la puntuación y la mostramos en la UI
        Score = 0;
        UpdateScore(0);
    }


    /// <summary> Instancia un targetPrefab cada spawnRate segundos </summary>
    IEnumerator SpawnTarget()
    {
        while (true) //De manera indefinida
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
}

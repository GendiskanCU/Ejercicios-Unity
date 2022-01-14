using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver;

    public float floatForce;
    private float gravityModifier = 1.5f;
    private Rigidbody playerRb;

    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    private AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;
    public AudioClip boundSound;

    public float upperLimit = 14.5f, lowerLimit = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();

        // Apply a small upward force at the start of the game
        playerRb = GetComponent<Rigidbody>();
        playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);

    }

    // Update is called once per frame
    void Update()
    {
        // While space is pressed
        if (Input.GetKey(KeyCode.Space))
        {
            if (!gameOver &&
                transform.position.y < upperLimit) //Si no ha finalizado el juego ni se ha llegado ya a la parte superior
            {
                playerRb.AddForce(Vector3.up * floatForce); //Flotará
            }
        }
      
        //Si se alcanza la parte superior de la escena no se permitirá ascender más
        if (transform.position.y > upperLimit)
            transform.position = new Vector3(transform.position.x, upperLimit, transform.position.z);

        //Si se alcanza la parte inferior de la escena no se permitirá descender más y rebotará ligeramente
        if (transform.position.y < lowerLimit & !gameOver)
        {
            playerAudio.PlayOneShot(boundSound, 0.3f);
            transform.position = new Vector3(transform.position.x, lowerLimit + 0.1f, transform.position.z);
            playerRb.AddForce(Vector3.up * 2f, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject);
            transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
            Invoke("RestartScene", 3.5f);
        } 

        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject);
        }
    }

    private void RestartScene()
    {
        Physics.gravity /= gravityModifier;//Se restaura el valor inicial de la gravedad
        SceneManager.LoadSceneAsync("Challenge 3");//Se recarga la escena
    }

}

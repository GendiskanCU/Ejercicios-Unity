using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyX : MonoBehaviour
{
    public float movementForce = 0.5f;
    private Rigidbody enemyRb;
    private GameObject enemyGoal;

    private SpawnManagerX spawn;//Para capturar el spawn y poder acceder al número de oleada actual

    private AudioSource _audioSource;
    [SerializeField] private AudioClip soundGoal;
    [SerializeField] private AudioClip soundDefeat;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        enemyGoal = GameObject.Find("Player Goal");
        _audioSource = GetComponent<AudioSource>();

        spawn = GameObject.Find("Spawn Manager").GetComponent<SpawnManagerX>();
        //La velocidad de movimiento aumentará proporcionalmente al número de oleada actual devuelto por el SpawnManager
        movementForce *= spawn.WaveCount / 2f;
    }

    // Update is called once per frame
    void Update()
    {
        // Set enemy direction towards player goal and move there
        Vector3 lookDirection = (enemyGoal.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * movementForce, ForceMode.Force);

    }

    private void OnCollisionEnter(Collision other)
    {
        // If enemy collides with either goal, destroy it
        if (other.gameObject.name == "Enemy Goal")
        {
            _audioSource.PlayOneShot(soundGoal);
            Invoke("DestroyEnemy", 0.2f);
        } 
        else if (other.gameObject.name == "Player Goal")
        {
            _audioSource.PlayOneShot(soundDefeat);
            Invoke("DestroyEnemy", 0.2f);
        }
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

}

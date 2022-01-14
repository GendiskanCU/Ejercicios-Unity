using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    private Rigidbody playerRb;
    private float speed = 500;
    private GameObject focalPoint;

    public bool hasPowerup;
    public GameObject powerupIndicator;
    public GameObject impulseIndicatorPrefab;
    public int powerUpDuration = 6;

    public float normalStrength = 10; // how hard to hit enemy without powerup
    public float powerupStrength = 40; // how hard to hit enemy with powerup

    private bool impulseOn;//El player está utilizando la habilidad del impulso
    public float impulseForce = 0.1f;
    public int secondsForNewImpulse = 1;

    private AudioSource _audioSource;
    public AudioClip softKickSound, hardKickSound, impulseSound;
    [Range(0, 1)] public float volumeSoundEffects = 0.75f;
    
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Add force to player in direction of the focal point (and camera)
        float verticalInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * verticalInput * speed * Time.deltaTime); 

        // Set powerup indicator position to beneath player
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.6f, 0);

        if (Input.GetKeyDown(KeyCode.Space) && !impulseOn)
        {
            impulseOn = true;
            _audioSource.PlayOneShot(impulseSound, volumeSoundEffects);
            playerRb.AddForce(focalPoint.transform.forward.normalized * impulseForce, ForceMode.Impulse);
            Instantiate(impulseIndicatorPrefab, transform.position + new Vector3(-0.2f, -1f, -0.5f),
                impulseIndicatorPrefab.transform.rotation);
            StartCoroutine(WaitForNewImpulse());//Hay que esperar un tiempo antes de un nuevo impulso
        }

    }

    // If Player collides with powerup, activate powerup
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            hasPowerup = true;
            powerupIndicator.SetActive(true);
            StartCoroutine(PowerupCooldown());
        }
    }

    // Coroutine to count down powerup duration
    IEnumerator PowerupCooldown()
    {
        yield return new WaitForSeconds(powerUpDuration);
        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }
    
    IEnumerator WaitForNewImpulse()
    {
        yield return new WaitForSeconds(secondsForNewImpulse);
        impulseOn = false;
    }

    // If Player collides with enemy
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = other.gameObject.transform.position - transform.position; 
           
            if (hasPowerup) // if have powerup hit enemy with powerup force
            {
                _audioSource.PlayOneShot(hardKickSound, volumeSoundEffects);
                enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            }
            else // if no powerup, hit enemy with normal strength 
            {
                _audioSource.PlayOneShot(softKickSound, volumeSoundEffects);
                enemyRigidbody.AddForce(awayFromPlayer * normalStrength, ForceMode.Impulse);
            }
        }
    }

}

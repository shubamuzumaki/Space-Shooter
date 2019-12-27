using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class Player : MonoBehaviour
{

    [Header("Player")]
    [SerializeField] float health = 5;
    [SerializeField] float speed = 10f;
    [SerializeField] [Range(0,1)] float maxYAllowed = 0.4f;
    [SerializeField] [Range(0,0.1f)] float padding = 0.05f;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] [Range(0,1)] float deathSFXVolume = 0.7f;

    [Header("Laser")]
    [SerializeField] GameObject playerLaser = null;
    [SerializeField] AudioClip laserSFX;
    [SerializeField] [Range(0,1)] float laserSFXVolume = 0.3f;
    [SerializeField] [Range(0,40f)] float laserSpeed = 20f;
    [SerializeField] [Range(0.1f,25f)] float fireRate = 2f;

    private float xMin;
    private float xMax;
    private float yMin;
    private float yMax;

    private Coroutine firingCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
    }

    void SetUpMoveBoundaries()
    {
        var gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0+padding, 0, 0)).x;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1-padding, 0, 0)).x;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0+padding, 0)).y;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, maxYAllowed, 0)).y;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        var deltaY = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newXPos, newYPos);
    }

    private void Fire()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FiringCoroutine());
        }
        else if(Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator FiringCoroutine()
    {
        while(true)
        {
            var laser = Instantiate(playerLaser, transform.position, Quaternion.identity);
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0,laserSpeed);
            AudioSource.PlayClipAtPoint(laserSFX, Camera.main.transform.position, laserSFXVolume);            
            yield return new WaitForSeconds(1/fireRate);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        var damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if(!damageDealer) return;
            ProcessHit(damageDealer);
    }
    
    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if(health <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(Vibrate(0.5f));
        }
    }

    public float GetHealth()
    {
        return health;
    }

    private void Die()
    {
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);
        FindObjectOfType<LevelManager>().LoadGameOverScene();
    }

    IEnumerator Vibrate(float duration)
    {
        GamePad.SetVibration(PlayerIndex.One,1,1);
        yield return new WaitForSeconds(duration);
        // GamePad.SetVibration(PlayerIndex.One,0,0);
    }
}

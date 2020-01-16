using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("Player")]
    [SerializeField] int health = 5;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] [Range(0,1)] float deathSFXVolume = 0.7f;
    [SerializeField] GameObject shield;

    [Header("Laser")]
    [SerializeField] GameObject playerLaser = null;
    [SerializeField] AudioClip laserSFX;
    [SerializeField] [Range(0,1)] float laserSFXVolume = 0.3f;
    [SerializeField] [Range(0,40f)] float laserSpeed = 20f;
    [SerializeField] [Range(0.1f,25f)] float fireRate = 2f;

    private Coroutine firingCoroutine;
    private float defaultFirerate;
    private bool isChargingPower = false;
    CameraShaker cameraShaker;

    // Start is called before the first frame update
    void Start()
    {
        defaultFirerate = fireRate;
        cameraShaker = FindObjectOfType<CameraShaker>();
    }

    void Update()
    {
        Fire();
    }

    private void Fire()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            if(firingCoroutine != null)
                StopCoroutine(firingCoroutine);
            firingCoroutine = StartCoroutine(FiringCoroutine());
        }
        else if(Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "playerElement")
            return;
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
            cameraShaker.ShakeCamera(1f,0.25f);
        }
        else
        {
            VibrateManager.GetInstance().Vibrate();
            cameraShaker.ShakeCamera(0.3f, 0.2f);
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);
        FindObjectOfType<LevelManager>().LoadGameOverScene();
    }
    
    public int GetHealth()
    {
        return health;
    }

    public void IncreaseHealth(int value)
    {
        health += value;
    }

    public float GetFirerate()
    {
        return fireRate;
    }

    public void SetFirerate(float value)
    {
        fireRate = value;
        Debug.Log("Fire Rate: " + fireRate );
    }

    public float GetDefaultFirerate()
    {
        return defaultFirerate;
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

    public void ActivateShield()
    {
        shield.SetActive(true);
    }

    public void DeActivateShield()
    {
        shield.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject enemyLaser;
    [SerializeField] GameObject explosionVFX;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] AudioClip laserSFX;
    
    [Header("Config Params")]
    [SerializeField] float health = 5f;
    [SerializeField] int scoreValue = 10;
    [SerializeField] float laserSpeed = 10f;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 2f;
    [SerializeField] float durationOfExplosion = 1f;
    [SerializeField] [Range(0,1)] float deathSFXVolume = 0.7f;
    [SerializeField] [Range(0,1)] float laserSFXVolume = 0.3f;

    [Header("Power Drops")]
    [SerializeField] [Range(0,100)] int probability = 20;
    [SerializeField] [Range(0,10)] float powerDropSpeed = 3.5f;

    //cached Params
    PowerDropManager powerDropper;

    private void Start() 
    {
        powerDropper = PowerDropManager.GetInstance();
        StartCoroutine(Fire());
    }

    IEnumerator Fire()
    {
        while(true)
        {
            var laser = Instantiate(enemyLaser, transform.position, Quaternion.identity);
            laser.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -laserSpeed);
            AudioSource.PlayClipAtPoint(laserSFX, Camera.main.transform.position, laserSFXVolume);

            yield return new WaitForSeconds(Random.Range(minTimeBetweenShots,maxTimeBetweenShots));
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        var damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if(!damageDealer) 
            return;
        
        ProcessHit(damageDealer);
    }
    
    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if(health <= 0)
            Die();
    }

    private void Die()
    {
        GameSession.GetInstance().EnemyDied(scoreValue);
        Destroy(gameObject);  
        var explosion = Instantiate(explosionVFX,transform.position,transform.rotation);
        Destroy(explosion, durationOfExplosion);
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);

        DropPowerUp();
    }

    private void DropPowerUp()
    {
        if(Random.Range(0,100) < probability)
        {
            if(powerDropper == null)
            {
                Debug.Log("No PowerDropper Found");
                return;
            }

            var power = powerDropper.GetPowerDrop();
            if(power == null)
                return;
            var drop = Instantiate(power,transform.position,Quaternion.identity);
            drop.GetComponent<Rigidbody2D>().velocity += Vector2.down * powerDropSpeed;
        }
    }
}

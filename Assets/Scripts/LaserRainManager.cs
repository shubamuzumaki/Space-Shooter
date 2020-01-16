using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRainManager : MonoBehaviour
{
    [SerializeField] GameObject screenCleaner;
    [SerializeField] float cleaningSpeed = 5f;
    List<Transform> topSpawnPoints = new List<Transform>();
    List<Transform> bottomSpawnPoints = new List<Transform>();
    
    void Start()
    {
        var topSpawn = transform.GetChild(0).gameObject;
        var bottomSpawn = transform.GetChild(1).gameObject;

        foreach (Transform child in topSpawn.transform)
            topSpawnPoints.Add(child);

        foreach (Transform child in bottomSpawn.transform)
            bottomSpawnPoints.Add(child);

    }

    public void ActivateRainHell()
    {
        StartCoroutine(HellRainTop());
        StartCoroutine(HellRainBottom());
    }

    IEnumerator HellRainBottom()
    {
        foreach (var pos in bottomSpawnPoints)
        {
            var cleaner = Instantiate(screenCleaner, pos.position, screenCleaner.transform.rotation);
            cleaner.GetComponent<Rigidbody2D>().velocity = Vector2.up * cleaningSpeed;
            yield return new WaitForSeconds(1);
        }
    }

    IEnumerator HellRainTop()
    {
        foreach (var pos in topSpawnPoints)
        {
            var cleaner = Instantiate(screenCleaner, pos.position, screenCleaner.transform.rotation);
            cleaner.GetComponent<Rigidbody2D>().velocity = Vector2.down * cleaningSpeed;
            yield return new WaitForSeconds(1);
        }
    }

}

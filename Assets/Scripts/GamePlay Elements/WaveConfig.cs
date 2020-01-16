using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] int enemyCount = 10;
    [SerializeField] float randomFactor = 0.5f;
    [SerializeField] float spawnRate = 5f;
    [SerializeField] float moveSpeed = 10f;

    public GameObject GetEnemyPrefab() { return enemyPrefab;}
    
    public List<Transform> GetWayPoints() 
    {
        var waveWaypoints = new List<Transform>();
        foreach (Transform child in pathPrefab.transform)
            waveWaypoints.Add(child);
        
        return waveWaypoints;
    }

    public int GetEnemyCount() { return enemyCount;}
    public float GetRandomFactor() { return randomFactor;}
    public float GetEnemySpawnRate() { return spawnRate;}
    public float GetEnemyMoveSpeed() { return moveSpeed;}
}

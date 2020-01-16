using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;

    IEnumerator Start() 
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
        while(looping);
    }

    IEnumerator SpawnAllWaves()
    {
        for(int i=startingWave; i<waveConfigs.Count; ++i)
            yield return StartCoroutine(WaveSpawner(waveConfigs[i]));
    }
    private IEnumerator WaveSpawner(WaveConfig wave)
    {
        var enemyCount = wave.GetEnemyCount();
        for(int i=0; i<enemyCount; ++i)
        {
            var newEnemy = Instantiate(wave.GetEnemyPrefab(),wave.GetWayPoints()[0].position,Quaternion.identity);
            newEnemy.GetComponent<EnemyMovement>().SetWaveConfig(wave);
            yield return new WaitForSeconds(wave.GetEnemySpawnRate());
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PowerDropManager : MonoBehaviour
{
    [SerializeField] List<GameObject> legendaryPowerUp;
    [SerializeField] List<GameObject> rarePowerUp;
    [SerializeField] List<GameObject> commonPowerUp;

    [SerializeField] [Range(0,50)] int legendaryProbab = 5;
    [SerializeField] [Range(0,50)] int rareProbab = 30;

    private static PowerDropManager instance;

    private void Awake() 
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);    
    }

    public static PowerDropManager GetInstance()
    {
        return instance;
    }

    public GameObject GetPowerDrop()
    {

        int rand = UnityEngine.Random.Range(0,Int32.MaxValue);
        int n = rand %100;
        int ind;
        if(n < legendaryProbab && legendaryPowerUp.Count > 0)
        {
            Debug.Log("Legendry Drop");
            ind = rand%legendaryPowerUp.Count;
            return legendaryPowerUp[ind];
        }
        else if(n < (legendaryProbab+ rareProbab) && rarePowerUp.Count > 0)
        {
            Debug.Log("Rare Drop");
            ind = rand%rarePowerUp.Count;
            return rarePowerUp[ind];
        }
        else if(commonPowerUp.Count > 0)
        {
            Debug.Log("Common Drop");
            ind = rand%commonPowerUp.Count;
            return commonPowerUp[ind];
        }
        return null;
    }
}

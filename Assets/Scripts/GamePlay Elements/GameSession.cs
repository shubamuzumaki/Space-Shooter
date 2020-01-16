using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//this is publisher
public class GameSession : MonoBehaviour
{
    private int score = 0;
    private event Action<int> OnScoreUpdate;
    private static GameSession instance = null;
       

    public void Awake() 
    {
        if(instance == null)
        {
            instance = this;
            //Debug.Log("GameSession Kept");
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            //Debug.Log("GameSession Destroyed in pieces");
            Destroy(gameObject);
        }
    }

    public static GameSession GetInstance()
    {
        return instance;
    }


    public void AddSubscriberOnScoreUpdate(Action<int> subscriberMethod)
    {
        OnScoreUpdate += subscriberMethod;
        UpdateScore(0);
    }
       
    public void UnSubscribeOnScoreUpdate(Action<int> subscriberMethod)
    {
        OnScoreUpdate -= subscriberMethod;
    }

    public void EnemyDied(int enemyValue)
    {
        UpdateScore(enemyValue);
    }

    public void Reset()
    {
        Debug.Log("Game Score Resetted");
        UpdateScore(-score);
    }

    private void UpdateScore(int incrValue)
    {
        score += incrValue;
        OnScoreUpdate ?. Invoke(score); // null-conditional operator or Elvis Operator for thread safety
    }

}

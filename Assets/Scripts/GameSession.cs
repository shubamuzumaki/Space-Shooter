using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField]private int score = 5;

    public void Awake() 
    {
        if(FindObjectsOfType(GetType()).Length > 1 )
        {
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("GameSession Kept");
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void AddToScore(int value)
    {
        score += value;
    }

    public void Reset()
    {
        score = 0;
    }

}

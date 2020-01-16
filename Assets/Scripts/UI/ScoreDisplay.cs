using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ScoreDisplay : MonoBehaviour
{
    GameSession gameSession;
    Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<Text>();
        GameSession.GetInstance().AddSubscriberOnScoreUpdate(OnEnemyDeath);
    }

    public void OnEnemyDeath(int scoreUpdateValue)
    {
        scoreText.text = scoreUpdateValue.ToString();
    }

    private void OnDestroy()
    {
        GameSession.GetInstance().UnSubscribeOnScoreUpdate(OnEnemyDeath);
    }
}

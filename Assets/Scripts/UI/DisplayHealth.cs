﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHealth : MonoBehaviour
{
    Player player;
    Text playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        playerHealth = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        int health = player.GetHealth();
        if(health <= 0)
            playerHealth.text = "DEAD";
        else
            playerHealth.text = health.ToString();
    }
}

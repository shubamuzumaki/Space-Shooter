using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatHealth :CheatCode
{
    [SerializeField] int healthBoost = 50;
    override
    public void OnCheatActivation()
    {
        var player = FindObjectOfType<Player>();
        if (player != null)
        {
            player.IncreaseHealth(healthBoost);
        }
    }
}

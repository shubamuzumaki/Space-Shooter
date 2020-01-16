using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpHealthBoost : PowerUp
{
    [SerializeField] int healthBuff = 5;

    override
    public void ActivatePowerUp()
    {
        var player = powerController.gameObject.GetComponent<Player>();
        if(player == null)
            return;
        
        player.IncreaseHealth(healthBuff);
    }

    override
    public void DeActivatePowerUp()
    {
        // var player = powerController.gameObject.GetComponent<Player>();
        // if(player == null)
        //     return;
        
        // player.SetFirerate(player.GetDefaultFirerate());
        // Debug.Log("Fire Rate Multiplier Deactivated:");

    }
}

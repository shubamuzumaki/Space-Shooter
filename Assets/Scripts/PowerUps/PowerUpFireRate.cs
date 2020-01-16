using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpFireRate : PowerUp
{
    override
    public void ActivatePowerUp()
    {
        var player = powerController.gameObject.GetComponent<Player>();
        if(player == null)
            return;
        
        player.SetFirerate(player.GetFirerate()*2);

        // Debug.Log("Fire rate Multiplier Activated: ");
    }

    override
    public void DeActivatePowerUp()
    {
        var player = powerController.gameObject.GetComponent<Player>();
        if(player == null)
            return;
        
        player.SetFirerate(player.GetDefaultFirerate());
        // Debug.Log("Fire Rate Multiplier Deactivated:");

    }
}

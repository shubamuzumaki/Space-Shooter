using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpShield : PowerUp
{
    [SerializeField] float deactivateShieldTime = 1f;

    override
    public  void ActivatePowerUp()
    {
        var player = powerController.GetComponent<Player>();
        if(player != null)
            player.ActivateShield();
    }

    override
    public void DeActivatePowerUp()
    {
        //var player = powerController.GetComponent<Player>();
        //if (player != null)
        //{
        //    StartCoroutine(DeactivatingShieldCoroutine(player));
        //}
    }

    public override void DeActivateAndDestroy()
    {
        var player = powerController.GetComponent<Player>();
        if (player != null)
        {
            StartCoroutine(DeactivatingShieldCoroutine(player));
        }
    }

    IEnumerator DeactivatingShieldCoroutine(Player player)
    {
        float curTime = 0;
        while(curTime < deactivateShieldTime)
        {
            player.DeActivateShield();
            yield return new WaitForSeconds(0.1f);
            player.ActivateShield();
            yield return new WaitForSeconds(0.1f);
            curTime += 0.1f;
        }
        player.DeActivateShield();
        DestroyPowerUp();
    }
}

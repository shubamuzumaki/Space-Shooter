using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpTimeFreezer : PowerUp
{

    [SerializeField] float slowDownFactor = 0.05f;

    override
    public void ActivatePowerUp()
    {

        Debug.Log("Activated Time Freeze" + Time.fixedDeltaTime);
        Time.timeScale = slowDownFactor;
        Time.fixedDeltaTime = Time.fixedDeltaTime * Time.timeScale;
        
        //var player = powerController.GetComponent<Player>();
        //if(player != null)
        //    player.ActivateFalconMode(1/slowDownFactor);
    }

    override
    public void DeActivatePowerUp()
    {
        Debug.Log("Deactivate Time Freeze");
        //var player = powerController.GetComponent<Player>();
        //if(player != null)
        //    player.DeActivateFalconMode(1/slowDownFactor);

        Time.fixedDeltaTime = Time.fixedDeltaTime/Time.timeScale;
        Time.timeScale = 1;
    }

    override
    public float GetDurationOfEffect()
    {
        return durationOfEffect*slowDownFactor;
    }

    private void HelperActivater()
    {
       
    }
}

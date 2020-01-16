using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatLaserRain : CheatCode
{
    [SerializeField] LaserRainManager laserRainManager;

    override
    public void OnCheatActivation()
    {
        Debug.Log("Laser Rain activated");
        laserRainManager.ActivateRainHell();
    }
}

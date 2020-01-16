using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrateManager
{
    private static VibrateManager instance;

    private VibrateManager()
    {

    }

    public static VibrateManager GetInstance()
    {
        if (instance == null)
            instance = new VibrateManager();
        return instance;
    }

    public void Vibrate()
    {

    }

    //IEnumerator Vibrate(float duration)
    //{
    //    GamePad.SetVibration(PlayerIndex.One, 0.5f, 0.5f);
    //    Handheld.Vibrate();
    //    yield return new WaitForSeconds(duration);
    //    GamePad.SetVibration(PlayerIndex.One, 0, 0);
    //}
}

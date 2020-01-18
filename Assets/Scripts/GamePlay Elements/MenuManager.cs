using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    [SerializeField] GameObject startMenuCanvas;
    [SerializeField] GameObject settingCanavas;
    [SerializeField] GameObject leaderboardCanvas;

    public void OpenSettings()
    {
        settingCanavas.SetActive(true);
        startMenuCanvas.SetActive(false);
    }

    public void openMainMenu()
    {
        startMenuCanvas.SetActive(true);
        settingCanavas.SetActive(false);
        leaderboardCanvas.SetActive(false);
    }

    public void OpenLeaderboard()
    {
        leaderboardCanvas.SetActive(true);
        startMenuCanvas.SetActive(false);
    }

}

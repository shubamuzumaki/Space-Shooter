using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XInputDotNetPure;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float delayInSeconds = 2f;

    public void LoadStartMenuScene()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGameOverScene()
    {
        StartCoroutine(WaitAndLoad());
    }

    public void LoadGame()
    {
        var gamesession = FindObjectOfType<GameSession>();
        if(gamesession)
            gamesession.Reset();

        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Debug.Log("Application Quitted");
        Application.Quit();
    }

    IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(delayInSeconds);
        GamePad.SetVibration(PlayerIndex.One,0,0);
        SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings -1);

    }
}

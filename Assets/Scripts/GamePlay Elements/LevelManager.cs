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
        Debug.Log("Loading starting scene");
        SceneManager.LoadScene(0);
    }

    public void LoadGameOverScene()
    {
        StartCoroutine(WaitAndLoad());
    }

    public void LoadGame()
    {
        Debug.Log("Loading Game scene");

        var gamesession = GameSession.GetInstance();
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
        SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings -1);
    }
}

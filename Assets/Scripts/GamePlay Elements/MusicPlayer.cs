using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    static private MusicPlayer instance = null;

    public void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static MusicPlayer GetInstance()
    {
        return instance;
    }
}

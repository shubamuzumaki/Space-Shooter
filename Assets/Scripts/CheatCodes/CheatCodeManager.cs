using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatCodeManager : MonoBehaviour
{
    private static CheatCodeManager instance = null;
    
    [SerializeField] private float delayTimePerStroke = 0.5f;
    
    private Trie trie = new Trie();
    private List<CheatCode> cheatCodeList = new List<CheatCode>();
    private Coroutine resetSearchCoroutine = null;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static CheatCodeManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
    }

    IEnumerator ResetSearchCoroutine()
    {
        yield return new WaitForSeconds(delayTimePerStroke);
        trie.Reset();
    }


    void Update()
    {
        LogKeyStroke(Input.inputString);
    }

    private void LogKeyStroke(string str)
    {
        if (str.Length == 0) return;

        if (resetSearchCoroutine != null)
            StopCoroutine(resetSearchCoroutine);

        str = str.ToLower();
        trie.Search(str[0]);

        resetSearchCoroutine = StartCoroutine(ResetSearchCoroutine());
    }

    public bool AddCheatCode(CheatCode cheatCode)
    {
        if(trie.Insert(cheatCode))
        {
            cheatCodeList.Add(cheatCode);
            return true;
        }
        return false;
    }

    public void RemoveCheatCode(CheatCode cheatCode)
    {
        if (cheatCode == null)
            return;

        cheatCodeList.Remove(cheatCode);
        trie.Remove(cheatCode);
    }
}


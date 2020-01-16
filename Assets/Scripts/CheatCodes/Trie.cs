using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trie
{ 
    class TrieNode
    {
        public Dictionary<char, TrieNode> map = new Dictionary<char, TrieNode>();
        public bool isEndOfWord;
        public CheatCode cheatCode;
    }


    private TrieNode head = new TrieNode();
    HashSet<TrieNode> set = new HashSet<TrieNode>();
    TrieNode curNode;

    public Trie()
    {
        set.Add(head);
    }

    public bool Insert(CheatCode cheatCode)
    {
        string str = cheatCode.GetCheatCode();
        int strLen = str.Length;

        if (strLen == 0)
            return false;

        str = str.ToLower();

        TrieNode curNode = head;

        for (int i = 0; i < strLen; ++i)
        {
            char ch = str[i];
            if (!curNode.map.ContainsKey(ch))
            {
                TrieNode temp = new TrieNode();
                curNode.map.Add(ch, temp);
                curNode = temp;
            }
            else
            {
                curNode = curNode.map[ch];
                if (curNode.isEndOfWord)
                    return false; 
            }
        }

        if (curNode.map.Count > 0)
            return false;

        curNode.isEndOfWord = true;
        curNode.cheatCode = cheatCode;
        Debug.Log("Cheat Added: " + cheatCode.GetCheatCode().ToUpper());
        return true;
    }
    
    public void Remove(CheatCode cheatCode)
    {
        if (cheatCode == null)
            return;

        string str = cheatCode.GetCheatCode();
        if (str.Equals(""))
            return;

        var curNode = head;

        for(int i=0; i<str.Length; ++i)
        {
            char ch = str[i];
            if (curNode.map.ContainsKey(ch))
            {
                if(i == str.Length-1)
                    break;
                curNode = curNode.map[ch];
            }
            else
                return;
        }

        curNode.map.Remove(str[str.Length-1]);
        Debug.Log("Cheat Removed: " + str.ToUpper());
    }

    public void Search(char ch)
    {
        TrieNode tempNode = null;
        HashSet<TrieNode> tempSet = new HashSet<TrieNode>();
        HashSet<TrieNode> toBeRemoved = new HashSet<TrieNode>();

        foreach(TrieNode tn in set)
        {
            if (tn.map.ContainsKey(ch))
            {
                tempNode = tn.map[ch];
                if (tempNode.isEndOfWord)
                {
                    tempNode.cheatCode.OnCheatActivation();
                    Reset();
                    return;
                }

                tempSet.Add(tempNode);
            }
            else if (tn != head)
                toBeRemoved.Add(tn);
        }

        set.UnionWith(tempSet);
        foreach (var tn in toBeRemoved)
            set.Remove(tn);
    }

    public void Reset()
    {
        set.Clear();
        set.Add(head);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CheatCode : MonoBehaviour
{
    [Header("Cheat Area")]
    [SerializeField] protected string cheatCode;
    [SerializeField] protected string cheatDescription;

    private bool isCheatRegistered = false;

    public virtual void Start()
    {
        isCheatRegistered =  CheatCodeManager.GetInstance().AddCheatCode(this);
        if(!isCheatRegistered)
        {
            Debug.Log(cheatCode + " :Registration Failed \n Prefix Match Error Please rename Cheatcode");
            Destroy(gameObject);
        }
    }

    public virtual void OnDestroy()
    {
        if(isCheatRegistered)
            CheatCodeManager.GetInstance().RemoveCheatCode(this);
    }

    public virtual string GetCheatCode()
    {
        return this.cheatCode;
    }

    public virtual string GetCheatDescription()
    {
        return this.cheatDescription;
    }

    public abstract void OnCheatActivation();
}

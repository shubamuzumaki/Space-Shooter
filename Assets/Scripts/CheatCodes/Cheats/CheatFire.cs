using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatFire : CheatCode
{
    [SerializeField] float durationOfEffect = 8;

    override
    public void OnCheatActivation()
    {
        var player = FindObjectOfType<Player>();
        if (player != null)
        {
            player.SetFirerate(player.GetDefaultFirerate() * 2.5f);
            StartCoroutine(DeactivateCheatDelay(player));
        }
    }


    IEnumerator DeactivateCheatDelay(Player player)
    {
        yield return new WaitForSeconds(durationOfEffect);
        player.SetFirerate(player.GetDefaultFirerate());
        Debug.Log(cheatCode + " Deactivated");
    }
}

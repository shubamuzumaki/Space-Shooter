using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerController : MonoBehaviour
{
    PowerUp power;
    Player player;
    bool isPowerActivated = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        HandlePowers();
    }

    public void addPower(PowerUp power)
    {
        if(this.power == null)
            this.power = power;
    }

    void HandlePowers()
    {
        var input = Input.GetAxis("Jump");
        if(input == 1)
        {
            ActivatePowerUp();
        }
    }

    public void ActivatePowerUp()
    {
        if (this.power != null && !isPowerActivated)
        {
            StartCoroutine(ActivatePowerUpCoroutine(this.power));
            isPowerActivated = true;
            this.power = null;
        }
    }

    IEnumerator ActivatePowerUpCoroutine(PowerUp power)
    {
        if(power == null) 
            yield return null;
        
        power.ActivatePowerUp();

        VibrateManager.GetInstance().Vibrate();
        yield return new WaitForSeconds(power.GetDurationOfEffect());
        DeActivatePowerUp(power);
    }

    private void DeActivatePowerUp(PowerUp power)
    {
        if(power == null)
            return;

        power.DeActivateAndDestroy();
        isPowerActivated = false;
    }
}

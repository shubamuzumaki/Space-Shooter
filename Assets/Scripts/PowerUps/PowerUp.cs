using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class  PowerUp : MonoBehaviour
{
    [SerializeField] protected string description;
    [SerializeField] protected float durationOfEffect;
    [SerializeField] bool isPermanent = false;
    [SerializeField] GameObject powerCollectedVFX;
    [SerializeField] float durationOfVFX = 2f;

    private SpriteRenderer spriteRenderer;
    private State currentState = State.INACTIVE;
    protected PowerController powerController;

    enum State
    {
        INACTIVE,//Can be picked up
        ACTIVE   //Picked up by someone
    }

    private void Start() 
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public string GetDescription()
    {
        return description;
    }

    public virtual float GetDurationOfEffect()
    {
        return durationOfEffect;
    }

   

    public abstract void ActivatePowerUp();

    public abstract void DeActivatePowerUp();

    public virtual void DeActivateAndDestroy()
    {
        DeActivatePowerUp();
        DestroyPowerUp();
    }

    public void DestroyPowerUp()
    {
        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D other) 
    {
        if(currentState != State.INACTIVE)
            return;

        powerController = other.GetComponent<PowerController>();
        if(powerController == null)
            return;

        var collectedVFX = Instantiate(powerCollectedVFX,transform.position,Quaternion.identity);
        Destroy(collectedVFX,durationOfVFX);

        if(isPermanent)
        {
            //Debug.Log("instant " + description);
            ActivatePowerUp();
            DestroyPowerUp();
        }

        powerController.addPower(this);

        spriteRenderer.enabled = false; //make it invisible

        currentState = State.ACTIVE;

        transform.position += new Vector3(-5000,-5000,-5000);
    }
}
    

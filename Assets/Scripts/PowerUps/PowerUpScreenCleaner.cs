using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScreenCleaner : PowerUp
{
    [SerializeField] GameObject screenCleaner;
    [SerializeField] float cleaningSpeed = 2;

    override
    public void ActivatePowerUp()
    {
        var cleaner = Instantiate(screenCleaner,powerController.transform.position,screenCleaner.transform.rotation);
        cleaner.GetComponent<Rigidbody2D>().velocity = Vector2.up * cleaningSpeed;
    }

    override
    public void DeActivatePowerUp()
    {

    }
}

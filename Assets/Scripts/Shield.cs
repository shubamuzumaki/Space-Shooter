using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "enemyElement")
        {
            Destroy(other.gameObject);
            // Debug.Log("Shield in work");
        }
        
    }
}

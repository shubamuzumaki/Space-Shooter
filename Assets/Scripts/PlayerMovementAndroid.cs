using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementAndroid : MonoBehaviour
{
    [SerializeField] [Range(0, 1)] float maxYAllowed = 0.4f;
    [SerializeField] [Range(0, 0.1f)] float padding = 0.05f;

    private Vector3 mouseStartPos;
    private Vector3 playerStartPos;

    private float xMin;
    private float xMax;
    private float yMin;
    private float yMax;



    void Start()
    {
        Debug.Log("Andoid Controls initialized");
        SetUpMoveBoundaries();
    }

    void SetUpMoveBoundaries()
    {
        var gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0 + padding, 0, 0)).x;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1 - padding, 0, 0)).x;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0 + padding, 0)).y;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, maxYAllowed, 0)).y;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        var mousePos = Input.mousePosition;

        if(Input.GetMouseButtonDown(0))
        {
            mouseStartPos = Camera.main.ScreenToWorldPoint(new Vector2(mousePos.x,mousePos.y));
            playerStartPos = transform.position;
        }
        else if(Input.GetMouseButton(0))
        {
            var curPos = Camera.main.ScreenToWorldPoint(new Vector2(mousePos.x,mousePos.y));
            
            var deltaX = curPos.x - mouseStartPos.x;
            var deltaY = curPos.y - mouseStartPos.y;

            var newXPos = Mathf.Clamp(playerStartPos.x + deltaX, xMin, xMax);
            var newYPos = Mathf.Clamp(playerStartPos.y + deltaY, yMin, yMax);
            transform.position = new Vector2(newXPos, newYPos);
        }
    }
}

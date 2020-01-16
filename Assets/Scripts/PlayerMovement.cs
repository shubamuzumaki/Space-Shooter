using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 17;
    [SerializeField] [Range(0, 1)] float maxYAllowed = 0.4f;
    [SerializeField] [Range(0, 0.1f)] float padding = 0.05f;
    

    private float xMin;
    private float xMax;
    private float yMin;
    private float yMax;

    void Start()
    {
        Debug.Log("Mouse/Gamepad Controls initialized");
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
        var deltaX = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
        var deltaY = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newXPos, newYPos);
    }
}

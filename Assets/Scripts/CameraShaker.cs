using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    Camera gameCamera;
    bool isShaking = false;

    private void Start() 
    {
        gameCamera = GetComponent<Camera>();
         
    }

    public void ShakeCamera(float duration, float magnitude)
    {
        if(!isShaking)
            StartCoroutine(ShakeCameraCoroutine(duration,magnitude));
    }

    IEnumerator ShakeCameraCoroutine(float duration, float magnitude)
    {
        isShaking = true;
        var origPosition = gameCamera.transform.position;   
        for(float i=0; i<duration; i+=Time.deltaTime)
        {
            float xVar = Random.Range(origPosition.x-magnitude,origPosition.x+magnitude);
            float yVar = Random.Range(origPosition.y-magnitude,origPosition.y+magnitude);
            gameCamera.transform.position = new Vector3(xVar,yVar,origPosition.z);
            yield return null;
        }

        isShaking = false;
        gameCamera.transform.position = origPosition;
    }

}

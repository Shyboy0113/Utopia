using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    
    public float moveTime = 2f;
    public float moveDistance = 25f;
    
    private Coroutine currentCoroutine;

    public void SetToLeft()
    {
        transform.position = new Vector3(3f - moveDistance, 0, 0);
    }

    public void SetToRight()
    {
        transform.position = new Vector3(3f,0f,0f);
    }

    public void MoveToLeft()
    {
        if (currentCoroutine != null)
        {
            // If the narrow coroutine is already running, stop it
            StopCoroutine(currentCoroutine);
        }

        currentCoroutine = StartCoroutine(MoveCamera(-moveDistance));
        
    }

    public void MoveToRight()
    {
        if (currentCoroutine != null)
        {
            // If the narrow coroutine is already running, stop it
            StopCoroutine(currentCoroutine);
        }

        currentCoroutine = StartCoroutine(MoveCamera(moveDistance));
        
    }

    IEnumerator MoveCamera(float distance)
    {
        Vector3 targetPosition = transform.position + new Vector3(distance, 0f, 0f);
        Vector3 initialPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < moveTime)
        {
            transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / moveTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure that the camera reaches the exact target position
        transform.position = targetPosition;
    }


}

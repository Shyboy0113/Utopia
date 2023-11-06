using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    
    public float moveTime = 5f;
    
    private Coroutine currentCoroutine;
    
    public void MoveToLeft()
    {
        if (currentCoroutine != null)
        {
            // If the narrow coroutine is already running, stop it
            StopCoroutine(currentCoroutine);
        }

        currentCoroutine = StartCoroutine(MoveCamera(-5f));
        
    }

    public void MoveToRight()
    {
        if (currentCoroutine != null)
        {
            // If the narrow coroutine is already running, stop it
            StopCoroutine(currentCoroutine);
        }

        currentCoroutine = StartCoroutine(MoveCamera(+5f));
        
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

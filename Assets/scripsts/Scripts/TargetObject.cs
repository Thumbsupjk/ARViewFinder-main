using UnityEngine;
using System.Collections;

// Example implementation of IRayEventReceiver
public class TargetObject : MonoBehaviour, IRayEventReceiver
{
    private bool triggered = false;
    private Coroutine moveCoroutine = null;
    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.position; // Store the original position
    }

    public void OnRaycastEnter()
    {
        if (!triggered)
        {
            triggered = true;
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine); // Stop any existing move coroutine
            }
            moveCoroutine = StartCoroutine(MoveObject(Vector3.up * 2.0f, 2.0f)); // Move up by 2 units over 2 seconds
        }
        Debug.Log("Ray Cast Enter");
    }

    public void OnRaycastExit()
    {
        if (triggered)
        {
            triggered = false;
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine); // Stop any existing move coroutine
            }
            moveCoroutine = StartCoroutine(MoveObject(originalPosition, 2.0f)); // Return to original position over 2 seconds
        }
        Debug.Log("Ray Cast Exit");
    }

    private IEnumerator MoveObject(Vector3 targetPosition, float duration)
    {
        float elapsedTime = 0.0f;
        Vector3 initialPosition = transform.position;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / duration);
            yield return null;
        }

        // Ensure it completes the final movement to avoid any precision errors
        transform.position = targetPosition;
        moveCoroutine = null; // Reset the coroutine reference
    }
}

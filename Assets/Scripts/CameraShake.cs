using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // Method that shakes the camera for a certain duration (shakeDuration) in random directions
    // with a certain magnitude/intensity (shakeMagnitude)
    public IEnumerator Shake(float shakeDuration, float shakeMagnitude)
    {
        Vector3 originalPosition = transform.localPosition;
        float elapsedTime = 0.0f;

        while (elapsedTime < shakeDuration)
        {
            float x = Random.Range(-1.0f, 1.0f) * shakeMagnitude;
            float y = Random.Range(-1.0f, 1.0f) * shakeMagnitude;

            transform.localPosition = new Vector3(x, y, originalPosition.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Resets the position of the camera to its original position from before the shake
        transform.localPosition = originalPosition;
    }
}

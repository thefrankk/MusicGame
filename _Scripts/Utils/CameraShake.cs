using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraShake : MonoBehaviour
{
    // Singleton instance.
    public static CameraShake Instance;

    // Duration of shake.
    private float shakeDuration = 0.2f;
    
    // Magnitude of shake.
    private float shakeMagnitude = 0.01f;

    // The initial position of the GameObject to which the script is attached.
    private Vector3 initialPosition;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Call this function to start camera shake.
    public void ShakeCamera()
    {

        transform.DOShakePosition(0.2f, 0.5f);
        // StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        initialPosition = transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            transform.localPosition = new Vector3(x, y, initialPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = initialPosition;
    }
}

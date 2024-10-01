using System.Collections;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public static ScreenShake Instance; 

    public float shakeDuration = 0.5f; // Ne kadar süre titreme yapacak
    public float shakeMagnitude = 0.1f;

    private RectTransform canvasRectTransform;
    private Vector3 originalPosition;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        canvasRectTransform = GetComponent<RectTransform>();
        originalPosition = canvasRectTransform.anchoredPosition;
    }

    public void Shake()
    {
        StartCoroutine(ShakeCoroutine());
    }

    private IEnumerator ShakeCoroutine()
    {
        float elapsed = 0.0f;

        while (elapsed < shakeDuration)
        {
            //Randomly determine power of shake
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            canvasRectTransform.anchoredPosition = originalPosition + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        canvasRectTransform.anchoredPosition = originalPosition;
    }
}

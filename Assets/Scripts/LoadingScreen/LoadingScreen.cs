using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI LoadingPercentageText;
    [SerializeField] private string sceneToLoad;

    void Start()
    {
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            int percent = Mathf.RoundToInt(progress * 100f);
            LoadingPercentageText.text = percent + "%";

            if (progress >= 1f)
            {
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}

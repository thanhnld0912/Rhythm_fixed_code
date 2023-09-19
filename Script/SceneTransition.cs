using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public float fadeDuration = 1.0f; // Duration of the fade effect
    public Color transitionColor = Color.black; // Color to transition to

    public int nextLevelNumber; // Number of the next level to load

    private void Start()
    {
        // Set the initial color to the current scene's background color
        transitionColor = Camera.main.backgroundColor;
    }

    public void ChangeLevel(int levelNumber)
    {
        nextLevelNumber = levelNumber;

        // Start the fading coroutine
        StartCoroutine(FadeOutAndLoadScene());
    }

    private System.Collections.IEnumerator FadeOutAndLoadScene()
    {
        // Fade out from the current scene's color to the transition color
        float elapsedTime = 0.0f;
        Color initialColor = Camera.main.backgroundColor;

        while (elapsedTime < fadeDuration)
        {
            float t = elapsedTime / fadeDuration;
            Color newColor = Color.Lerp(initialColor, transitionColor, t);
            SetSceneColor(newColor);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Load the next scene asynchronously based on the level number
        string nextSceneName = "Level" + nextLevelNumber;
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(nextSceneName);

        // Wait for the scene to finish loading
        while (!asyncOperation.isDone)
        {
            yield return null;
        }

        // Fade in from the transition color to the new scene's color
        elapsedTime = 0.0f;
        initialColor = transitionColor;

        while (elapsedTime < fadeDuration)
        {
            float t = elapsedTime / fadeDuration;
            Color newColor = Color.Lerp(initialColor, Camera.main.backgroundColor, t);
            SetSceneColor(newColor);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private void SetSceneColor(Color color)
    {
        Camera.main.backgroundColor = color;
        // You can also change the color of other objects in the scene if needed
        // by finding and modifying their materials or shaders
    }
}

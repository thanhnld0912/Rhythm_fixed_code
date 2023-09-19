using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundImageSwitcher : MonoBehaviour
{
    public Image backgroundImage;
    public Sprite[] backgroundImages;
    public float switchInterval = 5f; 
    public float transitionDuration = 2f; 

    private int currentImageIndex = 0;
    private Coroutine imageSwitchCoroutine;

    private void Start()
    {
        StartImageSwitching();
    }

    private void StartImageSwitching()
    {
        if (backgroundImages.Length == 0)
            return;

        if (imageSwitchCoroutine != null)
            StopCoroutine(imageSwitchCoroutine);

        imageSwitchCoroutine = StartCoroutine(SwitchBackgroundImageCoroutine());
    }

    private IEnumerator SwitchBackgroundImageCoroutine()
    {
        while (true)
        {
            int nextImageIndex = GetRandomImageIndex();

            float timer = 0f;
            Color initialColor = Color.white;
            Color targetColor = Color.black;

            while (timer <= transitionDuration)
            {
                float normalizedTime = timer / transitionDuration;
                backgroundImage.color = Color.Lerp(initialColor, targetColor, normalizedTime);
                timer += Time.deltaTime;
                yield return null;
            }

            backgroundImage.color = targetColor;

            currentImageIndex = nextImageIndex;
            backgroundImage.sprite = backgroundImages[currentImageIndex];

            timer = 0f;
            initialColor = targetColor;
            targetColor = Color.white;

            while (timer <= transitionDuration)
            {
                float normalizedTime = timer / transitionDuration;
                backgroundImage.color = Color.Lerp(initialColor, targetColor, normalizedTime);
                timer += Time.deltaTime;
                yield return null;
            }

            backgroundImage.color = targetColor;

            yield return new WaitForSeconds(switchInterval - (transitionDuration * 2));
        }
    }

    private int GetRandomImageIndex()
    {
        if (backgroundImages.Length <= 1)
            return 0;

        int randomIndex = currentImageIndex;
        while (randomIndex == currentImageIndex)
        {
            randomIndex = Random.Range(0, backgroundImages.Length);
        }

        return randomIndex;
    }
}

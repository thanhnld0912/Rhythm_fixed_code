using System.Collections;
using UnityEngine;
using TMPro;

public class RainbowText : MonoBehaviour
{
    public float colorChangeSpeed = 1f; 
    public float colorIntensity = 1f; 
    public Color[] rainbowColors;

    public TMP_Text textComponent;
    public int currentIndex = 0;

    private void Start()
    {
        textComponent = GetComponent<TMP_Text>();
    }

    private void Update()
    {

        float t = Mathf.PingPong(Time.time * colorChangeSpeed, rainbowColors.Length);
        currentIndex = Mathf.FloorToInt(t);

        
        string originalText = textComponent.GetParsedText();
        string rainbowText = "";
        for (int i = 0; i < originalText.Length; i++)
        {
            char c = originalText[i];
            Color color = rainbowColors[(currentIndex + i) % rainbowColors.Length] * colorIntensity;
            rainbowText += "<color=#" + ColorUtility.ToHtmlStringRGB(color) + ">" + c + "</color>";
        }

        
        textComponent.text = rainbowText;
    }
}

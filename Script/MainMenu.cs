using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioSource music;
    public bool startPlaying = true;
    public bool isPlaying = false;
    
    
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }
}

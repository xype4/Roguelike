using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public GameObject butonPanel;
    public GameObject startPanel;
    public Text seedText;
    public Slider difficult;
    public Slider size;

    private void Start()
    {
        butonPanel.SetActive(true);
        startPanel.SetActive(false);
    }

    public void InMainMenu()
    {
        butonPanel.SetActive(true);
        startPanel.SetActive(false);
    }

    public void InGameMenu()
    {
        butonPanel.SetActive(false);
        startPanel.SetActive(true);
    }

    public void LoadGame()
    {
        
        Settings.seed = seedText.text;
        Settings.difficult = difficult.value;
        Settings.size = size.value;
        SceneManager.LoadScene(1);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

}

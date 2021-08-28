using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenuMAnager : MonoBehaviour
{
    public GameObject pauseMenuObject;
    public string musicName = "Music";
    public GameObject settingsMenu;
    public bool hideOnPlay;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlayIf(musicName);
        if (hideOnPlay)
        {
            ResumeGame();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        AudioManager.instance.StopAll();
        PlayerController.instance.isStoped = true;
        pauseMenuObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    { 
        if(settingsMenu != null)
        {
            settingsMenu.SetActive(false);
        }
        PlayerController.instance.isStoped = false;
        pauseMenuObject.SetActive(false);
        Time.timeScale = 1f;
        AudioManager.instance.StartAll();
    }
}

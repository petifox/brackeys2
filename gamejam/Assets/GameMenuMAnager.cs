using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenuMAnager : MonoBehaviour
{
    public GameObject pauseMenuObject;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
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
        pauseMenuObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        pauseMenuObject.SetActive(false);
        Time.timeScale = 1f;
    }
}

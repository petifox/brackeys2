using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class MenuManager : MonoBehaviour
{
    public List<Menu> menus;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < menus.Count; i++)
        {
            menus[i].id = i;
        }
    }
    
    /// <summary>
    /// the name of the menu
    /// </summary>
    /// <param name="Name"></param>
    public void LoadMenu(string Name)
    {
        foreach (var item in menus)
        {
            if(item.gameObject.name == Name)
            {
                item.Activate();
            }
            else
            {
                item.DeActivate();
            }
        }
    }

    /// <summary>
    /// the menu's index from the menus list
    /// </summary>
    /// <param name="index"></param>
    public void LoadMenu(int index)
    {
        foreach (var item in menus)
        {
            if (item.id == index)
            {
                item.Activate();
            }
            else
            {
                item.DeActivate();
            }
        }
    }
    
    /// <summary>
    /// load a scene by it's build index
    /// </summary>
    /// <param name="BuildIndex"></param>
    public void LoadScene(int BuildIndex)
    {
        if(health.instance != null)
            health.instance.SetDefault();

        if (AudioManager.instance != null)
            AudioManager.instance.StopAll();

        SceneManager.LoadScene(BuildIndex);
    }

    /// <summary>
    /// load a scene by it's name
    /// </summary>
    /// <param name="name"></param>
    public void LoadScene(string name)
    {
        if (health.instance != null)
            health.instance.SetDefault();

        if (AudioManager.instance != null)
            AudioManager.instance.StopAll();

        AudioManager.instance.StopAll();
        SceneManager.LoadScene(name);
    }
    /// <summary>
    /// quit the application
    /// </summary>
    public void Quit()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}

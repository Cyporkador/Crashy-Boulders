using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject mainMenu;
    public GameObject levelSelect;

    public void back()
    {
        levelSelect.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void play()
    {
        levelSelect.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void Level1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void Level2()
    {
        SceneManager.LoadScene("Level2");
    }

    public void Level3()
    {
        SceneManager.LoadScene("Level3");
    }
}

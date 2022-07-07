using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : SingletonGeneric<MainMenu>
{
    public void Load_Single_Player()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit_Game()
    {
        Application.Quit();
    }
}

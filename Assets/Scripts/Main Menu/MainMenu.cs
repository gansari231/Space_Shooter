using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Load_Single_Player()
    {
        SceneManager.LoadScene(1);
    }

    public void Load_Multi_Player()
    {
        SceneManager.LoadScene(2);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private bool _isGameOver;
    [SerializeField]
    private bool _isCoopMode;
    [SerializeField]
    private GameObject _pauseGame;
    private Animator _pauseMenuAnim;
    void Start()
    {
        _pauseMenuAnim = GameObject.Find("Pause_Menu_Panel").GetComponent<Animator>();
        _pauseMenuAnim.updateMode = AnimatorUpdateMode.UnscaledTime;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {
            SceneManager.LoadScene(0);
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            CheckPaused();
        }
    }

    public void CheckGameStatus()
    {
        _isGameOver = true;
    }

    public void CheckPaused()
    {
         _pauseGame.SetActive(true);
        _pauseMenuAnim.SetBool("isPaused", true);
         Time.timeScale = 0;
    }

    public void Resume_Game()
    {
        _pauseGame.SetActive(false);
        Time.timeScale = 1;
    }
}

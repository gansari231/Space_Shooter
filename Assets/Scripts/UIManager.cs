using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _highScoreText;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartGameText;

    [SerializeField]
    private Image _scoreImage;
    [SerializeField]
    private Sprite[] _scoreSprite;

    private int _highScore = 0;
    private int _tempScore = 0;
    private GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score : " + 0;
        _highScore = PlayerPrefs.GetInt("HighScore", 0);
        _highScoreText.text = "HighScore : " + _highScore;
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(int _scoreToAdd)
    {
        _scoreText.text = "Score : " + _scoreToAdd;
        _tempScore = _scoreToAdd;
    }

    public void UpdateHighScore()
    {
        if (_tempScore > _highScore)
        {
            _highScore = _tempScore;
            PlayerPrefs.SetInt("HighScore", _highScore);
            _highScoreText.text = "HighScore : " + _highScore;
        }
    }
    public void UpdateLives(int _currentLives)
    {
        _scoreImage.sprite = _scoreSprite[_currentLives];

        if(_currentLives == 0)
        {
            GameOverSequence();
        }
    }

    void GameOverSequence()
    {
        _gameOverText.gameObject.SetActive(true);
        _restartGameText.gameObject.SetActive(true);
        StartCoroutine("GameOverFlicker");
        _gameManager.CheckGameStatus();
    }

    IEnumerator GameOverFlicker()
    {
        while(true)
        {
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void Resume_Game()
    {
        _gameManager.Resume_Game();
    }

    public void Back_To_MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : SingletonGeneric<UIManager>
{
    [SerializeField]
    private TextMeshProUGUI _scoreText;
    [SerializeField]
    private TextMeshProUGUI _highScoreText;
    [SerializeField]
    private TextMeshProUGUI _gameOverText;
    [SerializeField]
    private TextMeshProUGUI _restartGameText;
    [SerializeField]
    private TextMeshProUGUI _achievementName;
    [SerializeField]
    private TextMeshProUGUI _achievementInfo;
    [SerializeField]
    private CanvasRenderer _achievementPanel;
    [SerializeField]
    private TextMeshProUGUI _waveNumberText;
    [SerializeField]
    private TextMeshProUGUI _gameCompleteText;


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

        SubscribeEvents();
    }

    void OnDisable()
    {
        UnSubscribeEvents();
    }

    void SubscribeEvents()
    {
        EventHandler.Instance.OnGameOver += GameOverSequence;
    }

    void UnSubscribeEvents()
    {
        EventHandler.Instance.OnGameOver -= GameOverSequence;
    }

    public void UpdateScore(int _scoreToAdd)
    {
        _scoreText.text = "Score : " + _scoreToAdd;
        _tempScore = _scoreToAdd;
    }

    public void UpdateWavetext(int waveNumber)
    {
        _waveNumberText.text = "W A V E " + waveNumber;
        _waveNumberText.gameObject.SetActive(true);
        StartCoroutine(DisableWavetext());
    }

    IEnumerator DisableWavetext()
    {
        yield return new WaitForSeconds(3.0f);
        _waveNumberText.gameObject.SetActive(false);
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
            EventHandler.Instance.InvokeOnGameOver();
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

    public void LaserAchievementSystem(string AchievementName, string AchievmentInfo)
    {
        _achievementPanel.gameObject.SetActive(true);
        _achievementName.text = " " + AchievementName;
        _achievementInfo.text = " " + AchievmentInfo;
        StartCoroutine(AchievementSystem());
    }

    IEnumerator AchievementSystem()
    {
        yield return new WaitForSeconds(5.0f);
        _achievementPanel.gameObject.SetActive(false);
    }

    public void GameCompleted()
    {
        _gameCompleteText.gameObject.SetActive(true);
        StartCoroutine(GameEnd());
    }

    IEnumerator GameEnd()
    {
        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene("Main_Menu");
    }
}

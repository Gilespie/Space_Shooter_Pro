using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_ScoreText;
    [SerializeField] private TextMeshProUGUI m_BestScoreText;
    [SerializeField] private TextMeshProUGUI m_GameoverText;
    [SerializeField] private TextMeshProUGUI m_RestartText;
    [SerializeField] private GameObject m_PausePanel;
    [SerializeField] private Sprite[] m_LivesSprites;
    [SerializeField] private Image m_LivesImage;
    private GameManager _gameManager;
    private int _bestScore = 0;

    void Start()
    {
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        m_ScoreText.text = "Score: " + 0;

        LoadData();
        
        m_BestScoreText.text = "Best score: " + _bestScore;

        m_GameoverText.gameObject.SetActive(false);
        m_RestartText.gameObject.SetActive(false);

        if(_gameManager ==  null)
        {
            Debug.LogError("Game_Manager is NULL!");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            m_PausePanel.SetActive(true);
            Time.timeScale = 0f;
        }

        if (_gameManager._isGameover)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("Single_game");
            }
        }
    }

    public void UpdateScore(int playerscore)
    {
        m_ScoreText.text = "Score: " + playerscore.ToString();

        if(playerscore > _bestScore)
        {
            _bestScore = playerscore;
            UpdateBestScore();
        }
    }

    public void UpdateBestScore()
    {
        SaveData();
        m_BestScoreText.text = "Best score: " + _bestScore.ToString();
    }

    public void UpdateLives(int playerlives)
    {
        if (playerlives < 0) return;

        m_LivesImage.sprite = m_LivesSprites[playerlives];

        if(playerlives == 0)
        {
            GameOverSequence();
        }
    }

    private void GameOverSequence()
    {
        _gameManager.GameOver();
        m_GameoverText.gameObject.SetActive(true);
        m_RestartText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickeringText());
    }

    private IEnumerator GameOverFlickeringText()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.5f);
            m_GameoverText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            m_GameoverText.gameObject.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        m_PausePanel.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Main_menu");
        Time.timeScale = 1.0f;
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt("BestScore", _bestScore);
        PlayerPrefs.Save();
    }

    private void LoadData()
    {
        _bestScore = PlayerPrefs.GetInt("BestScore", _bestScore);
    }
}

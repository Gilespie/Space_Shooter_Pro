using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_ScoreText;
    [SerializeField] private TextMeshProUGUI m_GameoverText;
    [SerializeField] private TextMeshProUGUI m_RestartText;
    [SerializeField] private Sprite[] m_LivesSprites;
    [SerializeField] private Image m_LivesImage;

    private GameManager _gameManager;

    void Start()
    {
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        m_ScoreText.text = "Score: " + 0;
        m_GameoverText.gameObject.SetActive(false);
        m_RestartText.gameObject.SetActive(false);

        if(_gameManager ==  null)
        {
            Debug.LogError("Game_Manager is NULL!");
        }
    }
    
    public void UpdateScore(int playerscore)
    {
        m_ScoreText.text = "Score: " + playerscore.ToString();
    }

    public void UpdateLives(int playerlives)
    {
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
}

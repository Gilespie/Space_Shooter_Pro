using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject m_SettingsPanel;
    [SerializeField] private GameObject m_MainPanel;

    private void Start()
    {
        m_SettingsPanel.SetActive(false);    
    }

    public void SingleGame()
    {
        SceneManager.LoadScene("Single_game");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OpenSettings()
    {
        m_SettingsPanel.SetActive(true);
        m_MainPanel.SetActive(false);
    }

    public void CloseSettings()
    {
        m_MainPanel.SetActive(true);
        m_SettingsPanel.SetActive(false);
    }

    public void ResetStatistic()
    {
        PlayerPrefs.DeleteAll();
    }
}
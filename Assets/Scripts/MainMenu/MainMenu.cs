using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{ 
    public void SingleGame()
    {
        SceneManager.LoadScene("Single_game");
    }

    public void CoopGame()
    {
        SceneManager.LoadScene("Coop_game");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

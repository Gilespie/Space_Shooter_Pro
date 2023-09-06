using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool _isGameover;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && _isGameover == true)
        {
            SceneManager.LoadScene("Game");
        }
    }

    public void GameOver()
    {
        _isGameover = true;
    }
}

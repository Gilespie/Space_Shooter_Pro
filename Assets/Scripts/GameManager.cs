using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool _isGameover = false;

    public void GameOver()
    {
        _isGameover = true;
    }
}
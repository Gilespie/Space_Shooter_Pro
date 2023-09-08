using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject m_Player;
    public bool _isGameover = false;
    
    public void GameOver()
    {
        _isGameover = true;
    }
}

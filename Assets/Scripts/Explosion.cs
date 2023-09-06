using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float m_Seconds = 3f;

    private void Start()
    {
        Destroy(gameObject, m_Seconds);
    }
}

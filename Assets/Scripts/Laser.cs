using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float m_Speed = 8f;
    [SerializeField] private bool m_IsEnemy = false;

    private void Update()
    {
        if(m_IsEnemy)
        {
            MoveDown();
        }
        else
        {
            MoveUP();
        }
    }

    private void MoveUP()
    {
        transform.Translate(Vector3.up * m_Speed * Time.deltaTime);

        if (transform.position.y < -8)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(gameObject);
        }
    }

    private void MoveDown()
    {
        transform.Translate(Vector3.down * m_Speed * Time.deltaTime);

        if (transform.position.y > 8)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(gameObject);
        }
    }

    public void AssingEnemyLaser()
    {
        m_IsEnemy = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && m_IsEnemy == true)
        {
            Player player = other.GetComponent<Player>();

            if(player != null)
            {
                player.Damage();
            }
        }
    }
}
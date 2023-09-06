using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] private float m_Speed = 3f;
    [SerializeField] private int m_PowerupID;
    
    private void Update()
    {
        transform.Translate(Vector3.down * m_Speed * Time.deltaTime); 

        if(transform.position.y < -5.6f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Player player = other.transform.GetComponent<Player>();

            if(player != null)
            {
                switch(m_PowerupID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedBoostActive();
                        break;
                    case 2:
                        player.ShieldsActive();
                        break;

                    default: break;
                }
            }

            Destroy(gameObject);
        }
    }
}

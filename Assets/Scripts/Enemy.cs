using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float m_Speed = 4f;
    private Player _player;
    private Animator _Animator;
    private AudioSource _AudioSource;

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _AudioSource = GetComponent<AudioSource>();
        
        if(_player == null)
        {
            Debug.LogError("Player is NULL");
        }

        _Animator = GetComponent<Animator>();

        if(_Animator == null)
        {
            Debug.LogError("Animator is NULL");
        }

        if(_AudioSource == null)
        {
            Debug.LogError("AudioSource is NULL");
            
        }
    }

    private void Update()
    {
        transform.Translate(Vector3.down * m_Speed * Time.deltaTime);

        if(transform.position.y < -5.6f)
        {
            float randomX = Random.Range(-8, 8);
            transform.position = new Vector3(randomX, 7f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Player player = other.transform.GetComponent<Player>();

            if(player != null)
            {
                player.Damage();
            }

            _Animator.SetBool("OnEnemyDeath", true);
            m_Speed = 0;
            _AudioSource.Play();

            Destroy(gameObject, 2.8f);
        }

        if(other.CompareTag("Laser"))
        {
            Destroy(other.gameObject);

            if(_player != null)
            {
                _player.AddScore(10);
            }

            _Animator.SetBool("OnEnemyDeath", true);
            m_Speed = 0;
            _AudioSource.Play();

            Destroy(gameObject, 2.8f);
        }
    }
}

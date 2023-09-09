using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float m_Speed = 4f;
    [SerializeField] private GameObject m_LaserPrefab;
    private Player _player;
    private Animator _Animator;
    private float _fireRate = 3f;
    private float _canFire = -1f;
    private bool _isDead = false;

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        
        if(_player == null)
        {
            Debug.LogError("Player is NULL");
        }

        _Animator = GetComponent<Animator>();

        if(_Animator == null)
        {
            Debug.LogError("Animator is NULL");
        }
    }

    private void Update()
    {
        CalculateMovement();

        if(Time.time > _canFire && _isDead == false)
        {
            _fireRate = Random.Range(3f, 7f);
            _canFire = Time.time + _fireRate;
            GameObject enemyLaser = Instantiate(m_LaserPrefab, transform.position, Quaternion.identity);
            Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();

            for(int i =0; i < lasers.Length; i++) 
            {
                lasers[i].AssingEnemyLaser();
            }
        }
    }

    private void CalculateMovement()
    {
        transform.Translate(Vector3.down * m_Speed * Time.deltaTime);

        if (transform.position.y < -5.6f)
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
                _isDead = true;
            }

            _Animator.SetBool("OnEnemyDeath", true);
            m_Speed = 0;
            AudioManager.Instance.PlaySFX(2);

            Destroy(gameObject, 2.8f);
        }

        if(other.CompareTag("Laser"))
        {
            Destroy(other.gameObject);

            if(_player != null)
            {
                _player.AddScore(10);
                _isDead = true;
            }

            _Animator.SetBool("OnEnemyDeath", true);
            m_Speed = 0;
            AudioManager.Instance.PlaySFX(2);

            Destroy(GetComponent<Collider2D>());
            Destroy(gameObject, 2.8f);
        }
    }
}
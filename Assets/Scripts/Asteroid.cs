using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float m_RotateSpeed;
    [SerializeField] private GameObject m_ExplosionPrefab;
    private SpawnManager _spawnManager;

    private void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();    
    }

    void Update()
    {
        transform.Rotate(Vector3.forward * m_RotateSpeed * Time.deltaTime);    
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Laser"))
        {
            Instantiate(m_ExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            _spawnManager.StartSpawningRoutines();
            Destroy(gameObject);
        }
    }
}

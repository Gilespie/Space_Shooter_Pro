using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject m_EnemyPrefab;
    [SerializeField] private GameObject m_EnemyContainer;
    [SerializeField] private GameObject[] m_PowerupsPrefab;
    [SerializeField] private float m_EnemySeconds = 5f;
    [SerializeField] private float m_MinSecondsPowerup = 3f;
    [SerializeField] private float m_MaxSecondsPowerUp = 7f;

    private float _powerupSeconds;
    private Vector3 _spawnPosition;
    private bool _stopSpawning = false;

    void Start()
    {
        _powerupSeconds = Random.Range(m_MinSecondsPowerup, m_MaxSecondsPowerUp);

        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    void Update()
    {
        
    }

    private IEnumerator SpawnEnemyRoutine()
    {
        while (_stopSpawning == false)
        {
            _spawnPosition = new Vector3(Random.Range(-8, 8), 7, 0); 
            GameObject newEnemy = Instantiate(m_EnemyPrefab, _spawnPosition, Quaternion.identity);
            newEnemy.transform.parent = m_EnemyContainer.transform;
            yield return new WaitForSeconds(m_EnemySeconds);
        }
    }

    private IEnumerator SpawnPowerupRoutine()
    {
        while(_stopSpawning == false)
        {
            _spawnPosition = new Vector3(Random.Range(-8, 8), 7, 0);
            int randomPowerup = Random.Range(0, m_PowerupsPrefab.Length);
            Instantiate(m_PowerupsPrefab[randomPowerup], _spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(_powerupSeconds);
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}

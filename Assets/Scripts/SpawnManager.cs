using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject m_EnemyPrefab;
    [SerializeField] private GameObject m_EnemyContainer;
    [SerializeField] private GameObject[] m_PowerupsPrefab;
    [SerializeField] private float m_EnemySeconds = 5f;

    private float _powerupSeconds;
    private Vector3 _spawnPosition;
    private bool _stopSpawning = false;

    public void Startspawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    private IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3f);

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
        yield return new WaitForSeconds(3f);

        while(_stopSpawning == false)
        {
            _spawnPosition = new Vector3(Random.Range(-8, 8), 7, 0);
            int randomPowerup = Random.Range(0, 3);
            Instantiate(m_PowerupsPrefab[randomPowerup], _spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3f, 7f));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}

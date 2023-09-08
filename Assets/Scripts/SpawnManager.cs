using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject m_EnemyPrefab;
    [SerializeField] private GameObject m_EnemyContainer;
    [SerializeField] private GameObject[] m_PowerupsPrefab;
    [SerializeField] private float m_EnemySeconds = 5f;
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
    }

    public void StartSpawningRoutines()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    private IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3f);

        while (_gameManager._isGameover == false)
        {
            GameObject newEnemy = Instantiate(m_EnemyPrefab, new Vector3(Random.Range(-8, 8), 7, 0), Quaternion.identity);
            newEnemy.transform.parent = m_EnemyContainer.transform;
            yield return new WaitForSeconds(m_EnemySeconds);
        }
    }

    private IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(3f);

        while (_gameManager._isGameover == false)
        {
            int randomPowerup = Random.Range(0, 3);
            Instantiate(m_PowerupsPrefab[randomPowerup], new Vector3(Random.Range(-8, 8), 7, 0), Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3f, 7f));
        }
    }
}
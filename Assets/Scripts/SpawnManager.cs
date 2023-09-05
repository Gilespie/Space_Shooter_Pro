using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject m_EnemyPrefab;
    [SerializeField] private GameObject m_EnemyContainer;
    [SerializeField] private float m_Seconds;
    private Vector3 m_SpawnPosition;
    private bool stopSpawning = false;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    void Update()
    {
        
    }

    private IEnumerator SpawnRoutine()
    {
        while (stopSpawning == false)
        {
            m_SpawnPosition = new Vector3(Random.Range(-8, 8), 7, 0); 
            GameObject newEnemy = Instantiate(m_EnemyPrefab, m_SpawnPosition, Quaternion.identity);
            newEnemy.transform.parent = m_EnemyContainer.transform;
            yield return new WaitForSeconds(m_Seconds);
        }
    }

    public void OnPlayerDeath()
    {
        stopSpawning = true;
    }
}

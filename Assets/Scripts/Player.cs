using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float m_Speed;
    [SerializeField] private GameObject m_LaserPrefab;
    [SerializeField] private Vector3 m_SpawnPosition;
    [SerializeField] private float m_FireRate = 0.5f;
    [SerializeField] private int m_Lives = 3;
    private float canFire = -1;
    private SpawnManager m_SpawnManager;

    private void Start()
    {
        m_SpawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        transform.position = Vector3.zero;

        if(m_SpawnManager == null)
        {
            Debug.LogError("Spawn manager is NULL!");
        }
    }

    private void Update()
    {
        CalculateMovement();

        if (Input.GetMouseButtonDown(0) && Time.time > canFire)
        {
            FireLaser();
        }
    }

    private void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * m_Speed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    private void FireLaser()
    {
            canFire = Time.time + m_FireRate;
            Instantiate(m_LaserPrefab, transform.position + m_SpawnPosition, Quaternion.identity);
    }

    public void Damage()
    {
        m_Lives--;

        if(m_Lives < 1)
        {
            m_SpawnManager.OnPlayerDeath();
            Destroy(gameObject);
        }
    }
}

﻿using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float m_Speed;
    [SerializeField] private GameObject m_LaserPrefab;
    [SerializeField] private GameObject m_TripleShotPrefab;
    [SerializeField] private GameObject m_ShieldVisual;
    [SerializeField] private GameObject m_LeftEngine;
    [SerializeField] private GameObject m_RightEngine;
    [SerializeField] private Vector3 m_SpawnPosition;
    [SerializeField] private float m_FireRate = 0.5f;
    [SerializeField] private int m_Lives = 3;
    [SerializeField] private float m_DurationTripleShot = 3f;
    [SerializeField] private float m_DurationSpeedBoost = 5f;

    private SpawnManager _spawnManager;
    private UIManager _uiManager;
    private GameManager _gameManager;

    private float _canFire = -1;
    private bool _isTripleShotActive = false;
    private bool _isSpeedBoostActive = false;
    private bool _isShieldActive = false;
    private float _speedMultiplier = 2;
    private int _score = 0;

    private void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        _score = 0;
        transform.position = Vector3.zero;

        if(_spawnManager == null)
        {
            Debug.LogError("Spawn_Manager is NULL!");
        }

        if(_uiManager == null)
        {
            Debug.LogError("UI_Manager is NULL!");
        }
    }

    private void Update()
    {
        CalculateMovement();

        if (Input.GetMouseButtonDown(0) && Time.time > _canFire)
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
        _canFire = Time.time + m_FireRate;

        if (_isTripleShotActive)
        {
            Instantiate(m_TripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(m_LaserPrefab, transform.position + m_SpawnPosition, Quaternion.identity);
        }

        AudioManager.Instance.PlaySFX(0);
    }

    public void Damage()
    {
        if(_isShieldActive)
        {
            _isShieldActive = false;
            m_ShieldVisual.SetActive(false);
            return;
        }

        m_Lives--;

        if(m_Lives == 2)
        {
            m_LeftEngine.SetActive(true);
        }
        else if(m_Lives == 1)
        {
            m_RightEngine.SetActive(true);
        }

        _uiManager.UpdateLives(m_Lives);

        if(m_Lives < 1)
        {
            _gameManager.GameOver();
            Destroy(gameObject);
        }
    }

    public void TripleShotActive()
    {
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    public void SpeedBoostActive()
    {
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    private IEnumerator TripleShotPowerDownRoutine()
    {
        _isTripleShotActive = true;
        yield return new WaitForSeconds(m_DurationTripleShot);
        _isTripleShotActive = false;
    }
    private IEnumerator SpeedBoostPowerDownRoutine()
    {
        _isSpeedBoostActive = true;
        m_Speed *= _speedMultiplier;
        yield return new WaitForSeconds(m_DurationSpeedBoost);
        m_Speed /= _speedMultiplier;
        _isSpeedBoostActive = false;
    }

    public void ShieldsActive()
    {
        _isShieldActive = true;
        m_ShieldVisual.SetActive(true);
    }

    public void AddScore(int point)
    {
        _score += point;
        _uiManager.UpdateScore(_score);
    }
}
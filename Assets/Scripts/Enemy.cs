using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float m_Speed = 4f;

    private void Start()
    {
        
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Player player = other.transform.GetComponent<Player>();

            if(player != null)
            {
                player.Damage();
            }

            Destroy(gameObject);
        }

        if(other.CompareTag("Laser"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}

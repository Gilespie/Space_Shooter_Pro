using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float m_Speed = 8f;

    private void Update()
    {
        transform.Translate(Vector3.up * m_Speed * Time.deltaTime);    

        if(transform.position.y > 8)
        { 
            if(transform.parent != null)
            {
                Destroy(transform.parent.gameObject);   
            }

            Destroy(gameObject);
        }
    }
}

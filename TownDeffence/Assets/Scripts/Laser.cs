using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Spells
{
    [SerializeField] GameObject _particle;

    #region Start & End Laser point
    [SerializeField] Transform startPoint;
    [SerializeField] Transform endPoint;
    #endregion

    #region Lasers fields    
    LineRenderer line;    
    float speed = 4.5f;
    #endregion

    new void Start()
    {
        line = GetComponentInChildren<LineRenderer>();
        line.SetPosition(0, startPoint.position);
        line.SetPosition(1, endPoint.position);
    }

    new void Update()
    {
        if (line.enabled && _gameManager.isGameActive)
        {
            UpdateLaserPosition();
        }
        RemoveLaser();
    }

    void RemoveLaser()
    {
        if (endPoint.position.y >= 5.5f)
        {            
            line.enabled = false;
            _particle.SetActive(false);
            endPoint.position = startPoint.position;
        }
    }

    void UpdateLaserPosition()
    {   
        line.SetPosition(1, endPoint.position);
        _particle.transform.position = endPoint.position;
        if (endPoint.position.y <= 5.5f)
        {
            endPoint.Translate(Vector3.up * speed * Time.deltaTime);
        }
        EnemiesPosition(endPoint.position);        
    }

    void EnemiesPosition(Vector3 position)
    {
        Collider2D[] collider2D = Physics2D.OverlapCircleAll(position, 0.8f);
        if (collider2D.Length > 0)
        {
            foreach (Collider2D col in collider2D)
            {
                if (col.CompareTag("Enemy"))
                {
                    Vector3 direction = col.transform.position - position;
                    direction.z = 0;                    
                    DamageEnemy(col.transform, 100);
                }
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{  
    float _topBorder = 5.5f;

    void Update()
    {
        if (transform.position.y > _topBorder)
        {
            gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        ParticleSystem effect = ObjectPool.SharedInstance.GetPooledParticle();
        if (effect != null)
        {
            effect.transform.position = transform.position;
            effect.transform.rotation = transform.rotation;
            effect.Play();
        }       
        gameObject.SetActive(false);                      
    }
} 
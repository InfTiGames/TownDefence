using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Spells
{
    private new void Start()
    {
        

    }

    void Explote()
    {
        DamageObjects(transform.position);        
    }

    new void Update()
    {
        StartCoroutine(ExploteDelay());      
    }

    void DamageObjects(Vector3 position)
    {
        Collider2D[] collider2D = Physics2D.OverlapCircleAll(position, 1.5f);
        if (collider2D.Length > 0)
        {
            foreach (Collider2D col in collider2D)
            {
                if (col.CompareTag("Enemy"))
                {
                    Vector3 direction = col.transform.position - position;
                    direction.z = 0;
                    col.attachedRigidbody.AddForce(direction.normalized * _power/4);
                    DamageEnemy(col.transform, 15);
                }
            }
        }        
    }

    IEnumerator ExploteDelay()
    {
        yield return new WaitForSeconds(0.2f);
        Explote();
        gameObject.SetActive(false);        
    }
}
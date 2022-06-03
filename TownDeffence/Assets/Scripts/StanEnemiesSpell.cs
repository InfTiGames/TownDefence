using UnityEngine;
using System.Collections;

public class StanEnemiesSpell : Spells
{
    void StanEnemies()
    {
        EnemiesPosition(transform.position);        
    }

    public void StanDelay()
    {
        if (_gameManager.isGameActive)
        {            
            UseSpell(25f);            
            Invoke("StanEnemies", 0.5f);
        }
    }

    void EnemiesPosition(Vector3 position)
    {
        Collider2D[] collider2D = Physics2D.OverlapCircleAll(position, 9);
        if (collider2D.Length > 0)
        {
            foreach (Collider2D col in collider2D)
            {
                if (col.CompareTag("Enemy"))
                {
                    StartCoroutine(EnemiesToStop(col.attachedRigidbody));
                }
            }
        }
    }

    IEnumerator EnemiesToStop(Rigidbody2D enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();
        if (e != null)
        {
            e.inStan = true;
        }        
        yield return new WaitForSeconds(5.5f);
        if (e != null)
        {
            e.inStan = false;
        }
    }
}
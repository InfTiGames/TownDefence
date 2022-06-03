using UnityEngine;

public class WaveSpell : Spells
{
    void WaveActivate()
    {        
        EnemiesPosition(transform.position);
    }

    public void ActivateDelay()
    {    
        if (_gameManager.isGameActive)
        {            
            UseSpell(30f);
            _gameManager.UpdateCoins(-100);
            Invoke("WaveActivate", 1.0f);
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
                    Vector3 direction = col.transform.position - position;
                    direction.z = 0;
                    col.attachedRigidbody.AddForce(direction.normalized * _power);
                    DamageEnemy(col.transform, 30);
                }
            }
        }        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1f, 0f, 0, 5f);
        Gizmos.DrawWireSphere(transform.position, 9);
    }
}
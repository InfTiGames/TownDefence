using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerControler : MonoBehaviour
{
    #region Shoot & Bullet fields
    [HideInInspector] public bool shooting;
    [SerializeField] float _shootInvoke = 1f;
    public float shootInvokeColdown = 1F;
    public bool secondFirePointIsActive = false;
    public Transform firePoint;
    public Transform secondFirePoint;
    [SerializeField] float _bulletForce = 5.0f;
    [SerializeField] float _rotspeed = 20.0f;
    #endregion

    #region Current Target
    Vector2 _distance = new Vector2(0, 9f);
    Vector3 _offset = new Vector3(0, 0.4f, 0);
    GameObject _curTarget;
    #endregion

    #region TemporarilyPowerUp Fields
    public bool hasPowerUP = false;
    public GameObject powerUpIndic;
    #endregion
        
    public GameManager gameManager;

    private void FixedUpdate()
    {
        Rotation();
    }

    void Rotation()
    {
        if (_curTarget != null && gameManager.isGameActive)
        {
            Vector2 lookDir = _curTarget.transform.position - transform.position - _offset;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            if (lookDir.y < _distance.y)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _rotspeed * Time.deltaTime);
                if (_shootInvoke > 0) _shootInvoke -= Time.deltaTime;
                if (_shootInvoke < 0) _shootInvoke = 0;
                if (_shootInvoke == 0)
                {
                    shooting = true;
                    Shoot();
                    _shootInvoke = shootInvokeColdown;
                }
            }
        }
        else
        {
            shooting = false;
            _curTarget = SortTargets();
        }
    }

    public void UpdateFirePOintPos()
    {        
        Vector2 newPos = new Vector2(0.2f, 0.7f);
        firePoint.transform.TransformPoint(newPos);
    }

    void Shoot()
    {
        GameObject bullet = ObjectPool.SharedInstance.GetPooledObject();
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (bullet != null)
        {
            bullet.transform.position = firePoint.transform.position;
            bullet.transform.rotation = firePoint.transform.rotation;
            bullet.SetActive(true);
            rb.AddForce(firePoint.up * _bulletForce, ForceMode2D.Impulse);
        }
        if (secondFirePointIsActive)
        {
            GameObject bul = ObjectPool.SharedInstance.GetPooledObject();
            Rigidbody2D rbp = bul.GetComponent<Rigidbody2D>();
            if (bul != null)
            {
                bul.transform.position = secondFirePoint.transform.position;
                bul.transform.rotation = secondFirePoint.transform.rotation;
                bul.SetActive(true);                    
                rbp.AddForce(secondFirePoint.up * _bulletForce, ForceMode2D.Impulse);
            }
        }
    }

    public void StartRoutine()
    {
        StartCoroutine(PowerUpCountdownRoutine());
    }

    IEnumerator PowerUpCountdownRoutine()
    {
        yield return new WaitForSeconds(10.0f);
        hasPowerUP = false;
        shootInvokeColdown += 0.25f;
        powerUpIndic.gameObject.SetActive(false);
    }

    GameObject SortTargets()
    {
        float closestDistance = 10.0f;
        GameObject nearest = null;
        List<GameObject> sorting = GameObject.FindGameObjectsWithTag("Enemy").ToList();
        foreach (var everyTarget in sorting)
        {            
            if ((Vector2.Distance(everyTarget.transform.position, transform.position) < closestDistance) || closestDistance == 5.0f)
            {
                closestDistance = Vector2.Distance(everyTarget.transform.position, transform.position);
                nearest = everyTarget;
            }
        }
        return nearest;
    }
}
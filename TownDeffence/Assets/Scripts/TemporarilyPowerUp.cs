using UnityEngine;

public class TemporarilyPowerUp : MonoBehaviour
{
    PlayerControler _playerControler;
    GameManager _gameManager;
    Rigidbody2D _powerUpRb;
    public GameObject expl;

    #region Spawn position & Speed fields
    float Speed = 3.0f;
    float xRange = 2.0f;
    float ySpawnPos = -5.5f;
    #endregion

    void Start()
    {
        _playerControler = GameObject.Find("Player").GetComponent<PlayerControler>();
        _powerUpRb = GetComponent<Rigidbody2D>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _powerUpRb.AddForce(GenerateRandomForce(), ForceMode2D.Impulse);
        transform.position = GenerateRandomSpawnPos();
    }

    void OnMouseDown()
    {
        if (_gameManager.isGameActive)
        {
            _playerControler.StartRoutine();
            _playerControler.hasPowerUP = true;
            _playerControler.powerUpIndic.gameObject.SetActive(true);
            _playerControler.shootInvokeColdown -= 0.25f;            
            GameObject effect = Instantiate(expl, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
            Destroy(gameObject);                       
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {        
        if (collision.gameObject.CompareTag("GameOver"))
        {
            Destroy(gameObject);
        }
    }

    Vector2 GenerateRandomForce()
    {
        return Vector2.up * Speed;
    }

    Vector2 GenerateRandomSpawnPos()
    {
        return new Vector2(Random.Range(-xRange, xRange), ySpawnPos);
    }
}
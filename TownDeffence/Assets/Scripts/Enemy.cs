using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;
    protected GameManager _gameManager;
    public static int beginDamage = 12;
    public bool inStan;    

    #region FXs Fields
    public GameObject explosionFx;
    public GameObject hitFx;
    #endregion

    #region Health Fields
    public HealthBar healtBar;
    public static int maxHealth;
    public int currentHealth;
    #endregion

    #region Speed Fields
    public static float speed;
    float _randomSpeed;
    float _minSpeed = 0.1f;
    float _maxSpeed = 0.9f;
    #endregion

    #region Level Fields
    LevelUp _experience;
    int _expCount;
    float _minExp = 4f;
    float _maxExp = 12f;
    #endregion

    #region Score Fields
    int _maxScoreValue = 15;
    int _minScoreValue = 5;
    #endregion

    void Start()
    {        
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _experience = GameObject.Find("LvlUpBar").GetComponent<LevelUp>();        
        rb = GetComponent<Rigidbody2D>();

        //if (name == "FirstBoss")
        //{
        //    currentHealth = 2000;
        //    healtBar.SetMaxHealth(currentHealth);
        //    _randomSpeed = 0.25f;
        //    _expCount = 20;
        //}
        //else if (name == "SecondBoss")
        //{
        //    currentHealth = 3000;
        //    healtBar.SetMaxHealth(currentHealth);
        //    _randomSpeed = 0.2f;
        //    _expCount = 40;
        //}
        //else if (name == "ThirdBoss")
        //{
        //    currentHealth = 4000;
        //    healtBar.SetMaxHealth(currentHealth);
        //    _randomSpeed = 0.3f;
        //    _expCount = 60;
        //}
        //else
        //{
            currentHealth = maxHealth;
            healtBar.SetMaxHealth(maxHealth);
            _randomSpeed = Random.Range(_minSpeed, _maxSpeed);
            _expCount = (int)Random.Range(_minExp, _maxExp);
        //}
    }

    void FixedUpdate()
    {
        if (_gameManager.isGameActive)
        {
            if (currentHealth <= 0)
            {
                GameObject effect = Instantiate(explosionFx, transform.position, Quaternion.identity);
                Destroy(effect, 1f);
                _gameManager.UpdateCoins(Random.Range(_minScoreValue, _maxScoreValue));
                //if (name == "Enemy(Clone)")
                //{
                    GameManager._enemiesOnMap--;
                //}

                //if (name == "FirstBoss")
                //{
                //    GameManager.waveNumber++;
                //}
                _experience.SetExp(_expCount);
                Destroy(gameObject);
            }
            Movement();

            //if (GameManager.waveNumber == 10)
            //{
            //    if (name == "Enemy(Clone)")
            //    {
            //        gameObject.SetActive(false);
            //    }                
            //}
            //else
            //{
            //    if (name == "Enemy(Clone)")
            //    {
            //        gameObject.SetActive(true);
            //    }
            //}

            //if (GameManager.waveNumber == 20 && name == "Enemy(Clone)")
            //{
            //    gameObject.SetActive(false);
            //}

            //if (GameManager.waveNumber == 30 && name == "Enemy(Clone)")
            //{
            //    gameObject.SetActive(false);
            //}
        }
    }

    void Movement()
    {
        if (inStan)
        {
            rb.gravityScale = 0;
        }
        else
        {
            rb.gravityScale = _randomSpeed * speed / 3.5f;
        }        
    }

    void OnMouseDown()
    {
        if (_gameManager.isGameActive)
        {            
            GameObject effect = Instantiate(hitFx, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
            TakeDamage(8);            
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(beginDamage);
        }

        if (collision.gameObject.CompareTag("GameOver"))
        {            
            Destroy(gameObject);
            GameManager.untilGameOver++;
            GameManager._enemiesOnMap--;
            if (GameManager.untilGameOver >= 3)
            {
                _gameManager.GameOver();
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healtBar.SetHealth(currentHealth);        
    }
}
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region GameObjects
    public GameObject coinsImage;
    public GameObject titleScreen;
    public GameObject powerUp;
    public GameObject enemy;
    public GameObject Player;
    public GameObject Hp;
    public GameObject gameOver;
    public GameObject spawnRangeObj;
    public GameObject spellPanel;
    //public GameObject firstBoss;
    //public GameObject secondBoss;
    //public GameObject thirdBoss;
    #endregion

    #region Buttons
    public Button Lazer;
    public Button shopLazer;
    public Button Stan;
    public Button shopStan;
    #endregion

    public TextMeshProUGUI tmpScore;    
    public bool isGameActive;
    public bool IsClicked;    
    public static int waveNumber;
    public static int untilGameOver = 0;
    public int coins;
    int _enemyCount = 1;    
    public static int _enemiesOnMap;
    bool _buyLaser;
    bool _buyStan;

    void Update()
    {
        if (_enemiesOnMap <= 0 && isGameActive)
        {
            _enemyCount++;
            waveNumber++;
            SpawnEnemyWave(_enemyCount);
        }

        //if (waveNumber == 10 && firstBoss != null)
        //{
        //    firstBoss.SetActive(true);
        //}
        //else if (waveNumber == 20 && secondBoss != null)
        //{
        //    secondBoss.SetActive(true);
        //}
        //else if (waveNumber == 30 && thirdBoss != null)
        //{
        //    thirdBoss.SetActive(true);
        //}
    }

    public void UpdateCoins(int scoreToAdd)
    {
        coins += scoreToAdd;
        tmpScore.text = "Score: " + coins;
    }

    public void GameOver()
    {        
        gameOver.gameObject.SetActive(true);
        isGameActive = false;
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator PowerUps()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(30.0f);
            Instantiate(powerUp, powerUp.transform.position, powerUp.transform.rotation);
        }
    }

    public void buyLaser()
    {
        _buyLaser = true;
        //PlayerPrefs.SetInt("LASER", 1);
        shopLazer.interactable = false;
    }

    public void buyStan()
    {
        _buyStan = true;
        shopStan.interactable = false;
    }

    public void StartGame(int difficulty)
    {
        untilGameOver = 0;
        Time.timeScale = 1;
        titleScreen.gameObject.SetActive(false);
        spellPanel.SetActive(true);
        
        if (_buyLaser)
        {
            Lazer.gameObject.SetActive(true);
        }

        if (_buyStan)
        {
            Stan.gameObject.SetActive(true);
        }

        Player.gameObject.SetActive(true);
        coinsImage.SetActive(true);
        Hp.gameObject.SetActive(true);        
        isGameActive = true;
        Enemy.maxHealth = 40 * difficulty;
        Enemy.speed = difficulty;
        coins = 1000;
        StartCoroutine(PowerUps());
        SpawnEnemyWave(_enemyCount);
        UpdateCoins(0);
        waveNumber = 8;
    }

    void SpawnEnemyWave(int enemiesToSPawn)
    {
        for (int i = 0; i < enemiesToSPawn; i++)
        {
            Instantiate(enemy, GenerateSpawnPosition(), enemy.transform.rotation);
        }
        _enemiesOnMap = enemiesToSPawn;
    }
    
    Vector2 GenerateSpawnPosition()
    {
        float spawnX = Random.Range(-spawnRangeObj.transform.position.x, spawnRangeObj.transform.position.x);
        float spawnY = Random.Range(10, 5);
        Vector2 randomPos = new Vector2(spawnX, spawnY);
        return randomPos;
    }
}
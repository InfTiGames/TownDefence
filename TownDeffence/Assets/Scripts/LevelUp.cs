using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelUp : MonoBehaviour
{
    [SerializeField] PlayerControler _playerControler;
    public GameManager gameManager;

    #region Shield Fields
    [HideInInspector] public bool firstShieldsActivation;
    public GameObject shield;
    float _shieldRepeating = 30f;
    #endregion

    #region Level Fields
    public GameObject panel;
    public Slider slider;
    public Image fill;
    public int lvl;
    int _maxExp = 20;
    #endregion

    void Start()
    {
        SetMaxExp(_maxExp);
    }

    void Update()
    {
        if (slider.value == _maxExp)
        {
            _maxExp += 20;
            SetMaxExp(_maxExp);
            panel.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }

    void SetMaxExp(int exp)
    {
        slider.maxValue = exp;
        slider.value = 0;
    }

    public void SetExp(int exp)
    {
        slider.value += exp;        
    }

    public void IncreaceShootSpeed()
    {
        _playerControler.shootInvokeColdown -= 0.05f;
        Time.timeScale = 1;
    }

    public void AddShootPoint()
    {
        _playerControler.UpdateFirePOintPos();
        _playerControler.secondFirePointIsActive = true;
        Time.timeScale = 1;
    }

    public void IncreaceDamage()
    {
        Enemy.beginDamage++;
        Time.timeScale = 1;
    }

    public void TwoMoreLives()
    {
        GameManager.untilGameOver -= 2;
        Time.timeScale = 1;
    }

    public void StartShield()
    {
        if (!firstShieldsActivation)
        {
            StartCoroutine(ShieldActivate());
            firstShieldsActivation = true;
        }
        else
        {
            _shieldRepeating -= 15f;            
        }
        Time.timeScale = 1;
    }

    IEnumerator ShieldActivate()
    {
        while (gameManager.isGameActive)
        {            
            shield.gameObject.SetActive(true);
            yield return new WaitForSeconds(_shieldRepeating);
            shield.gameObject.SetActive(false);
            yield return new WaitForSeconds(10);
        }
    }
}
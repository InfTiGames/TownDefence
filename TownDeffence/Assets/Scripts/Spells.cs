using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Spells : MonoBehaviour
{
    [SerializeField] protected GameManager _gameManager;    
    protected float _power = 10000f;

    [SerializeField] protected Image imageCooldown;
    [SerializeField] protected TMP_Text textCooldown;
    [SerializeField] Button spellButton;

    protected bool isCooldown;
    protected float cooldownTime;
    protected float cooldownTimer = 0.0f;

    protected virtual void Start()
    {
        textCooldown.gameObject.SetActive(false);
        imageCooldown.fillAmount = 0.0f;
    }

    protected virtual void Update()
    {
        if (isCooldown)
        {
            ApplyCooldown();
        }
    }

    protected void ApplyCooldown()
    {
        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0.0f)
        {
            isCooldown = false;
            textCooldown.gameObject.SetActive(false);
            imageCooldown.fillAmount = 0.0f;
            spellButton.interactable = true;
        }
        else
        {
            textCooldown.text = Mathf.RoundToInt(cooldownTimer).ToString();
            imageCooldown.fillAmount = cooldownTimer / cooldownTime;
        }
    }

    public void UseSpell(float cd)
    {
        if (!isCooldown)
        {
            isCooldown = true;
            textCooldown.gameObject.SetActive(true);
            cooldownTimer = cd;
            cooldownTime = cd;
            spellButton.interactable = false;
        }
    }

    protected void DamageEnemy(Transform enemy, int damage)
    {
        Enemy enemies = enemy.GetComponent<Enemy>();
        if (enemies != null)
        {
            enemies.TakeDamage(damage);
        }
    }
}
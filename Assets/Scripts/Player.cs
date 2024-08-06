using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Text YouDiedText;
    [SerializeField] GameObject Weapon;
    public PlayerHealthBar healthBar;
    [SerializeField] Text HealText;
    public bool HealingPortionUsed = false;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        YouDiedText.enabled = false;
        HealText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            YouDiedText.enabled = true;
            Weapon.SendMessage("PlayerDied");
        }

        if (currentHealth == 50 && !HealingPortionUsed)
        {
            HealText.enabled = true;
        }

        if (Input.GetKey(KeyCode.F) && HealText.isActiveAndEnabled)
        {
            currentHealth = 100;
            HealText.enabled = false;
            healthBar.SetHealth(currentHealth);
            HealingPortionUsed = true;  
        }
    }

    void GotHit ()
    {
        if (currentHealth > 0)
            currentHealth -= 10;
        healthBar.SetHealth(currentHealth); 
    }
}

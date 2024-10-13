using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public float waitTime = 10.0f;

    private int healthNumber;
    private Text healthDisplay;
    private float healTimer = 0.0f;
    private DeathMenu DMenu;
    private bool playerDead = false;

    // Start is called before the first frame update
    void Start()
    {
        healthDisplay = GameObject.Find("Health").GetComponent<Text>();
        healthNumber = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        DMenu = GameObject.Find("DeathCanvasMap").GetComponent<DeathMenu>();
        if (healthNumber <= 0)
        {
            playerDead = true;

            DMenu.youDied();

            healthNumber = maxHealth;

        }
        if (healthNumber < maxHealth)
        {
            healTimer += Time.deltaTime;
            if (healTimer > waitTime)
            {
                healthNumber = maxHealth;
                healTimer = 0.0f;
            }
        }
    }

    public void lowerHealth(int damage)
    {
        healthNumber -=damage;
    }

    public void increaseHealth(int heal)
    {
        healthNumber += heal;
    }

    public void resetHealth()
    {
        healthNumber = maxHealth;
    }

    private void OnGUI()
    {
        healthDisplay.text = healthNumber.ToString();
    }

    public bool IsPlayerDead()
    {
        return playerDead;
    }

    public void SetPlayerAlive()
    {
        playerDead = false;
    }
}

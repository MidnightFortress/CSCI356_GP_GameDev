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
    private PlayerPlacer playerPlacer;
    private float healTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        healthDisplay = GameObject.Find("Health").GetComponent<Text>();
        healthNumber = maxHealth;

        playerPlacer = GetComponent<PlayerPlacer>();
        if (playerPlacer == null)
        {
            Debug.LogError("Health Script: PlayerPlacer script not found on the player.");
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(healthNumber <= 0)
        {
            if (playerPlacer != null)
            {
                playerPlacer.RespawnPlayer();  // call lthe player placer script
            }

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
}

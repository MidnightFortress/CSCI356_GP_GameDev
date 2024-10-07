using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;

    private int healthNumber;
    private Text healthDisplay;
    
    // Start is called before the first frame update
    void Start()
    {
        healthDisplay = GameObject.Find("Health").GetComponent<Text>();
        healthNumber = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(healthNumber <= 0)
        {
            gameObject.transform.position = GameObject.Find("PlayerSpawn").GetComponent<Transform>().position;
            healthNumber = maxHealth;
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

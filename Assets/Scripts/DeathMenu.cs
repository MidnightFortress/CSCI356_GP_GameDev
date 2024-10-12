using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMenu : MonoBehaviour
{
    public GameObject DeathMenuUI;

    private PlayerPlacer playerPlacer;

    void Update()
    {
        GameObject respawnObject = GameObject.FindGameObjectWithTag("Respawn");
        if (respawnObject != null)
        {
            playerPlacer = respawnObject.GetComponent<PlayerPlacer>();
        }

        if (playerPlacer == null)
        {
            Debug.LogError("Health Script: PlayerPlacer script not found on the Respawn object.");
        }
    }

    public void respawnButton()
    {
        DeathMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        PauseMenu.GamePaused = false;
        playerPlacer.RespawnPlayer();
    }

    public void youDied()
    {
        DeathMenuUI.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        PauseMenu.GamePaused = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

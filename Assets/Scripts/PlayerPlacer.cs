using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPlacer : MonoBehaviour
{
    private Vector3 startPos;
    private Quaternion startRot;
    GameObject respawnObject;
    public GameObject elevator;
    public GameObject block;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    // loading and deleteling the scenes

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        RespawnPlayer();
    }

    public void RespawnPlayer()
    {
        MusicManager musicMe = FindAnyObjectByType<MusicManager>();
        if (musicMe.CurrentSong == null)
        {
            Debug.LogError("CurrentSong is not assigned in MusicManager!");
        }
        else if (musicMe.CurrentSong == null)
        {
            Debug.LogError("CurrentSong is not assigned in MusicManager!");
        }
        else
        {
            musicMe.CurrentSong.mute = true;
        }

        respawnObject = GameObject.FindGameObjectWithTag("Respawn");
        if (respawnObject != null)
        {
            
            startPos = respawnObject.transform.position;
            startRot = respawnObject.transform.rotation;

            Debug.Log("StartPos from Respawn object: " + startPos);
                // finding the spawn point, as i tagged it as respwan

                GameObject player = GameObject.FindWithTag("Player");
                if (player != null)
                {
                Debug.Log("Original Player Position before placement: " + player.transform.position);
                FPSInput fpsInput = player.GetComponent<FPSInput>();
                Health healthMe = player.GetComponent<Health>();

                CharacterController charControl = player.GetComponent<CharacterController>();

                if (charControl != null)
                {
                    charControl.enabled = false; 
                } // turn this asshole off messes with the respawn

                fpsInput.enabled = false;

                fpsInput.ResetVelocity();

                // check if elevator present
                if (elevator != null)
                {
                    // reset elevator
                    elevator.GetComponent<ElevatorBehaviour>().ResetPosition();
                }

                if (block != null)
                {
                    block.GetComponent<DropBlock>().ResetBlock();
                }

                player.transform.position = startPos;
                player.transform.rotation = startRot;

                Debug.Log("Player Placed at: " + startPos);

                Debug.Log("Actual Player Position after placement: " + player.transform.position);

                fpsInput.enabled = true;

                if (charControl != null)
                {
                    charControl.enabled = true; // Turn on the CharacterController
                    healthMe.SetPlayerAlive(); // mark player as not dead
                }


                // this if gets the player and places them in the spawn point. tyhere is a bit of debugging as well, to make sure it is working
                // had a bit of a problem with this as sometimes it works sometimes it doesnt. If it doesnt work it just leaves the player in the same position as scene 1
                // to counter this i moved scene 1 to the dsame position as scene 2, so if it doenst work it should hopefully drop the player in the same room in scene 2

                // figured out the problem the movement and the char controller were fucking with the placer. you have to turn them off, place the player and then turn it back on
            }
                else
                {
                    Debug.LogError("Player not found in the scene.");
                }
        }
          else
            {
                Debug.LogError("Respawn object does not have the PlayerPlacer script.");
            }
        }
    // extra debugs
    public void SetRespawn()
    {
        respawnObject = GameObject.FindGameObjectWithTag("Respawn");
        startPos = new Vector3(191,92,240);
        respawnObject.transform.position = startPos;
        Debug.Log("Respawn point updated.");
        // this is so we can create a checkpoint, and the player doesnt have to reload and walk all the way back after death
    }

}


using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPlacer : MonoBehaviour
{
    private Vector3 startPos;
<<<<<<< HEAD

    private bool hasPlacedPlayer = false;

=======
    //private bool hasPlacedPlayer = false; this is never used and so I commented it out..
>>>>>>> parent of 1acd4f1 (Merge pull request #18 from Fulminin/Will's-Branch)
    private Quaternion startRot;

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
<<<<<<< HEAD

        hasPlacedPlayer = false;
=======
        // hasPlacedPlayer = false;
>>>>>>> parent of 1acd4f1 (Merge pull request #18 from Fulminin/Will's-Branch)
        // placed a bool because it was sometimes placing teh player twice

        GameObject respawnObject = GameObject.FindGameObjectWithTag("Respawn");
        if (respawnObject != null)
        {
            PlayerPlacer respawnPlacer = respawnObject.GetComponent<PlayerPlacer>();
            if (respawnPlacer != null)
            {
                startPos = respawnObject.transform.position;
                startRot = respawnObject.transform.rotation;
                Debug.Log("StartPos from Respawn object: " + startPos);
                // finding the spawn point, as i tagged it as respwan

                GameObject player = GameObject.FindWithTag("Player");
                if (player != null)
                {
                    Debug.Log("Original Player Position before placement: " + player.transform.position);

                    player.transform.position = startPos;
                    player.transform.rotation = startRot;

                    Debug.Log("Player Placed at: " + startPos);

                    Debug.Log("Actual Player Position after placement: " + player.transform.position);

<<<<<<< HEAD

                    hasPlacedPlayer = true;

=======
                    // hasPlacedPlayer = true;
>>>>>>> parent of 1acd4f1 (Merge pull request #18 from Fulminin/Will's-Branch)
                    // this if gets the player and places them in the spawn point. tyhere is a bit of debugging as well, to make sure it is working
                    // had a bit of a problem with this as sometimes it works sometimes it doesnt. If it doesnt work it just leaves the player in the same position as scene 1
                    // to counter this i moved scene 1 to the dsame position as scene 2, so if it doenst work it should hopefully drop the player in the same room in scene 2
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
        else
        {
            Debug.LogError("No object tagged 'Respawn' found in the scene.");
        }
        // extra debugs
    }
}


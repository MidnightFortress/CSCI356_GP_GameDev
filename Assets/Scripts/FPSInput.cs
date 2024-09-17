using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script")]
public class FPSInput : MonoBehaviour
{
    // Variables which can be modified in editor
    public int jumpMax = 2; // The number of times able to jump before needing to touch the ground again
    public float playerSpeed = 5.0f; // speed for player movement
    public float jumpHeight = 0.5f; //how high to jump
    public float gravity = -9.8f; // Gravity Strength

    //Internal Variables
    private int airJump = 0; // Count of how many times character jumped since touching the ground
    private CharacterController charControl; // the controler for the character
    private Vector3 playerVelocity; // how fast the player moves
    private bool grounded; // is the character on the ground
    public float fallDisable = 0.2f; // Time the character is fall till controls diabled
    private float fallTimer = 0.0f; // time the character has been falling

    //Activation when the object is created
    private void Start()
    {
        charControl = gameObject.GetComponent<CharacterController>();
    }

    // Updating on each frame
    private void Update()
    {
        // Getting movement Direction and moveing the character
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        move = transform.TransformDirection(move); // transform direction to relative to camera direction

        // Resetting values when touching the ground
        grounded = charControl.isGrounded;
	
	// Determining if the character is falling or walking down a incline
	if(!grounded)
	{
	    fallTimer += Time.deltaTime;
	}
	else
	{
	   fallTimer = 0.0f;
	}

	
	if(fallTimer < fallDisable)
	{
       	    if(playerVelocity.y < 0)
            {
		airJump = 0;
		playerVelocity = Vector3.zero;
            }
            // move when on the ground
            charControl.Move(move * Time.deltaTime * playerSpeed);
	}

        // UpDown Movement of player
        if (Input.GetButtonDown("Jump") && airJump < jumpMax)
        {
            airJump++; // counting jumps
            playerVelocity = move; // direction the player is moving in the
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity); // attempting to make an arc in movement
        }

        playerVelocity.y += gravity * Time.deltaTime; // accellerating the player down
        charControl.Move(playerVelocity * Time.deltaTime * playerSpeed); // moving the player in preconfigured direction
    }
}

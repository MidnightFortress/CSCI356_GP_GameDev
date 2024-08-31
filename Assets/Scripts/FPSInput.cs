using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))] // enforces dependency on character controller
[AddComponentMenu("Control Script/FPS Input")]  // add to the Unity editor's component menu
public class FPSInput : MonoBehaviour
{
    // movement sensitivity
    public float speed = 6.0f;

    // reference to the character controller
    private CharacterController charController;

    // jump related variables
    public float gravity = -9.8f;
    public float jumpSpeed = 15.0f;
    private float terminalVelocity = -20.0f;

    private float vertSpeed; // variable that will change player veticle speed based on state (i.e. jumping, falling, on ground)

    // Start is called before the first frame update
    void Start()
    {
        // get the character controller component
        charController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // input functionality to employ jumping
        if (Input.GetButtonDown("Jump") && charController.isGrounded)
        {
            // if player on ground allow jumping
            vertSpeed = jumpSpeed;
        }
        else if (!charController.isGrounded)
        {
            // if player not grounded i.e., cannot jump
            vertSpeed += gravity * 3 * Time.deltaTime; // increase acceleration due to gravity

            // ensure fall speed does not exceed terminal velocity
            if (vertSpeed < terminalVelocity)
            {
                // set vert speed to terminal velocity
                vertSpeed = terminalVelocity;
            }
        }


        // changes based on WASD keys
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);

        // make diagonal movement consistent
        movement = Vector3.ClampMagnitude(movement, speed);

        // add the vert speed to the y dimension of the movement vector
        movement.y = vertSpeed;

        // ensure movement is independent of the framerate
        movement *= Time.deltaTime;

        // transform from local space to global space
        movement = transform.TransformDirection(movement);

        // pass the movement to the character controller
        charController.Move(movement);
    }
}

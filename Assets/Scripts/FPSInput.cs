using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script")]
public class FPSInput : MonoBehaviour
{
    // Movement  speed
    public float moveSpeed = 9f;
    private float horizontalSpeed = 0.0f;
    private float verticalSpeed = 0.0f;

    // Gravity (acceleration)
    public float gravity = -9.8f;
    public float jumpSpeed = 9.8f; // jump speed
    private float upDownSpeed = 0; // vertical speed

    // Number of Jumps before touching ground
    public int airJump = 1; // max jumps befoe needing to touch the ground
    public int jumpCount = 0; // the current number of jumps

    // Character Controller
    private CharacterController characterControl;

    // Start is called before the first frame update
    void Start()
    {
        //getting the character controller compnent
        characterControl = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Getting key input
        if (characterControl.isGrounded)
        {
            horizontalSpeed = Input.GetAxis("Horizontal") * moveSpeed; // horizontal input
            verticalSpeed = Input.GetAxis("Vertical") * moveSpeed; //vertical input
        }

        // determining if jump
        if (Input.GetButtonDown("Jump") && jumpCount < airJump) // jump input
        {
            // so you can change directions in mid air using the jump
            if (!characterControl.isGrounded)
            {
                horizontalSpeed = Input.GetAxis("Horizontal") * moveSpeed; // horizontal input
                verticalSpeed = Input.GetAxis("Vertical") * moveSpeed; //vertical input
            }

            upDownSpeed = (jumpSpeed); // + gravity); // adding upward speed... do i really need to include gravity... its speed not a force... so no... i guess
            jumpCount++; // jump count to count the number of times the player has jumped in mid air
        }
        else if (!characterControl.isGrounded) // only apply this when in the air otherwise player keeps acellerating faster into the ground when on the ground
        {
            upDownSpeed = upDownSpeed + gravity * Time.deltaTime; // to apply a gradual accrelleration down
        }

        if (jumpCount != 0 && characterControl.isGrounded) // reseeting the air jump counter when touching the ground... need to figure out how to not have the reset when touching walls
        {
            jumpCount = 0;
        }

        // applying movement
        Vector3 move = new Vector3(horizontalSpeed, upDownSpeed, verticalSpeed);

        // How the shit Do I make Diagonals work.....
        move = Vector3.ClampMagnitude(move, moveSpeed); // I think this works... will this make diagonals the same speed...

        move *= Time.deltaTime; // Making movement independent of frame rate

        move = transform.TransformDirection(move); // converting movement ot global space

        characterControl.Move(move); // passing to the Charicter Controller

    }

    private void OnCollisionEnter(Collision collision)
    {
        jumpCount = 0;
        upDownSpeed = 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]
public class FPSInput : MonoBehaviour
{
    public float speed = 8.0f;
    private CharacterController character;
    [SerializeField] public float jumpSpeed = 15.0f;
    public float gravity = -9.8f;
    public float terminalVelocity = -20.0f;
    private float vertSpeed;
    public int jumpCount = 0;
    [SerializeField] public int maxJump = 2;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float dx = Input.GetAxis("Horizontal") * speed;
        float dz = Input.GetAxis("Vertical") * speed;
        Vector3 movementVec = new Vector3(dx, 0, dz);
        // puting the delta x and y in the vector

        movementVec = Vector3.ClampMagnitude(movementVec, speed);
        // have to clamp the movementr speed so there is no exponential growt

        movementVec = transform.TransformDirection(movementVec);

        if (character.isGrounded)
        {
            jumpCount = 0;
            vertSpeed = 0;
        }

        if (Input.GetButtonDown("Jump") && jumpCount <= maxJump)
        {
            vertSpeed += jumpSpeed;
            jumpCount++;
        }
        else if (!character.isGrounded)
        {
            vertSpeed += gravity * 5 * Time.deltaTime;
            if (vertSpeed < terminalVelocity)
            {
                vertSpeed = terminalVelocity;

            }
        }
        // this is the double jump logic, it doesnt allow for more than 2 jumps, will reset the count on the ground. 
        // gets the power and does the math to clamp the sped for terminal velocity

        movementVec.y = vertSpeed;

        movementVec *= Time.deltaTime;
        character.Move(movementVec);
    }

}

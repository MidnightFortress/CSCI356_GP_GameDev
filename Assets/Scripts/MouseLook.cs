using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Mouse Look")]
public class MouseLook: MonoBehaviour
{
    // how quickly to look arouund
    public enum RotationA
    {
        mXAndY = 0,
        mX = 1,
        mY = 2,
    }
    // the options that can be given

    public RotationA rotationA = RotationA.mX;

    //sensitivity
    public float senH = 10.0f;
    public float senV = 10.0f;

    // max and min looking angle
    public float minV = -45.0f;
    public float maxV = 45.0f;

    // getting the pitch
    private float vRot = 0;


    // Start is called before the first frame update
    void Start()
    {
        Rigidbody body = GetComponent<Rigidbody>();
        if (body != null)
        {
            body.freezeRotation = true;
        }
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per framea
    void Update()
    {
        if (rotationA == RotationA.mX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * senH, 0);
            // making the rotation for movement x
        }
        else if (rotationA == RotationA.mY)
        {
            // if enum my is used
            vRot -= Input.GetAxis("Mouse Y") * senV;
            vRot = Mathf.Clamp(vRot, minV, maxV);
            // movement of the mouse and the axis of the y-axis, clamps it at the min and max vert and horizontal
            float hRot = transform.localEulerAngles.y;

            transform.localEulerAngles = new Vector3(vRot, hRot, 0);
            // sets the local rotation using the ver and hor rotation so there is no roll 

        }
        else
        // used for option y and x
        {
            vRot -= Input.GetAxis("Mouse Y") * senV;
            vRot = Mathf.Clamp(vRot, minV, maxV);
            // same as the top ones

            float delta = Input.GetAxis("Mouse X") * senH;
            float hRot = transform.localEulerAngles.y + delta;
            // updates the yaw based on mouse movement

            transform.localEulerAngles = new Vector3(vRot, hRot, 0);
            // no roll
        }
    }
}

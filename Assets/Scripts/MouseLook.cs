using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Mouse Look")]
public class MouseLook: MonoBehaviour
{
    // how quickly to look arouund
    public float yawSensitivity = 9.0f; // Left Right
    public float pitchSensitivity = 9.0f; // Up Down

    // pitch limits when looking
    public float maxPitch = 45.0f; // Up?
    public float minPitch = -45.0f; // Down?

    private float pitchRotation = 0; // to input Pitch so it can be clamped

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
        // Change Pitch Angle
        pitchRotation -= Input.GetAxis("Mouse Y") * pitchSensitivity;
        pitchRotation = Mathf.Clamp(pitchRotation, minPitch, maxPitch);

        // Change Yaw Angle
        float yawChange = Input.GetAxis("Mouse X") * yawSensitivity;
        float yawRotation = transform.localEulerAngles.y + yawChange;

        // Setting Angles
        transform.localEulerAngles = new Vector3(pitchRotation, yawRotation, 0);
    }
}

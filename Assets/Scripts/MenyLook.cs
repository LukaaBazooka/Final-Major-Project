using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenyLook : MonoBehaviour
{
    float rotationX = 0f;
    float rotationY = 0f;

    public Camera mainCamera;


    public Vector2 sensitivity = Vector2.one * 150;
    public float dampingFactor = 0.6f; // Adjust damping factor as needed
    public float sensitivityDecayRate = 0.95f; // Adjust decay rate as needed
    public float smoothRange = 10f; // Adjust the range for smooth interpolation
    public float delayBeforeLook = 1f; // Delay before being able to look around

    public float bopFrequency = 5f; // Frequency of the bop effect
    public float bopAmplitude = 1f; // Amplitude of the bop effect
    public float fovBase = 60f; // Base field of view
    public float fovOffset = 10f; // Field of view offset from base

    bool canLook = false;

    void Start()
    {
        Invoke("EnableLooking", delayBeforeLook);
    }

    void FixedUpdate()
    {
        if (!canLook)
            return;

        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Check if mouse moved
        if (mouseX != 0 || mouseY != 0)
        {
            // Reset sensitivity
            sensitivity = Vector2.one * 100;
        }

        // Apply sensitivity and damping
        rotationY += mouseX * Time.deltaTime * sensitivity.x;
        rotationX -= mouseY * Time.deltaTime * -1 * sensitivity.y;

        // Apply damping
        sensitivity *= dampingFactor;

        // Decay sensitivity over time
        sensitivity *= sensitivityDecayRate;

        // Smooth near bounds
        rotationX = SmoothNearBounds(rotationX, -10f, 10f);
        rotationY = SmoothNearBounds(rotationY, -10f, 10f);

        // Clamp to prevent further rotation
        rotationX = Mathf.Clamp(rotationX, -20f, 20f);
        rotationY = Mathf.Clamp(rotationY, -20f, 20f);

        // Update rotation
        transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);

        mainCamera.fieldOfView = fovBase + Mathf.Sin(Time.time * bopFrequency) * bopAmplitude;
    }

    void EnableLooking()
    {
        canLook = true;
    }

    float SmoothNearBounds(float angle, float minAngle, float maxAngle)
    {
        float t = Mathf.InverseLerp(minAngle, maxAngle, angle);
        float smoothT = Mathf.Clamp01((1f - Mathf.Abs(t - 0.5f)) * (1f / smoothRange));
        float smoothFactor = Mathf.Lerp(0f, 1f, smoothT);

        float smoothAngle = Mathf.Lerp(minAngle, maxAngle, t);
        return Mathf.Lerp(angle, smoothAngle, smoothFactor);
    }
}
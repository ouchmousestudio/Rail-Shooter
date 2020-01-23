using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{

    [Header("Screen position")]
    [Tooltip("Range on X axis")] [SerializeField] float xRange = 5f;
    [Tooltip("Range on Y axis")] [SerializeField] float yRange = 3.5f;

    [Header("Speed")]
    [SerializeField] float xSpeed = 4f;
    [SerializeField] float posPitchFactor = -5f;
    [SerializeField] float posYawFactor = 5f;
    [SerializeField] float controlPitchFactor = -20f;
    [SerializeField] float controlRollFactor = -20f;

    float xThrow, yThrow;
    bool controlEnabled = true;

    // Update is called once per frame
    void Update()
    {
        if (controlEnabled)
        {
            ProcessTranslation();
            ProcessRotation();
        }
    }

    private void ProcessRotation()
    {
        float pitch = transform.localPosition.y * posPitchFactor + yThrow * controlPitchFactor;
        float yaw = transform.localPosition.x * posYawFactor;
        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    //Call by string reference (collissionHandler.cs)
    void OnPlayerDeath()
    {
        print("Controls Frozen"); 
        controlEnabled = false;
    }

    private void ProcessTranslation()
    {

        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        float xOffset = xThrow * xSpeed * Time.deltaTime;
        float yOffset = yThrow * xSpeed * Time.deltaTime;


        float rawXPos = transform.localPosition.x + xOffset;
        float xPos = Mathf.Clamp(rawXPos, -xRange, xRange);
        float rawYPos = transform.localPosition.y + yOffset;
        float yPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(xPos, yPos, transform.localPosition.z);
    }
}

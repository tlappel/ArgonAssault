using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] float controlSpeed = 15f;
    [SerializeField] float controlXRange = 10f;
    [SerializeField] float controlYRange = 7f;
    [SerializeField] float positionPitchFactor = -3f;
    [SerializeField] float controlPitchFactor = -15f;
    
    [SerializeField] float positionYawFactor = 2;
    [SerializeField] float controlYawFactor = 15f;
    [SerializeField] float controlRollFactor = -20f;
    //float pitch;
    //float yaw;
    //float roll;
    float xThrow,yThrow;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
    }

    void ProcessRotation()
    {
        float pitchFromPositon = transform.localPosition.y * positionPitchFactor;
        float pitchFromControl = yThrow * controlPitchFactor;
        float pitch = pitchFromPositon + pitchFromControl;
        float yawFromPosition = transform.localPosition.x * positionYawFactor;
        float yawFromControl = xThrow * controlYawFactor;
        float yaw = yawFromPosition + yawFromControl;
        float roll = xThrow * controlRollFactor;
        transform.localRotation = Quaternion.Euler(pitch,yaw,roll);
    }
    private void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");
        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedX = Mathf.Clamp(rawXPos, -controlXRange, controlXRange);
        float clampedY = Mathf.Clamp(rawYPos, -controlYRange, controlYRange);
        transform.localPosition = new Vector3(clampedX, clampedY, transform.localPosition.z);
    }
}

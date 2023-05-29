using UnityEngine;

public class ChildObjectMovement : MonoBehaviour
{
    public Transform targetObject;                 // Reference to the target object (parent object)
    public Transform childObject;                  // Reference to the child object
    public float rotationSpeed = 180f;             // Speed of rotation
    public float forwardMovementFrequency = 2f;    // Frequency of the forward movement
    public float forwardMovementAmplitude = 0.5f;  // Amplitude of the forward movement
    public float crankAmplitude = 0.5f;            // Amplitude of the cranking motion
    public float crankOffset = 1f;                  // Offset for the cranking motion
    public float bobFrequency = 2f;                // Frequency of the bobbing motion
    public float bobAmplitude = 0.1f;              // Amplitude of the bobbing motion
    public float pumpFrequency = 2f;               // Frequency of the pumping motion
    public float pumpAmplitude = 0.1f;             // Amplitude of the pumping motion
    public float backAndForthFrequency = 1f;        // Frequency of the back-and-forth motion
    public float backAndForthAmplitude = 0.5f;      // Amplitude of the back-and-forth motion
    public float twistFrequency = 2f;               // Frequency of the twisting motion
    public float twistAmplitude = 45f;              // Amplitude of the twisting motion

    private Quaternion initialChildRotation;       // Initial rotation of the child object
    private float forwardMovementTime;             // Time value for the forward movement
    private float bobTime;                         // Time value for the bobbing motion
    private float pumpTime;                        // Time value for the pumping motion
    private float backAndForthTime;                 // Time value for the back-and-forth motion
    private float twistTime;                        // Time value for the twisting motion
    private bool isRecoiling;                       // Flag to track recoil state

    private void Start()
    {
        // Store the initial rotation of the child object relative to the target object
        initialChildRotation = childObject.localRotation * Quaternion.Inverse(targetObject.localRotation);
    }

    private void Update()
    {
        if (!targetObject.GetComponent<MeshRenderer>().enabled)
        {
            // Increment the time value based on the forward movement frequency
            forwardMovementTime += forwardMovementFrequency * Time.deltaTime;

            // Calculate the forward offset for the movement
            float forwardOffset = Mathf.Sin(forwardMovementTime) * forwardMovementAmplitude;

            // Calculate the crank rotation
            float crankRotation = Mathf.Sin(forwardMovementTime) * crankAmplitude;

            // Increment the time value based on the bobbing frequency
            bobTime += bobFrequency * Time.deltaTime;

            // Calculate the vertical offset for the bobbing motion
            float verticalOffset = Mathf.Sin(bobTime) * bobAmplitude;

            // Increment the time value based on the pumping frequency
            pumpTime += pumpFrequency * Time.deltaTime;

            // Calculate the pumping offset for the up and down motion
            float pumpingOffset = Mathf.Sin(pumpTime) * pumpAmplitude;

            // Increment the time value based on the back-and-forth frequency
            backAndForthTime += backAndForthFrequency * Time.deltaTime;

            // Calculate the back-and-forth offset along the z-axis
            float backAndForthOffset = Mathf.Sin(backAndForthTime) * backAndForthAmplitude;

            // Modify the backAndForthOffset to move towards and away from the arms
            backAndForthOffset += Mathf.Sign(backAndForthOffset) * forwardOffset;

            // Increment the time value based on the twist frequency
            twistTime += twistFrequency * Time.deltaTime;

            // Calculate the twisting rotation around the local x-axis
            float twistRotation = Mathf.Sin(twistTime) * twistAmplitude;

            // Apply recoil if isRecoiling flag is true
            if (isRecoiling)
            {
                float recoilAmplitude = 0.5f; // Adjust the recoil amplitude as needed
                float recoilOffset = Mathf.Sin(Time.time * 10f) * recoilAmplitude;

                forwardOffset += recoilOffset;
            }

            // Update the position and rotation of the child object relative to the target object
            Vector3 newPosition = targetObject.localPosition + targetObject.forward * forwardOffset + targetObject.up * pumpingOffset + targetObject.up * verticalOffset + targetObject.right * backAndForthOffset;
            Quaternion newRotation = targetObject.localRotation * Quaternion.Euler(0f, twistRotation, 0f) * initialChildRotation;

            // Apply the new position and rotation to the child object
            childObject.localPosition = newPosition;
            childObject.localRotation = newRotation;
        }
    }

    public void StartRecoil()
    {
        isRecoiling = true;
    }

    public void StopRecoil()
    {
        isRecoiling = false;
    }
}

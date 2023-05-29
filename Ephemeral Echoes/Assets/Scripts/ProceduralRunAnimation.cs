using UnityEngine;

public class ProceduralRunAnimation : MonoBehaviour
{
    public Transform leftArmBone;       // Reference to the left arm bone
    public Transform rightArmBone;      // Reference to the right arm bone
    public float bobFrequency = 5f;     // Frequency of the arm bobbing motion
    public float bobAmplitude = 0.05f;  // Amplitude of the arm bobbing motion
    public float armRotationSpeed = 180f; // Speed of arm rotation

    private Vector3 initialLeftArmPosition;   // Initial position of the left arm bone
    private Vector3 initialRightArmPosition;  // Initial position of the right arm bone
    private float time;                       // Time value for the bobbing motion
    private bool isAnimating = false;         // Flag to track animation state

    private void Start()
    {
        // Store the initial positions of the arm bones
        initialLeftArmPosition = leftArmBone.localPosition;
        initialRightArmPosition = rightArmBone.localPosition;
    }

    private void Update()
    {
        if (isAnimating)
        {
            // Increment the time value based on the bobbing frequency
            time += bobFrequency * Time.deltaTime;

            // Calculate the vertical offset for the arm bobbing motion
            float verticalOffset = Mathf.Sin(time) * bobAmplitude;

            // Update the positions of the arm bones with the bobbing motion
            leftArmBone.localPosition = initialLeftArmPosition + Vector3.up * verticalOffset;
            rightArmBone.localPosition = initialRightArmPosition + Vector3.up * verticalOffset;

            // Rotate the arms around the local y-axis for a running motion
            float rotationAngle = Mathf.Sin(time) * armRotationSpeed * Time.deltaTime;
            leftArmBone.Rotate(Vector3.up, rotationAngle);
            rightArmBone.Rotate(Vector3.up, -rotationAngle);
        }
    }

    public void StartAnimation()
    {
        isAnimating = true;

        // Deactivate the child objects rigLayerRight and rigLayerLeft
        foreach (Transform child in transform)
        {
            if (child.name == "RigLayerRight" || child.name == "RigLayerLeft")
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    public void StopAnimation()
    {
        isAnimating = false;

        // Activate the child objects rigLayerRight and rigLayerLeft
        foreach (Transform child in transform)
        {
            if (child.name == "RigLayerRight" || child.name == "RigLayerLeft")
            {
                child.gameObject.SetActive(true);
            }
        }
    }
}

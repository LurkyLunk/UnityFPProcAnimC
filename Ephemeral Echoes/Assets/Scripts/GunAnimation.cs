using UnityEngine;

public class GunAnimation : MonoBehaviour
{
    public float normalBobbingSpeed = 1f;
    public float normalBobbingAmount = 0.05f;
    public float movingBobbingSpeed = 2f;
    public float movingBobbingAmount = 0.1f;
    public float tiltAngle = 10f;
    public float tiltSpeed = 5f;

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private bool isMoving = false;

    private void Start()
    {
        initialPosition = transform.localPosition;
        initialRotation = transform.localRotation;
    }

    private void Update()
    {
        // Determine if the character is moving
        isMoving = Input.GetAxis("Vertical") != 0f || Input.GetAxis("Horizontal") != 0f;

        // Select the appropriate bobbing parameters based on the character's movement
        float bobbingSpeed = isMoving ? movingBobbingSpeed : normalBobbingSpeed;
        float bobbingAmount = isMoving ? movingBobbingAmount : normalBobbingAmount;

        // Calculate the vertical bobbing motion
        float verticalBob = Mathf.Sin(Time.time * bobbingSpeed) * bobbingAmount;

        // Apply the bobbing motion to the gun's local position
        Vector3 newPosition = initialPosition + new Vector3(0f, verticalBob, 0f);
        transform.localPosition = newPosition;

        // Calculate the tilt rotation based on player's turning direction
        float turnAmount = Input.GetAxis("Horizontal");
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, -turnAmount * tiltAngle);

        // Smoothly rotate the gun towards the target rotation
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, tiltSpeed * Time.deltaTime);
    }

    private void LateUpdate()
    {
        // Reset the gun's rotation to the initial resting position
        transform.localRotation = Quaternion.Lerp(transform.localRotation, initialRotation, tiltSpeed * Time.deltaTime);
    }
}

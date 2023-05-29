using UnityEngine;

public class GunCameraBasedAnimation : MonoBehaviour
{
    public float maxTiltAngle = 10f;
    public float maxPitchAngle = 15f;
    public float tiltSpeed = 5f;
    public float pitchSpeed = 5f;
    public float swayAmount = 0.05f;
    public float swaySpeed = 5f;

    private Quaternion initialRotation;
    private Transform cameraTransform;

    private void Start()
    {
        initialRotation = transform.localRotation;

        // Get the camera's transform using the tag "MainCamera"
        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        if (mainCamera != null)
        {
            cameraTransform = mainCamera.transform;
        }
        else
        {
            Debug.LogError("MainCamera not found in the scene. Make sure to tag your camera as 'MainCamera'.");
        }
    }

    private void Update()
    {
        // Calculate the tilt rotation based on the camera's rotation
        if (cameraTransform != null)
        {
            float cameraRotationY = cameraTransform.localRotation.eulerAngles.y;
            float targetTilt = Mathf.Clamp(cameraRotationY, -maxTiltAngle, maxTiltAngle);
            Quaternion targetRotation = Quaternion.Euler(0f, targetTilt, 0f);

            // Smoothly rotate the gun towards the target tilt rotation
            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, tiltSpeed * Time.deltaTime);

            // Calculate the pitch rotation based on the camera's rotation
            float cameraRotationX = cameraTransform.localRotation.eulerAngles.x;
            float targetPitch = Mathf.Clamp(cameraRotationX, -maxPitchAngle, maxPitchAngle);
            Quaternion targetPitchRotation = Quaternion.Euler(-targetPitch, 0f, 0f);

            // Smoothly rotate the gun towards the target pitch rotation
            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetPitchRotation, pitchSpeed * Time.deltaTime);

            // Calculate the sway rotation based on mouse movement or camera rotation
            float swayRotationZ = -Input.GetAxis("Mouse X") * swayAmount;
            Quaternion swayRotation = Quaternion.Euler(0f, 0f, swayRotationZ);

            // Smoothly rotate the gun towards the sway rotation
            transform.localRotation = Quaternion.Lerp(transform.localRotation, transform.localRotation * swayRotation, swaySpeed * Time.deltaTime);
        }
    }

    private void LateUpdate()
    {
        // Reset the gun's rotation to the initial resting position
        transform.localRotation = Quaternion.Lerp(transform.localRotation, initialRotation, tiltSpeed * Time.deltaTime);
    }
}

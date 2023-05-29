using UnityEngine;

public class GunFireAnimation : MonoBehaviour
{
    public Transform gunTransform;                   // Reference to the gun model transform
    public float recoilDistance = 0.5f;              // Distance to move the gun back during recoil
    public float recoilSpeed = 10f;                  // Speed of the recoil animation
    public float recoverySpeed = 5f;                 // Speed of the recovery animation
    public AudioClip shotSound;                      // Sound to play when the gun is fired

    private AudioSource audioSource;                 // Reference to the AudioSource component
    private Vector3 initialGunPosition;              // Initial position of the gun model
    private Quaternion initialGunRotation;           // Initial rotation of the gun model
    private bool isRecoiling = false;                 // Flag to track if the gun is in recoil animation
    private bool canFire = true;                      // Flag to track if the gun can fire
    private int ammoCount = 10;                       // Current ammo count

    // Reference to the Gun class
    public Gun gun;

    private void Start()
    {
        // Store the initial position and rotation of the gun
        initialGunPosition = gunTransform.localPosition;
        initialGunRotation = gunTransform.localRotation;

        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isRecoiling && canFire)
        {
            // Start the recoil animation
            StartRecoilAnimation();

            // Play the shot sound
            if (shotSound != null)
            {
                audioSource.PlayOneShot(shotSound);
            }

            // Decrease ammo count or perform any other logic to expend ammo
            // Implement your own ammo management logic here
            DecreaseAmmoCount();
        }
    }

    private void DecreaseAmmoCount()
    {
        // Perform any logic to decrease the ammo count
        // Implement your own ammo management logic here
        ammoCount--;  // Decrement the ammo count by 1

        // Check if the ammo count is zero
        // If it is, set canFire to false to prevent firing
        if (ammoCount <= 0)
        {
            canFire = false;
        }

        // Update the ammo count in the Gun class
        gun.SetCurrentAmmo(ammoCount);
    }

    private void StartRecoilAnimation()
    {
        if (isRecoiling) return;

        isRecoiling = true;

        // Move the gun back during recoil
        Vector3 recoilPosition = initialGunPosition - gunTransform.forward * -recoilDistance;
        Quaternion recoilRotation = initialGunRotation;

        // Calculate the time it takes to reach the recoil position
        float recoilTime = recoilDistance / recoilSpeed;

        // Perform the recoil animation
        StartCoroutine(PerformRecoilAnimation(recoilPosition, recoilRotation, recoilTime));
    }

    private System.Collections.IEnumerator PerformRecoilAnimation(Vector3 targetPosition, Quaternion targetRotation, float animationTime)
    {
        float elapsedTime = 0f;

        while (elapsedTime < animationTime)
        {
            // Calculate the interpolation factor based on the elapsed time
            float t = elapsedTime / animationTime;

            // Interpolate the position and rotation towards the target values
            gunTransform.localPosition = Vector3.Lerp(initialGunPosition, targetPosition, t);
            gunTransform.localRotation = Quaternion.Lerp(initialGunRotation, targetRotation, t);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Ensure the gun ends up at the target position and rotation
        gunTransform.localPosition = targetPosition;
        gunTransform.localRotation = targetRotation;

        // Start the recovery animation
        StartRecoveryAnimation();
    }

    private void StartRecoveryAnimation()
    {
        // Calculate the time it takes to reach the initial position and rotation
        float recoveryTime = Vector3.Distance(gunTransform.localPosition, initialGunPosition) / recoverySpeed;

        // Perform the recovery animation
        StartCoroutine(PerformRecoveryAnimation(recoveryTime));
    }

    private System.Collections.IEnumerator PerformRecoveryAnimation(float animationTime)
    {
        float elapsedTime = 0f;
        Vector3 initialPosition = gunTransform.localPosition;
        Quaternion initialRotation = gunTransform.localRotation;

        while (elapsedTime < animationTime)
        {
            // Calculate the interpolation factor based on the elapsed time
            float t = elapsedTime / animationTime;

            // Interpolate the position and rotation towards the initial values
            gunTransform.localPosition = Vector3.Lerp(initialPosition, initialGunPosition, t);
            gunTransform.localRotation = Quaternion.Lerp(initialRotation, initialGunRotation, t);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Ensure the gun ends up at the initial position and rotation
        gunTransform.localPosition = initialGunPosition;
        gunTransform.localRotation = initialGunRotation;

        isRecoiling = false;
    }

    public void ResetAnimation()
    {
        // Reset animation flags and ammo count
        isRecoiling = false;
        canFire = true;
        ammoCount = 10;
    }
}

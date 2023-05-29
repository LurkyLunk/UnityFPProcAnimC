using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPController : MonoBehaviour
{   

    public Gun gunScript; // Reference to the Gun script
    public GameObject cam;
    public FootstepController footstepController; // Reference to the FootstepController component
    public PickupManager pickupManager; // Reference to the PickupManager component

    float speed = 0.1f;
    float Xsensitivity = 5;
    float Ysensitivity = 5;
    float MinimumX = -90;
    float MaximumX = 90;
    Rigidbody rb;
    CapsuleCollider capsule;
    Quaternion cameraRot;
    Quaternion characterRot;

    bool cursorIsLocked = true;
    bool lockCursor = true;
    bool isJumping = false; // Flag to track jumping state
    bool wasJumping = false; // Flag to track previous jumping state

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        capsule = GetComponent<CapsuleCollider>();

        cameraRot = cam.transform.localRotation;
        characterRot = transform.localRotation;

        pickupManager = GetComponent<PickupManager>();

        // Set the Gun script reference in the PickupManager
        pickupManager.SetGun(gunScript);
    
    }

    // Update is called once per frame
    void Update()
    {
        // Check for jump input
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.AddForce(0, 300, 0);
            footstepController.PlayJumpSound(); // Play the jump sound
            isJumping = true; // Set jumping flag to true
        }

        // Check for item pickups
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryPickupItem();
        }

        
        if (Input.GetMouseButtonDown(0))
        {
            if (gunScript != null)
            {
                gunScript.FireGun();
            }
        }
    }

   

    void TryPickupItem()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 2f); // Adjust the radius as needed

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Ammo"))
            {
                // Handle ammo pickup
                AmmoPickup ammoPickup = collider.GetComponent<AmmoPickup>();
                if (ammoPickup != null)
                {
                    if (ammoPickup.ammoAmount > 0) // Check if ammo is available
                    {
                        pickupManager.PickupAmmo(ammoPickup, gunScript); // Pass the instance of the AmmoPickup script
                    }
                }
            }
            else if (collider.CompareTag("Health"))
            {
                // Handle health pickup
                HealthPickup healthPickup = collider.GetComponent<HealthPickup>();
                if (healthPickup != null)
                {
                    if (healthPickup.healthAmount > 0) // Check if health is available
                    {
                        pickupManager.PickupHealth(healthPickup); // Pass the instance of the HealthPickup script
                    }
                }
            }
        }
    }

    void FixedUpdate()
    {
        float yRot = Input.GetAxis("Mouse X") * Ysensitivity;
        float xRot = Input.GetAxis("Mouse Y") * Xsensitivity;

        cameraRot *= Quaternion.Euler(-xRot, 0, 0);
        characterRot *= Quaternion.Euler(0, yRot, 0);

        cameraRot = ClampRotationAroundXAxis(cameraRot);

        this.transform.localRotation = characterRot;
        cam.transform.localRotation = cameraRot;

        float x = Input.GetAxis("Horizontal") * speed;
        float z = Input.GetAxis("Vertical") * speed;

        transform.position += cam.transform.forward * z + cam.transform.right * x;

        UpdateCursorLock();

        // Check if the character is no longer jumping (has landed)
        if (wasJumping && !isJumping && IsGrounded())
        {
            footstepController.PlayLandSound(); // Play the land sound
        }

        wasJumping = isJumping; // Update the previous jumping state
        isJumping = false; // Reset jumping flag
    }

    Quaternion ClampRotationAroundXAxis(Quaternion q)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);
        angleX = Mathf.Clamp(angleX, MinimumX, MaximumX);
        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        return q;
    }

    bool IsGrounded()
    {
        RaycastHit hitInfo;
        if (Physics.SphereCast(transform.position, capsule.radius, Vector3.down, out hitInfo,
            (capsule.height / 2f) - capsule.radius + 0.1f))
        {
            return true;
        }
        return false;
    }

    public void SetCursorLock(bool value)
    {
        lockCursor = value;
        if (!lockCursor)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void UpdateCursorLock()
    {
        if (lockCursor)
            InternalLockUpdate();
    }

    public void InternalLockUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
            cursorIsLocked = false;
        else if (Input.GetMouseButtonUp(0))
            cursorIsLocked = true;

        if (cursorIsLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (!cursorIsLocked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public bool IsCharacterMoving()
    {
        // Add your logic to determine if the character is moving
        // For example, you can check the magnitude of the input axis values
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        return Mathf.Abs(horizontalInput) > 0 || Mathf.Abs(verticalInput) > 0;
    }

    public bool IsCharacterJumping()
    {
        // Add your logic to determine if the character is jumping
        // You can use the isJumping flag or any other appropriate condition
        return isJumping;
    }
}

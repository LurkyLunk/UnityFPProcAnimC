using UnityEngine;

public class DisarmController : MonoBehaviour
{
    public GameObject gunObject;  // Reference to the gun GameObject
    public ProceduralRunAnimation proceduralRunAnimation;  // Reference to the ProceduralRunAnimation script
    public GunFireAnimation gunFireAnimation;  // Reference to the GunFireAnimation script

    private MeshRenderer gunMeshRenderer;  // Reference to the gun's MeshRenderer component
    private Transform[] armTransforms;  // Array to store references to the arm transforms
    private Vector3 disarmPosition = new Vector3(0.056f, -0.691f, 0.385f);  // Disarm position
    private Quaternion disarmRotation = Quaternion.Euler(84.173f, 29.784f, 21.989f);  // Disarm rotation
    private Vector3 initialGunPosition = new Vector3(0.07900001f, -0.18f, 0.642f);  // Initial gun position
    private Quaternion initialGunRotation = Quaternion.Euler(0, 0, 0);  // Initial gun rotation
    private bool isDisarmed = true;  // Flag to track if the gun is disarmed

    private void Start()
    {
        gunMeshRenderer = gunObject.GetComponent<MeshRenderer>(); // Get the gun's MeshRenderer component
        armTransforms = GetComponentsInChildren<Transform>(); // Get the arm transforms including the gun

        // Set the disarm position and rotation as the initial gun position and rotation
        gunObject.transform.position = disarmPosition;
        gunObject.transform.rotation = disarmRotation;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isDisarmed)
            {
                Rearm();
            }
            else
            {
                Disarm();
            }
        }
    }

    private void Disarm()
    {
        // Reset the position and rotation of the gun to its initial values
        gunObject.transform.position = initialGunPosition;
        gunObject.transform.rotation = initialGunRotation;

        // Enable the ProceduralRunAnimation script on the arms
        foreach (Transform armTransform in armTransforms)
        {
            ProceduralRunAnimation armAnimation = armTransform.GetComponent<ProceduralRunAnimation>();
            if (armAnimation != null)
            {
                armAnimation.enabled = true;
            }
        }

        // Deactivate the gun's mesh renderer
        gunMeshRenderer.enabled = false;

        // Disable the GunFireAnimation script
        gunFireAnimation.enabled = false;

        isDisarmed = true;
    }

    private void Rearm()
    {
        // Move the gun to the disarm position and rotation
        gunObject.transform.position = disarmPosition;
        gunObject.transform.rotation = disarmRotation;

        // Disable the ProceduralRunAnimation script on the arms
        foreach (Transform armTransform in armTransforms)
        {
            ProceduralRunAnimation armAnimation = armTransform.GetComponent<ProceduralRunAnimation>();
            if (armAnimation != null)
            {
                armAnimation.enabled = false;
            }
        }

        // Find the left and right hand objects
        GameObject leftHandObject = GameObject.Find("GunGripL");  // Replace "LeftHand" with the actual name or tag of the left hand object
        GameObject rightHandObject = GameObject.Find("GunGripR");  // Replace "RightHand" with the actual name or tag of the right hand object

        if (leftHandObject != null && rightHandObject != null)
        {
            // Parent the hands to the gun
            leftHandObject.transform.SetParent(gunObject.transform);
            rightHandObject.transform.SetParent(gunObject.transform);

            // Set the local positions and rotations of the hands relative to the gun
            leftHandObject.transform.localPosition = new Vector3(-0.005300001f, -0.007559985f, -0.01259999f);  // Adjust the values as needed
            leftHandObject.transform.localRotation = Quaternion.Euler(-86.921f, 212.604f, -392.476f);

            rightHandObject.transform.localPosition = new Vector3(0f, -0.0042f, -0.0203f);  // Adjust the values as needed
            rightHandObject.transform.localRotation = Quaternion.Euler(-80.63f, 54.012f, 132.603f);
        }

        // Activate the gun's mesh renderer
        gunMeshRenderer.enabled = true;

        // Enable the GunFireAnimation script
        gunFireAnimation.enabled = true;

        isDisarmed = false;
    }
}

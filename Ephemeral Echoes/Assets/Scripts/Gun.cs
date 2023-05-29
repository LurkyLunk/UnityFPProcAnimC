using UnityEngine;

public class Gun : MonoBehaviour
{
    public int maxAmmo = 10; // Maximum ammo count
    private int currentAmmo; // Current ammo count

    public void Start()
    {
        currentAmmo = maxAmmo; // Set the initial ammo count
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Left mouse button is clicked, fire the gun
            FireGun();
        }
    }

    public void FireGun()
    {
        if (currentAmmo > 0)
        {
            // Perform firing logic here, such as spawning a bullet or applying damage to a target

            // Decrease the ammo count
            currentAmmo--;

            // Optionally, you can play a sound or show visual effects when firing the gun

            Debug.Log("Ammo: " + currentAmmo);
        }
        else
        {
            // Out of ammo, handle accordingly (e.g., play a click sound or show a message)
            Debug.Log("Out of ammo!");
        }
    }

    public int GetCurrentAmmo()
    {
        return currentAmmo;
    }

    public void SetCurrentAmmo(int ammo)
    {
        currentAmmo = ammo;
    }
}

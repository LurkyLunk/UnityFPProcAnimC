using UnityEngine;

public class PickupManager : MonoBehaviour
{
    public GunFireAnimation gunFireAnimation; // Reference to the GunFireAnimation script
    int maxAmmoAmount = 10; // Maximum ammo amount
    int maxHealthAmount = 100; // Maximum health amount
    Gun gun; // Reference to the Gun script

    public void SetGun(Gun gunScript)
    {
        gun = gunScript;
    }

    public void PickupAmmo(AmmoPickup ammoPickup, Gun gun)
    {
        // Add the ammo amount to the player's ammo count or weapon
        // Implement your own logic here

        if (ammoPickup != null)
        {
            int currentAmmo = gun.GetCurrentAmmo();
            int newAmmo = currentAmmo + ammoPickup.Pickup();

            // Limit the new ammo to the maximum
            if (newAmmo > maxAmmoAmount)
            {
                newAmmo = maxAmmoAmount;
            }

            // Set the updated ammo amount
            gun.SetCurrentAmmo(newAmmo);
        }

        gunFireAnimation.ResetAnimation();
    }

    public void PickupHealth(HealthPickup healthPickup)
    {
        // Add the health amount to the player's health or healing system
        // Implement your own logic here

        if (healthPickup != null)
        {
            int currentHealth = healthPickup.healthAmount;
            int newHealth = currentHealth + healthPickup.Pickup();

            // Limit the new health to the maximum
            if (newHealth > maxHealthAmount)
            {
                newHealth = maxHealthAmount;
            }

            // Set the updated health amount
            healthPickup.healthAmount = newHealth;
        }
    }
}

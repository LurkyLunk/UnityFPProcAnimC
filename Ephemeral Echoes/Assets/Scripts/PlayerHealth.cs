using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;          // Maximum health value
    public int currentHealth;            // Current health value

    private void Start()
    {
        currentHealth = maxHealth;       // Initialize the current health to the maximum health value
    }

        public void PickupHealth(HealthPickup healthPickup)
    {
        // Add the health amount to the player's health or healing system
        // Implement your own logic here

        if (healthPickup != null)
        {
            int healthAmount = healthPickup.Pickup();
            PlayerHealth playerHealth = GetComponent<PlayerHealth>();

            playerHealth.AddHealth(healthAmount);
        }
    }


    public void TakeDamage(int damageAmount)
    {
        // Reduce the player's health by the damage amount
        currentHealth -= damageAmount;

        // Check if the player's health has reached zero or below
        if (currentHealth <= 0)
        {
            Die();                      // Call the Die function when the player's health is depleted
        }
    }

    public void AddHealth(int healAmount)
    {
        // Increase the player's health by the heal amount
        currentHealth += healAmount;

        // Ensure that the player's health does not exceed the maximum health value
        currentHealth = Mathf.Min(currentHealth, maxHealth);
    }

    private void Die()
    {
        // Handle player death here
        // For example, you can play a death animation, show a game over screen, or restart the level
    }
}

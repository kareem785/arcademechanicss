using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerhealths : MonoBehaviour
{
    public float health = 100f;
    public float damageAmount = 25f;
    public GameObject player;
    [SerializeField] private string tagToCollideWith = "Projectile";
    public int hitCount = 0;
    public Text healthText; // Reference to the UI Text component to display health

    private void Start()
    {
        // Initialize the health text on start
        UpdateHealthText();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(tagToCollideWith))
        {
            health -= damageAmount;
            hitCount++;

            // Update the health display after taking damage
            UpdateHealthText();

            if (health <= 0)
            {
                health = 0;
                Destroy(gameObject);
            }

            if (hitCount >= 4)
            {
                Destroy(gameObject);
            }
        }
    }

    // Method to update the health display in the UI
    private void UpdateHealthText()
    {
        healthText.text = "Health: " + health.ToString(); // Update text with the current health value
    }
}
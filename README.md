![Animation](https://github.com/user-attachments/assets/1cd693b3-7d3f-4d19-bd9c-5ba6a9b16a84)

jump mechanic
https://github.com/kareem785/arcademechanicss/blob/main/Assets/scripts/Playermovement.cs
jump 

![535665](https://github.com/user-attachments/assets/85174e62-698a-4037-87fe-93af44ba8736)
animaties
lopen animatie




movement script en platforms


![535665](https://github.com/user-attachments/assets/3b819354-99d4-4b2f-b268-e0a872bfe7cb)

![Screenshot 2025-04-15 175806](https://github.com/user-attachments/assets/d596dc92-c50c-4690-971b-02edceba2dcb)

platforms en movement

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 10f;
    [SerializeField]
    private float jumpSpeed = 5f;
    [SerializeField]
    private float gravity = 9.8f;
    [SerializeField]
    private float jumpCooldown = 0.5f;

    private Vector3 velocity;
    private bool canJump = true;
    private bool isGrounded;

    private float groundCheckDistance = 2f; // Slightly increased for better detection
    private LayerMask groundLayer;

    private Animator _animator;
    private Rigidbody _rb;

    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _rb = GetComponent<Rigidbody>();

        // Correct layer mask assignment
        groundLayer = LayerMask.GetMask("Ground");
    }

    void Update()
    {
        // Adjusted ground check with lower origin
        Vector3 rayOrigin = transform.position + Vector3.down * 0.5f;
        RaycastHit hit;
        isGrounded = Physics.Raycast(rayOrigin, Vector3.down, out hit, groundCheckDistance, groundLayer);

        // Debugging visualization
        Debug.DrawRay(rayOrigin, Vector3.down * groundCheckDistance, isGrounded ? Color.green : Color.red);

        if (isGrounded)
        {
            Debug.Log("Grounded!");

            canJump = true;    
        }
        else
        {
            Debug.Log("Not Grounded!");
        }

        // Horizontal movement
        Vector3 movement = Input.GetAxis("Horizontal") * transform.right +
                           Input.GetAxis("Vertical") * transform.forward;

        if (movement.magnitude > 1)
        {
            movement.Normalize();
        }
        transform.position += movement * movementSpeed * Time.deltaTime;

        // Handle jumping
        if (Input.GetKeyDown(KeyCode.Space) && canJump && isGrounded)
        {
            Jump();
        }

        // Update animator speed
        UpdateAnimator(movement);
    }

    private void Jump()
    {
        Debug.Log("Jump");
        _rb.velocity = new Vector3(_rb.velocity.x, jumpSpeed, _rb.velocity.z);
        _animator.SetTrigger("Jump");
        canJump = false;
      //  StartCoroutine(JumpCooldownCoroutine());
    }

    private IEnumerator JumpCooldownCoroutine()
    {
        yield return new WaitForSeconds(jumpCooldown);
        canJump = true;
    }

    private void FixedUpdate()
    {
        if (!isGrounded)
        {
            _rb.velocity += Vector3.down * gravity * Time.fixedDeltaTime;
        }
    }

    private void UpdateAnimator(Vector3 movement)
    {
        _animator.SetFloat("Speed", movement.magnitude > 0 ? 1 : 0);
    }
}

![535665](https://github.com/user-attachments/assets/652ccce2-dd3b-422c-b639-541deee60c65)


using TMPro;
using UnityEngine;
using UnityEngine.UI;  // Add this for accessing UI elements

public class PickupScript : MonoBehaviour
{
    private bool isPlayerNearby = false; // Detecteer of de speler dichtbij is
    public static int score = 0; // Static variable for score
    public TextMeshProUGUI scoreText; // Reference to the Text component to display the score

    public GameObject audioSource; // Reference to the AudioSource component

    // Start is called before the first frame update
    void Start()
    {
        // Get the AudioSource component attached to the item
        //audioSource = GetComponent<AudioSource>();

        // Update the UI with the initial score
        UpdateScoreUI();
    }

    // Update is called once per frame
    void Update()
    {
        // Als de speler dichtbij is en de speler op de E-toets drukt
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E)) // Druk op 'E' om op te pakken
        {
            PickupItem();
        }
    }

    // Detecteer wanneer de speler in de buurt komt (via trigger)
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Zorg ervoor dat je speler de juiste tag heeft
        {
            isPlayerNearby = true; // Speler is dichtbij
            Debug.Log("Druk op E om op te pakken!"); // Debug bericht
        }
    }

    // Detecteer wanneer de speler de trigger verlaat
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false; // Speler is niet meer dichtbij
            Debug.Log("Speler verliet de trigger"); // Debug bericht
        }
    }

    // Functie die wordt aangeroepen wanneer de speler het object oppakt
    private void PickupItem()
    {
        // Verhoog de score wanneer het object wordt opgepakt
        score++;
        Debug.Log("Object opgepakt! Huidige score: " + score); // Toon score in de debug log

        // Play the pickup sound if the AudioSource exists
        if (audioSource != null)
        {
            //audioSource.Play(); // Play the sound when the item is picked up
            
        }

        // Update the score text in the UI
        UpdateScoreUI();

        // Verwijder het object uit de scene nadat het is opgepakt
        Destroy(gameObject);
    }

    // Update the score UI on the screen
    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score; // Update the text component to show the current score
        }
    }
}

pickup script
![535665](https://github.com/user-attachments/assets/d50d2d4c-69f0-4618-8c5e-b16583b8ec14)


enemy kill en ik kill
![5356657](https://github.com/user-attachments/assets/85042e35-2ff9-4b3a-b80f-6f2eea719f53)


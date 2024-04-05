using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    public float speed = 0; 
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public float jumpHeight = 250.0f;
    private int jumpCount = 2;


    // Start is called before the first frame update
    void Start()
    {
        // Make sure the win text is disabled to begin with
        winTextObject.SetActive(false);
        
        // The rigidbody component attatched to the player
        rb = GetComponent <Rigidbody>();
        
        // Score count
        count = 0; 

        // Score text call
        SetCountText();
    }

    void OnMove(InputValue movementValue)
    {
        // Allow the player to move in the x and y directions using arrow keys
        Vector2 movementVector = movementValue.Get<Vector2>();
       
        movementX = movementVector.x; 
        movementY = movementVector.y; 
    }

    void SetCountText() 
        {
            // Set the counter text in the corner of the window to display the score
            countText.text =  "Count: " + count.ToString();
            
            // If the goal amount is achieved then display the win text
            if (count >= 7)
            {
                winTextObject.SetActive(true);
            }
        }
    private void FixedUpdate()
    {
        // Our movement vector
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        
        // Force applied to rigidbody
        rb.AddForce(movement * speed); 
    }

    private void Update(){
        // Allow for jump and double jump on space button if less than three jumps at a time
        if (Input.GetKeyDown("space") && jumpCount > 0) {

            // Establish a new vector for the jump force
            Vector3 jump = new Vector3(0.0f, jumpHeight, 0.0f);

            // Apply jump force vector
            rb.AddForce(jump);

            // Limit it to only two jumps in between ground touches
            jumpCount -= 1;
        }
    }


    /* The function dealing with PickUp collision on trigger enter*/
    void OnTriggerEnter(Collider other) 
   {
        if (other.gameObject.CompareTag("PickUp")) 
        {
            // Removes gameObject essentially making it look as though it was collected
            other.gameObject.SetActive(false);
            // Up the counter for the collection count text
            count = count + 1;
            SetCountText();
        }
   }

   private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with the ground objects
        if (collision.gameObject.CompareTag("Ground"))
        {
            // If so then reset jumpCount
            jumpCount = 2;
        }
    }
}


using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

    /***** ABILITIES *****/
    public static bool isJumpUnlocked = true;
    public static bool isDoubleJumpUnlocked = true;
    public static bool isDashUnlocked = true;
    public static bool isShrinkUnlocked = true;


    /***** ENVIRONMENT VARIABLES *****/

    public float speed;
    public float jumpSpeed;
    public float gravity;
    public Text text;
    public bool grounded = true;
    public bool canDoubleJump = isDoubleJumpUnlocked;

    /***** ORIENTATION and BODY *****/
    private Rigidbody rb;
    private Vector3 originPosition = Vector3.zero;
    private Vector3 moveDirection = Vector3.zero;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        
        if (Application.platform == RuntimePlatform.Android)
        {
            moveDirection.x = 3 * Input.acceleration.x;
            moveDirection.z = 3 * Mathf.Clamp(-Input.acceleration.z - 0.6f, -1.0f, 1.0f);
        }
        else
        {
            moveDirection.x = Input.GetAxis("Horizontal");
            moveDirection.z = Input.GetAxis("Vertical");
        }

        jump();
        dash();
        death();

        //text.text = "(Horizontal :"+moveDirection.x+", Vertical : "+moveDirection.z+")";
        text.text = "Position " + rb.position.y;
        rb.AddForce(moveDirection * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
        }
    }

    void jump()
    {
        if(Input.GetMouseButtonDown(0)) // Touch on the screen or Right Click
        {
            // If we are on the floor, we jump
            if(grounded)
            {
                rb.AddForce(new Vector3(0.0f, jumpSpeed, 0.0f));
                canDoubleJump = true;
                grounded = false;
            }
            // Else, we can doubleJump
            else
            {
                if (canDoubleJump)
                {
                    canDoubleJump = false;
                    rb.AddForce(new Vector3(0.0f, jumpSpeed, 0.0f));
                }
            }
        }

        // On the ground
        if(rb.position.y <= 0.7)
        {
            grounded = true;
        }

    }


    void dash()
    {
        if(Input.GetMouseButtonDown(1))
        {
            rb.AddForce(new Vector3(0.0f,0.0f, 1000.0f));
        }
    }

    void shrink()
    {

    }


    void death()
    {
        if(rb.position.y < -5)
        {
            text.text = "You're dead to me !";
            rb.MovePosition(originPosition);
        }
    }
}

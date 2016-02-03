using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public float speed;
    public float jumpSpeed;
    public float gravity;
    public Text text;

    private Rigidbody rb;

    private Vector3 moveDirection = Vector3.zero;

    // The initials orientation
    private int initialOrientationX;
    private int initialOrientationY;
    private int initialOrientationZ;


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

        // Jump
        if (Input.GetButton("Jump") && rb.transform.position.y <= 0.5)
        {
            rb.AddForce(new Vector3(0.0f, jumpSpeed,0.0f));
        }
            
        text.text = "(Horizontal :"+moveDirection.x+", Vertical : "+moveDirection.z+")";
        rb.AddForce(moveDirection * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
        }
    }
}

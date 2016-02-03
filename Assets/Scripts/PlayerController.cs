using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public float speed;
    public Text text;

    private Rigidbody rb;


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
        float moveVertical;
        float moveHorizontal;
        if (Application.platform == RuntimePlatform.Android)
        {
            moveHorizontal = 3*Input.acceleration.x;
            moveVertical = 3*Mathf.Clamp(-Input.acceleration.z - 0.6f,-1.0f,1.0f);
        }
        else
        {
            moveHorizontal = Input.GetAxis("Horizontal");
            moveVertical = Input.GetAxis("Vertical");
        }

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        text.text = "(Horizontal :"+moveHorizontal+", Vertical : "+moveVertical+")";
        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
        }
    }
}

using UnityEngine;
public class PlayerHandler : MonoBehaviour
{

    Rigidbody rb;

    [SerializeField] private float playerSpeed;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float turnSpeed = 120f;

    private int health = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        //Old movement:
        //Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //rb.MovePosition(rb.position + movement * playerSpeed * Time.fixedDeltaTime);
        //End Old Movement

        //TANK CONTROLS W/S : Forward/Backward (Relative to direction the player is facing)
        //              A/D : Turn Left/Right 
        float moveInput = Input.GetAxis("Vertical");
        float turnInput = Input.GetAxis("Horizontal");

        //Forward/Back
        Vector3 movement = transform.forward * moveInput * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);

        //Rotate
        Quaternion turnRotation = Quaternion.Euler(
            0f,
            turnInput * turnSpeed * Time.fixedDeltaTime,
            0f
            );
        rb.MoveRotation(rb.rotation * turnRotation);

    }

    //Connected to Patches.cs
    public void IncreaseHealth()
    {
        health++;
    }


}

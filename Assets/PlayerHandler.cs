using UnityEngine;
public class PlayerHandler : MonoBehaviour
{

    Rigidbody rb;

    [SerializeField] private float playerSpeed;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float backSpeedMult = 0.7f; //Player is slower when going backward
    [SerializeField] private float turnSpeed = 120f;
    [SerializeField] private float acceleration = 10f;
    private float currentSpeed = 0f;

    // For attack stance
    private bool isPrimed = false;

    //Weapon
    [SerializeField] private Transform wpnNeedle;
    [SerializeField] private float weaponRotateSpeed = 10f;

    private Quaternion weaponDefaultRotation;
    private Quaternion weaponPrimedRotation;

    private int health = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        //Weapon Rotation when primed
        weaponDefaultRotation = wpnNeedle.localRotation;
        weaponPrimedRotation = weaponDefaultRotation * Quaternion.Euler(90f, 0f, 00f); //Change axis here
    
}

    private void FixedUpdate()
    {
        /**Old movement:
        //Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //rb.MovePosition(rb.position + movement * playerSpeed * Time.fixedDeltaTime);
        **/

        //TANK CONTROLS W/S : Forward/Backward (Relative to direction the player is facing)
        //              A/D : Turn Left/Right 
        float moveInput = Input.GetAxisRaw("Vertical");
        float turnInput = Input.GetAxis("Horizontal");
       
        // Target speed
        float targetSpeed = 0f;

        // Move if not occupied (primed)
        if (!isPrimed)
        {
            targetSpeed = moveInput * moveSpeed;

            //reduce speed when going backward
            if (moveInput < 0)
            {
                targetSpeed = moveInput * moveSpeed * backSpeedMult;
            }
            else
            {
                targetSpeed = moveInput * moveSpeed;
            }

        }

        // Smoothly accelerate/decelerate toward target
        currentSpeed = Mathf.MoveTowards(
            currentSpeed,
            targetSpeed,
            acceleration * Time.fixedDeltaTime
        );

        Vector3 movement =
            transform.forward * currentSpeed * Time.fixedDeltaTime;

        rb.MovePosition(rb.position + movement);

        //Rotate
        Quaternion turnRotation = Quaternion.Euler(
            0f,
            turnInput * turnSpeed * Time.fixedDeltaTime,
            0f
            );
        rb.MoveRotation(rb.rotation * turnRotation);

    }

    private void Update()
    {

        //Attack stance CONTROLS
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            isPrimed = true;
        } else
        {
            isPrimed = false;
        }

        //WHEN PRIMED, rotate weapon
        if (isPrimed)
        {
            wpnNeedle.localRotation = Quaternion.Slerp(
                wpnNeedle.localRotation,
                weaponPrimedRotation,
                weaponRotateSpeed * Time.deltaTime
            );
        }
        else
        {
            wpnNeedle.localRotation = Quaternion.Slerp(
                wpnNeedle.localRotation,
                weaponDefaultRotation,
                weaponRotateSpeed * Time.deltaTime
            );
        }

    }

    //Connected to Patches.cs
    public void IncreaseHealth()
    {
        health++;
    }


}

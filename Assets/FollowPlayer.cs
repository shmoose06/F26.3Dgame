using UnityEngine;
//Script for 3rd person camera following the player
//ALSO holds camera controls
public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform cameraTarget;

    //Position relative to player at 0 degrees
    [SerializeField] private Vector3 offset = new Vector3(0f, 5f, -8f);

    [SerializeField] private float smoothTime = 0.25f;
    private float ROT_INCREMENT = 90f;

    private Vector3 velocity = Vector3.zero;
    private float currentRotation = 0f;


    private void Update()
    {
        //ROTATE INPUT
        if(Input.GetKeyDown(KeyCode.LeftBracket))
        {
            currentRotation -= ROT_INCREMENT;
        }
        if(Input.GetKeyDown(KeyCode.RightBracket))
        {
            currentRotation += ROT_INCREMENT;
        }
    }

    private void LateUpdate()
    {
        // Rotate offset around player
        Quaternion rotation = Quaternion.Euler(0f, currentRotation, 0f);
        Vector3 rotatedOffset = rotation * offset;

        Vector3 targetPosition = cameraTarget.position + rotatedOffset;

        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref velocity,
            smoothTime
        );

        transform.LookAt(cameraTarget);

    }

}

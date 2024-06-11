using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwlFly : MonoBehaviour
{
    public float moveSpeed;
    public float maxFloatHeight = 10;
    public float minFloatHeight;

    public Camera freeLookCamera;
    private float currentHeight;
    private Animator anim;
    private float xRotation;

    public LayerMask terrainLayer; // Add this to specify the terrain layer

    void Start()
    {
        currentHeight = transform.position.y;
        anim = GetComponent<Animator>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        xRotation = freeLookCamera.transform.rotation.eulerAngles.x;

        if (Input.GetKey(KeyCode.W))
        {
            MoveCharacter();
        }
        else
        {
            DisableMovement();
        }

        // Clamp current height to the maximum float height
        currentHeight = Mathf.Clamp(transform.position.y, currentHeight, maxFloatHeight);

        // Raycast downward to check the distance to the terrain
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, Mathf.Infinity, terrainLayer))
        {
            float terrainHeight = hit.point.y;
            float targetHeight = terrainHeight + minFloatHeight;

            // Update currentHeight to clamp between terrain + minFloatHeight and maxFloatHeight
            currentHeight = Mathf.Clamp(currentHeight, targetHeight, maxFloatHeight);

            // Update the position with the clamped currentHeight
            transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

            // Set animation based on height comparison
            anim.SetBool("isFlying", currentHeight > targetHeight);
        }
    }

    private void MoveCharacter()
    {
        Vector3 cameraForward = new Vector3(freeLookCamera.transform.forward.x, 0, freeLookCamera.transform.forward.z);
        transform.rotation = Quaternion.LookRotation(cameraForward);
        transform.Rotate(new Vector3(xRotation, 0, 0), Space.Self);

        anim.SetBool("isFlying", true);

        Vector3 forward = freeLookCamera.transform.forward;
        Vector3 flyDirection = forward.normalized;

        currentHeight += flyDirection.y * moveSpeed * Time.deltaTime;
        currentHeight = Mathf.Clamp(currentHeight, minFloatHeight, maxFloatHeight);

        transform.position += flyDirection * moveSpeed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);
    }

    private void DisableMovement()
    {
        anim.SetBool("isFlying", false);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }
}

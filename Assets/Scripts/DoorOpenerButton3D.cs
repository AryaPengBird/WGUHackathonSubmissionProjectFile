using UnityEngine;

public class DoorOpenerButton3D : MonoBehaviour
{
    [SerializeField] private GameObject door; // Reference to the door GameObject
    private bool isPlayerInRange = false; // Track if the player is in range

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player enters the trigger
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the player exits the trigger
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    private void Update()
    {
        // Check if the player is in range and presses the "F" key
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F))
        {
            OnOpenDoorButtonClick();
        }
    }

    private void OnOpenDoorButtonClick()
    {
        // Check if the player has collected at least 5 coins
        if (GameManager.Instance.CoinsCollected >= 5)
        {
            // Deactivate the door
            door.SetActive(false);
        }
    }
}

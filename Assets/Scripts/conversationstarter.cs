using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class conversationstarter : MonoBehaviour
{
    [SerializeField] private NPCConversation AliensConversation;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                StartConversation();
            }
        }
    }

    private void StartConversation()
    {
        // Unlock the cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        ConversationManager.Instance.StartConversation(AliensConversation);

        ConversationManager.OnConversationEnded += EndConversation;
    }

    private void EndConversation()
    {
        // Lock the cursor back
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Unsubscribe 
        ConversationManager.OnConversationEnded -= EndConversation;
    }
}

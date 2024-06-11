using UnityEngine;
using DialogueEditor;

public class CoinCollection : MonoBehaviour
{
    [SerializeField] private NPCConversation coinConversation;
    private bool correctAnswer = false;


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
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        ConversationManager.Instance.StartConversation(coinConversation);
        ConversationManager.OnConversationEnded += EndConversation;
    }

    private void EndConversation()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (correctAnswer)
        {
            GameManager.Instance.AddCoin();
            gameObject.SetActive(false); // Deactivate the orb
        }

        correctAnswer = false; // Reset 

        ConversationManager.OnConversationEnded -= EndConversation;
    }

    // dialogue correct event
    public void SetCorrectAnswer()
    {
        correctAnswer = true;
    }
}

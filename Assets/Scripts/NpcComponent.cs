using UnityEngine;

public class NpcComponent : MonoBehaviour
{
    [Header("Conversation Properties")]
    public ConversationComponent conversationComponent;
    private bool _canTalk = false;
    [Space]
    [Header("Mission Properties")]
    [Space]
    public bool startsMission = true;
    public MissionComponent missionComponent;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _canTalk)
        {
            if (!conversationComponent.StartConversation(FindObjectOfType<TextBox>()))
                missionComponent.TetherAndSetNewMission(FindObjectOfType<MissionBox>(), conversationComponent.npcName);
        }
        
        conversationComponent.Update();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _canTalk = other.CompareTag("Player");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _canTalk = false;
            conversationComponent.ResetDialogue();
            if (conversationComponent.textBoxObject != null)
                conversationComponent.textBoxObject.SetDialogueWindowVisibility(false);
        }
    }
}

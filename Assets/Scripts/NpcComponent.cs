using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.Serialization;

public class NpcComponent : MonoBehaviour
{
    public ConversationComponent conversationComponent;
    [Space]
    public bool startsMission = true;
    public MissionComponent missionComponent;

    private void Update()
    {
        conversationComponent.Update();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            conversationComponent.TetherToTextbox(FindObjectOfType<TextBox>());
        }
    }
}

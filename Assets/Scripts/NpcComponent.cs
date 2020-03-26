using System;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class NpcComponent : MonoBehaviour
{
    [Header("NPC Properties")] 
    public bool hasSeveralIdleAnims = false;
    private int _magicNumber = 3;
    public int maxMagicNumbers = 10;
    public float timeBetweenAnimCheck = 1;
    private float _timer;
    [Header("Conversation Properties")]
    public ConversationComponent conversationComponent;
    private bool _canTalk = false;
    [Space]
    [Header("Mission Properties")]
    [Space]
    public bool startsMission = true;
    public MissionComponent missionComponent;
    public GameObject alertSprite;
    public Sprite missionSprite;
    public Sprite missionInteractSprite;
    private Animator _animator;

    private const bool debug = true;
    private const float CompletedAlpha = 0.9f;

    private void Awake()
    {
        conversationComponent.vocalSource = GetComponent<AudioSource>();
        _timer = timeBetweenAnimCheck;
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {

#if true
        if (Input.GetKeyDown(KeyCode.L))
            OptionManager.SetIntPreference(missionComponent.targetSceneName, 1);
        if (Input.GetKeyDown(KeyCode.K))
            OptionManager.ClearSavedInfo();
#endif

        if (Input.GetKeyDown(KeyCode.E) && _canTalk)
        {
            if (OptionManager.GetIntIfExists(missionComponent.targetSceneName) != 1 &&
                !missionComponent.MissionBoxActive() &&
                !conversationComponent.StartConversation(FindObjectOfType<TextBox>()) && startsMission)
                missionComponent.TetherAndSetNewMission(FindObjectOfType<MissionBox>(), conversationComponent.npcName);
        }
        
        conversationComponent.Update();
        missionComponent.Update();
        if (hasSeveralIdleAnims)
            CheckToAlternateAnimation();
    }

    private void CheckToAlternateAnimation()
    {
        if (_timer <= 0)
        {
            if (Random.Range(0, maxMagicNumbers).Equals(_magicNumber))
            {
                //_animator.SetTrigger("Alternate");
                Debug.Log("Triggered Alternate Idle");
            }
            _timer = timeBetweenAnimCheck;
        }
        else
        {
            _timer -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _canTalk = other.CompareTag("Player");
        if (_canTalk)
            alertSprite.GetComponent<SpriteRenderer>().sprite = missionInteractSprite;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _canTalk = false;
            conversationComponent.ResetDialogue();
            if (conversationComponent.textBoxObject != null)
                conversationComponent.textBoxObject.SetDialogueWindowVisibility(false);
            alertSprite.GetComponent<SpriteRenderer>().sprite = missionSprite;
        }
    }
}

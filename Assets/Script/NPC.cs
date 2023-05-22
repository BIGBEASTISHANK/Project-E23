using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class NPC : MonoBehaviour
{
    // Variables
    private Quest questScript;

    [Header("NPC Data")]
    public int NPCId;
    [SerializeField] private string questTxt;
    [Space]
    [SerializeField] private TMP_Text questInfoPrompt;

    [Header("Player Check")]
    [SerializeField] private float playerCheckRadius;
    [Space]
    [SerializeField] private GameObject player;
    [SerializeField] private LayerMask playerLayer;

    // Refrences
    private void Start()
    {
        questScript = gameObject.GetComponent<Quest>(); // Getting questScript
    }
    private void FixedUpdate()
    {
        // Methods
        PlayerInRange();
    }


    // PlayerInRange
    private void PlayerInRange()
    {
        // When Player is in range
        if (Physics.CheckSphere(transform.position, playerCheckRadius, playerLayer))
        {
            // Message shower
            if (!thisNpcQuestCompleted())
            {
                if (!GameManager.instance.inAQuest) // If not in a quest
                    ShowQuestMsg();
                else if (GameManager.instance.inAQuest && GameManager.instance.inQuestId == NPCId) // this quest is accepted
                    QuestAcceptedMSG();
            }
            else
                QuestCompletedMsg(); // this quest is completed

            // Want to intract
            if (GameManager.instance.inputManager.Player.NPCIntraction.IsPressed())
            {
                // Starting Quest
                if (!GameManager.instance.inAQuest && !thisNpcQuestCompleted())
                    QuestStarting();

                // Start vibration on gamepad
                Gamepad.current.SetMotorSpeeds(1, 1);
            }
            else
                Gamepad.current.SetMotorSpeeds(0, 0); // Stopping vibration on gamepad
        }
        else
            questInfoPrompt.text = "";
    }


    // Show Quest
    private void ShowQuestMsg()
    {
        questInfoPrompt.text = questTxt;
    }


    // QuestAcceptedMSG
    private void QuestAcceptedMSG()
    {
        questInfoPrompt.text = "You have Accepted the quest, The Quest is: " + questTxt;
    }


    // QuestCompletedMsg
    private void QuestCompletedMsg()
    {
        questInfoPrompt.text = "You Have Completed This Quest";
    }


    // QuestStarting
    private void QuestStarting() => questScript.questStart = true;


    // NpcQuestCompletedCheck
    private bool thisNpcQuestCompleted()
    {
        // var
        bool value = false;

        // Checking if the quest is completed
        foreach (int i in GameManager.instance.npcQuestCompletedID)
        {
            if (i == NPCId)
            {
                value = true;
                break;
            }
            else
                value = false;
        }

        // Returning value
        return value;
    }
}

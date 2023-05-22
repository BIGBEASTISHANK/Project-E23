using UnityEngine;

public class Quest : MonoBehaviour
{
    // Variables
    private NPC npcScript;

    [HideInInspector] public bool questStart;

    // Refrences
    private void Start()
    {
        npcScript = gameObject.GetComponent<NPC>(); // Setting npc script
    }

    private void FixedUpdate()
    {
        // Methods for crossponding npc id
        if (questStart)
        {
            // Selecting quest according to NPCId
            switch (npcScript.NPCId)
            {
                case 1:
                    QuestStartingDefaults();
                    Quest1();
                    break;
                case 2:
                    QuestStartingDefaults();
                    Quest2();
                    break;
                case 3:
                    QuestStartingDefaults();
                    Quest3();
                    break;
                default:
                    QuestNotAvailable();
                    break;
            }
        }
    }


    // QuestCompleted
    private void QuestCompleted()
    {
        // Adding completed npc id
        GameManager.instance.npcQuestCompletedID.Add(npcScript.NPCId);

        // Stoping the quest
        questStart = false;
        GameManager.instance.inAQuest = false;
        GameManager.instance.inQuestId = 0;
    }


    // QuestStartingDefaults
    private void QuestStartingDefaults()
    {
        GameManager.instance.inAQuest = true;
        GameManager.instance.inQuestId = npcScript.NPCId;
    }


    // QuestNotAvailable
    private void QuestNotAvailable()
    {
        questStart = false; // setting quest to started to false

        // Logging
        GameManager.instance.DLogger("Quest Not Found");
    }


    // Quest 1
    private void Quest1()
    {
        if (Input.GetKey(KeyCode.H))
            QuestCompleted();

    }


    // Quest 2
    private void Quest2()
    {
        if (Input.GetKey(KeyCode.J))
            QuestCompleted();

    }


    // Quest 3
    private void Quest3()
    {
        if (Input.GetKey(KeyCode.K))
            QuestCompleted();

    }
}

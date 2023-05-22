using TigerForge;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singelton
    public static GameManager instance;

    // Variables
    [HideInInspector] public bool inAQuest;
    [HideInInspector] public int inQuestId;

    public EasyFileSave EFSTesting;
    public EasyFileSave npcQuestCompleteID;
    public InputManager inputManager;

    // Refrences
    private void Awake()
    {
        // Singelton
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;

        // Input Manager
        inputManager = new InputManager();

        // Easy Fie Save
        EFSTesting = new EasyFileSave("EFSTesting");
        npcQuestCompleteID = new EasyFileSave("npcQuestCompleteID");
    }

    private void Start() => TestingORDebuggingStart();

    private void FixedUpdate()
    {
        // Methods
        TestingORDebuggingUpdate();
    }


    // Public logger
    public void DLogger(string value) => Debug.Log(value);


    // Cursor Visiblity
    public void CursorVisiblity(bool value)
    {
        if (value == false) // Disable Cursor
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else // Enable cursor
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }


    // Testing Or Debugging
    private void TestingORDebuggingStart()
    {
        // EFSTesting.Delete();
        if (npcQuestCompleteID.Load())
        {
            foreach (int i in npcQuestCompleteID.GetList<int>("npcQuestCompleteIDList"))
            {
                DLogger(i.ToString());
            }
            npcQuestCompleteID.Dispose();
        }
    }

    // Testing Or Debugging update
    private void TestingORDebuggingUpdate()
    {
        // DLogger(Gamepad.current.IsActuated().ToString());
    }


    // OnEnable & OnDisable
    private void OnEnable() => inputManager.Enable();
    private void OnDisable() => inputManager.Disable();
}

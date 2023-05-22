using TigerForge;
using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    // Singelton
    public static GameManager instance;

    // Variables
    [HideInInspector] public bool inAQuest;
    [HideInInspector] public int inQuestId;
    [HideInInspector] public List<int> npcQuestCompletedID;

    public EasyFileSave EFSTesting;
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

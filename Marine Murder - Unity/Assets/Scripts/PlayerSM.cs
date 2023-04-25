using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem.UI;

public class PlayerSM : StateMachine
{
    [Header("Can change")]
    public float RaycastDistance;
    public float ZoomInPercentage = 0.5f;

    [SerializeField] private float textPanelDisappearTimer = 2f;
    [Space(10)]
    [Header("Don't change")]
    public GameObject PlayerFollowCamera;
    public GameObject HighlightPanel;
    public GameObject InteractExamineTextPanel;
    public GameObject ZoomVirtualCamera;
    public GameObject inventoryPanel;
    public GameObject puzzleButtonsParent;
    public GameObject crosshair;

    public MicroscopePuzzleScript puzzleScript;

    public Camera MainCamera;
    public Button InteractButton;
    public Button ExamineButton;
    public TMP_Text InteractExamineText;

    public PointerEventData PointerEventData;
    public EventSystem EventSystem;
    public GraphicRaycaster Raycaster;
    public FirstPersonInputModule firstPersonInputModule;
    public InputSystemUIInputModule inputSystemUIInputModule;


    [HideInInspector] public DefaultState defaultState;
    [HideInInspector] public DialogueState dialogueState;
    [HideInInspector] public ZoomState zoomState;
    [HideInInspector] public CodelockZoomState codelockZoomState;
    [HideInInspector] public MicroscopePuzzleState microscopePuzzleState;
    [HideInInspector] public CameraZoomState cameraZoomState;

    [HideInInspector] public GameObject player;

    private void Awake()
    {
        defaultState = new DefaultState(this);
        dialogueState = new DialogueState(this);
        zoomState = new ZoomState(this);
        codelockZoomState = new CodelockZoomState(this);
        microscopePuzzleState = new MicroscopePuzzleState(this);
        cameraZoomState = new CameraZoomState(this);

        player = gameObject;
    }

    public void ChangeState(BaseState newState, GameObject targetCamera)
    {
        currentState.Exit();

        currentState = newState;
        currentState.Enter(targetCamera);
    }

    public void ChangeState(BaseState newState, GameObject targetCamera, GameObject targetGO)
    {
        currentState.Exit();

        currentState = newState;
        currentState.Enter(targetCamera, targetGO);
    }

    public void ChangeState(BaseState newState, GameObject targetCamera, GameObject playerFollowCamera, GameObject targetGO, float zoomInPercentage)
    {
        currentState.Exit();

        currentState = newState;
        currentState.Enter(targetCamera, playerFollowCamera, targetGO, zoomInPercentage);
    }

    protected override BaseState GetInitialState()
    {
        return defaultState;
    }

    private IEnumerator HideInteractExamineTextPanel()
    {
        float timer = 0;
        while (timer < textPanelDisappearTimer)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        InteractExamineTextPanel.SetActive(false);
    }

    public void InteractResponse()
    {
        if (currentState.Target.TryGetComponent<IInteract>(out IInteract interaction))
        {
            InteractExamineText.text = interaction.GetInteractText();
            if (InteractExamineText.text != null)
                InteractExamineTextPanel.SetActive(true);
            StartCoroutine(HideInteractExamineTextPanel());
        }
    }
}
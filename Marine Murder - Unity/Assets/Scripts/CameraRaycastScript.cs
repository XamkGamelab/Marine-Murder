using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
using StarterAssets;
using UnityEngine.EventSystems;

public class CameraRaycastScript : MonoBehaviour
{
    [Header("Changeable variables")]
    [Space(5)]
    [SerializeField] private float raycastDistance = 10f;
    [SerializeField] private float textPanelDisappearTimer = 2f;
    [Tooltip("Value between 0 and 1")]
    [SerializeField] private float zoomInPercentage = 0.5f;

    [Header("Don't change these")]
    [Space(5)]
    [SerializeField] private PlayerSM playerSM;
    [SerializeField] private FirstPersonController firstPersonController;
    [SerializeField] private GameObject playerFollowCamera;
    [SerializeField] private GameObject zoomVirtualCamera;

    [SerializeField] private GameObject interactionExamineGO;
    [SerializeField] private GameObject highlightPanel;
    [SerializeField] private GameObject interactExamineTextPanel;

    [SerializeField] private Button interactButton;
    [SerializeField] private Button examineButton;
    [SerializeField] private TMP_Text interactExamineText;

    [SerializeField] GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    [SerializeField] EventSystem m_EventSystem;
    [SerializeField] RectTransform canvasRect;

    private bool Key;
    private new Camera camera;
    private GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        camera = gameObject.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastCheck();

        if (Input.GetKeyDown(KeyCode.Mouse0) && interactExamineTextPanel.activeSelf)
            ExitExamine();
    }

    void RaycastCheck()
    {
        RaycastHit hit;
        Ray forwardRay = camera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));

        // Bit shift the index of the layer (6) to get a bit mask, the "Interactables" layer
        // This will cast rays only against colliders in layer 6.
        //int layerMask = 1 << 6;
        int layerMask1 = 1 << 5;
        int layerMask2 = 1 << 6;
        int layerMask = layerMask1 | layerMask2;


        //Set up the new Pointer Event
        m_PointerEventData = new PointerEventData(m_EventSystem);
        //Set the Pointer Event Position to that of the game object
        m_PointerEventData.position = Input.mousePosition;

        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();

        //Raycast using the Graphics Raycaster and mouse click position
        m_Raycaster.Raycast(m_PointerEventData, results);


        // if hit something on interactables layer
        if (Physics.Raycast(forwardRay, out hit, raycastDistance, layerMask2))
        {
            // if hit something with IInteract component
            if (hit.transform.gameObject.TryGetComponent<IInteract>(out IInteract interaction))
            {
                // hit object has Outline script component
                if (hit.transform.gameObject.TryGetComponent<Outline>(out Outline outline))
                {
                    // no target -> set target to hit object & activate highlight panel
                    if (target == null || target != hit.transform.gameObject)
                    {
                        ResetTarget();

                        target = hit.transform.gameObject;
                        outline.enabled = true;

                        Key = true;

                        highlightPanel.SetActive(true);

                        interactButton.onClick.AddListener(interaction.Interact);
                        examineButton.onClick.AddListener(this.ExamineResponse);
                    }
                }
                else
                    ResetTarget();

                HighlighPanelPositionUpdate();
            }
        }
        // player is hovering over the highlight panel so update it's position
        else if (results.Count > 0)
        {
            if (target)
                HighlighPanelPositionUpdate();
            else
                ResetTarget();
        }
        // did not hit anything on interactables layer and key is true
        else if (Key == true)
            ResetTarget();
    }

    private void ResetTarget()
    {
        if (target != null)
            target.GetComponent<Outline>().enabled = false;

        target = null;
        highlightPanel.SetActive(false);
        Key = false;

        interactButton.onClick.RemoveAllListeners();
        examineButton.onClick.RemoveAllListeners();
    }

    private void HighlighPanelPositionUpdate()
    {
        // update the position on highlight panel if it's active
        if (highlightPanel.activeSelf)
        {
            Vector3 point = camera.WorldToScreenPoint(target.transform.position);
            highlightPanel.transform.position = point;

        }
    }

    private void ExamineResponse()
    {
        if (target.TryGetComponent<IInteract>(out IInteract interaction))
        {
            interactExamineTextPanel.SetActive(true);
            highlightPanel.SetActive(false);
            interactExamineText.text = interaction.GetExamineText();

            //zoomVirtualCamera.transform.position = Vector3.LerpUnclamped(playerFollowCamera.transform.position, target.transform.position, zoomInPercentage);
            //zoomVirtualCamera.transform.LookAt(target.transform.position);

            //playerFollowCamera.SetActive(false);
            //zoomVirtualCamera.SetActive(true);
            playerSM.ChangeState(playerSM.zoomState, zoomVirtualCamera,playerFollowCamera, target, zoomInPercentage);
        }
    }

    private void ExitExamine()
    {
        interactExamineTextPanel.SetActive(false);
        highlightPanel.SetActive(true);

        //playerFollowCamera.SetActive(true);
        //zoomVirtualCamera.SetActive(false);
        playerSM.ChangeState(playerSM.defaultState);
    }

    private IEnumerator HideInteractExamineTextPanel()
    {
        float timer = 0;
        while (timer < textPanelDisappearTimer)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        interactExamineTextPanel.SetActive(false);
    }

    public void InteractResponse()
    {
        Debug.Log("InteractResponse");
        interactExamineTextPanel.SetActive(true);
        if (target.TryGetComponent<IInteract>(out IInteract interaction))
        {
            interactExamineText.text = interaction.GetInteractText();
            StartCoroutine(HideInteractExamineTextPanel());
        }
    }
}

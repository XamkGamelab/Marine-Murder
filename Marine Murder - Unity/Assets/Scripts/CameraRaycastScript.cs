using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
using StarterAssets;

public class CameraRaycastScript : MonoBehaviour
{
    [SerializeField] private float raycastDistance = 10f;
    [SerializeField] private FirstPersonController firstPersonController;

    [SerializeField] private GameObject interactionLookAtGO;
    [SerializeField] private GameObject highlightPanel;
    [SerializeField] private GameObject textPanel;

    [SerializeField] private Button interactButton;
    [SerializeField] private Button examineButton;
    [SerializeField] private TMP_Text interactLookAtText;

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

        if (Input.GetKeyDown(KeyCode.Mouse0) && textPanel.activeSelf)
            textPanel.SetActive(false);
    }

    void RaycastCheck()
    {
        RaycastHit hit;
        Ray forwardRay = camera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        // Bit shift the index of the layer (6) to get a bit mask, the "Interactables" layer
        // This will cast rays only against colliders in layer 6.
        int layerMask = 1 << 6;

        // if hit something on interactables layer
        if (Physics.Raycast(forwardRay, out hit, raycastDistance, layerMask))
        {
            // if hit something with IInteract component
            if (hit.transform.gameObject.TryGetComponent<IInteract>(out IInteract interaction))
            {
                // hit object has Outline script component
                if (hit.transform.gameObject.TryGetComponent<Outline>(out Outline outline))
                {
                    // no target -> set target to hit object & activate highlight panel
                    if (target == null)
                    {
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
        // did not hit anything on interactables layer and key is true
        else if (Key == true)
            ResetTarget();
    }

    private void ResetTarget()
    {
        if (target != null)
        {
            target.GetComponent<Outline>().enabled = false;
            target = null;
            highlightPanel.SetActive(false);
        }
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
        textPanel.SetActive(true);
        if (target.TryGetComponent<IInteract>(out IInteract interaction))
            interactLookAtText.text = interaction.GetExamineText();
    }

    public void InteractResponse()
    {
        Debug.Log("InteractResponse");
        textPanel.SetActive(true);
        if (target.TryGetComponent<IInteract>(out IInteract interaction))
            interactLookAtText.text = interaction.GetInteractText();
    }
}

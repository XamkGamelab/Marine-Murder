using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class CameraRaycastScript : MonoBehaviour
{
    [SerializeField] private float raycastDistance = 10f;
    [SerializeField] private GameObject highlightPanel;
    [SerializeField] private TMP_Text interactText;
    //[SerializeField] private Button interactButton;
    //[SerializeField] private Button LookAtButton;

    private bool Key;
    private Camera camera;
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
    }

    void RaycastCheck()
    {
        RaycastHit hit;
        Ray forwardRay = camera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        // Bit shift the index of the layer (6) to get a bit mask, the "Interactables" layer
        // This will cast rays only against colliders in layer 6.
        int layermask1 = 1 << 6;
        int layermask2 = 1 << 7;
        int layerMask = layermask1 | layermask2;

        // hit something on interactables layer
        if (Physics.Raycast(forwardRay, out hit, raycastDistance, layerMask))
        {
            if (hit.transform.gameObject.TryGetComponent<IInteract>(out IInteract interaction))
            {
                // hit object has Outline script component
                if (hit.transform.gameObject.TryGetComponent<Outline>(out Outline outline))
                {
                    if (target == null)
                    {
                        target = hit.transform.gameObject;
                        outline.enabled = true;

                        Key = true;

                        interactText.text = interaction.GetText();
                        highlightPanel.SetActive(true);
                    }
                }
                else
                {
                    if (target != null)
                    {
                        target.GetComponent<Outline>().enabled = false;
                        target = null;

                        highlightPanel.SetActive(false);
                    }
                    // Hitting something else.
                    Key = false;
                }

                if (highlightPanel.activeSelf)
                {
                    Vector3 point = camera.WorldToScreenPoint(target.transform.position);
                    highlightPanel.transform.position = point;

                }

                if (Input.GetKeyDown(KeyCode.Mouse0))
                    interaction.Interact();
            }
        }
        // did not hit anything on interactables layer and key is true
        else if (Key == true)
        {
            // not anymore.
            if (target != null)
            {
                target.GetComponent<Outline>().enabled = false;
                target = null;
                highlightPanel.SetActive(false);
            }
            Key = false;
        }
    }
}

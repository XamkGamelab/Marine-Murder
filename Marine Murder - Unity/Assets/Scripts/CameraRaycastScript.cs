using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaycastScript : MonoBehaviour
{
    [SerializeField] private float raycastDistance = 10f;

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
        int layerMask = 1 << 6;

        // hit something on interactables layer
        if (Physics.Raycast(forwardRay, out hit, raycastDistance, layerMask))
        {
            // hit object has Outline script component
            if (hit.transform.gameObject.TryGetComponent<Outline>(out Outline outline))
            {
                if (target == null)
                {
                    target = hit.transform.gameObject;
                    outline.enabled = true;

                    Key = true;
                }
            }
            else
            {
                if (target != null)
                {
                    target.GetComponent<Outline>().enabled = false;
                    target = null;
                }
                // Hitting something else.
                Key = false;

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
            }
            Key = false;
        }
    }
}

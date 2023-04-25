using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxTableScript : MonoBehaviour, IInteract
{
    [Header("Can change")]
    [SerializeField] private float lidOpenAngle = 90f;
    [SerializeField] private float lidOpenTime = 0.5f;
    [Space(10)]
    [Header("Don't change")]
    [SerializeField] private PlayerSM playerSM;
    [SerializeField] private GameObject zoomCamera;
    [SerializeField] private GameObject lid;

    private bool lidIsOpen = false;

    public string GetExamineText()
    {
        return null;
    }

    public string GetInteractText()
    {
        return null;
    }

    public bool HasInteract()
    {
        return true;
    }

    public void Interact()
    {
        //BoxCollider collider = GetComponent<BoxCollider>();
        //gameManager.ToggleObjectFocus(lockCamera, collider, 0f, true);

        if (lidIsOpen)
            playerSM.ChangeState(playerSM.codelockZoomState, zoomCamera, this.gameObject);
        else
        {
            StartCoroutine(OpenLid());
        }
    }

    public void Examine()
    {
        playerSM.ChangeState(playerSM.zoomState, playerSM.ZoomVirtualCamera, playerSM.PlayerFollowCamera, this.gameObject, playerSM.ZoomInPercentage);
    }

    private IEnumerator OpenLid()
    {
        float timer = 0f;

        while (timer < lidOpenTime)
        {
            lid.transform.Rotate((1 / lidOpenTime) * lidOpenAngle * Time.deltaTime * Vector3.down, Space.Self);
            timer += Time.deltaTime;
            yield return null;
        }

        lidIsOpen = true;
    }

    public bool HasExamine()
    {
        return true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;
using StarterAssets;

public class CodeLockScript : MonoBehaviour , IInteract
{
    [Header("Can change")]
    [SerializeField] private string examineText;

    // code lock stuff
    [SerializeField] private string defaultText;
    [SerializeField] private int[] correctNumber;

    // button stuff
    [SerializeField] private float buttonMoveAmount = 0.1f;
    [SerializeField] private float buttonMoveTime = 0.5f;
    [Space(10)]
    [Header("Don't change")]
    [SerializeField] private GameEventSO lockUnlocked;
    [SerializeField] private GameObject lockCamera;
    [SerializeField] private GameManagerScript gameManager;
    [SerializeField] private PlayerSM playerSM;


    private int[] givenNumber;
    private int index = 0;
    private bool locked = true;
    private TextMeshPro tMPro;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        givenNumber = new int[correctNumber.Length];
        tMPro = GetComponentInChildren<TextMeshPro>();
        audioSource = GetComponent<AudioSource>();
    }

    public float ButtonMoveAmount
    {
        get { return buttonMoveAmount; }
    }

    public float ButtonMoveTime
    {
        get { return buttonMoveTime; }
    }

    public void EnterNumber(int number)
    {
        if (locked)
        {
            givenNumber[index] = number;

            StringBuilder sb = new StringBuilder(tMPro.text);
            sb[index] = (char)(number + 48);  // need to add 48, because Unicode character for 1 is 49
            tMPro.text = sb.ToString();

            // enough numbers given, check if they are correct
            if (index >= correctNumber.Length - 1)
            {
                if (CompareArray(correctNumber, givenNumber))
                {
                    audioSource.pitch = 0.5f;
                    audioSource.Play();
                    Debug.Log("Correct code");
                    index = 0;
                    locked = false;
                    lockUnlocked.Raise();
                    gameObject.layer = 0;
                    playerSM.ChangeState(playerSM.defaultState);
                }
                else
                {
                    StartCoroutine(PlayWrongCodeSound());
                    Debug.Log("Wrong code");
                    index = 0;
                    tMPro.text = defaultText;
                }
            }
            else
            {
                index++;
            }
        }
    }

    private bool CompareArray(int[] array1, int[] array2)
    {
        for(int i=0; i < array1.Length; i++)
        {
            if (array1[i] != array2[i])
                return false;
        }
        return true;
    }

    public void Interact()
    {
        //BoxCollider collider = GetComponent<BoxCollider>();
        //gameManager.ToggleObjectFocus(lockCamera, collider, 0f, true);

        playerSM.ChangeState(playerSM.codelockZoomState, lockCamera, this.gameObject);
    }

    public void Examine()
    {
        playerSM.InteractExamineText.text = examineText;
        playerSM.InteractExamineTextPanel.SetActive(true);
        playerSM.ChangeState(playerSM.zoomState, playerSM.ZoomVirtualCamera, playerSM.PlayerFollowCamera, this.gameObject, playerSM.ZoomInPercentage);
    }

    public bool HasExamine()
    {
        return true;
    }

    public string GetExamineText()
    {
        return examineText;
    }

    public string GetInteractText()
    {
        return null;
    }

    public bool HasInteract()
    {
        return true;
    }

    IEnumerator PlayWrongCodeSound()
    {
        audioSource.pitch = 1f;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        audioSource.Play();
    }
}

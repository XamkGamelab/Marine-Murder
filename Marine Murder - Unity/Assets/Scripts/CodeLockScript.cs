using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;
using StarterAssets;

public class CodeLockScript : MonoBehaviour , IInteract
{
    // code lock stuff
    [SerializeField] private int[] correctNumber;
    [SerializeField] private string defaultText;
    [SerializeField] private GameEvent lockUnlocked;

    // button stuff
    [SerializeField] private float buttonMoveAmount = 0.1f;
    [SerializeField] private float buttonMoveTime = 0.5f;

    [SerializeField] private GameObject lockCamera;
    [SerializeField] private GameManagerScript gameManager;


    private int[] givenNumber;
    private int index = 0;
    private bool locked = true;
    private TextMeshPro tMPro;

    // Start is called before the first frame update
    void Start()
    {
        givenNumber = new int[correctNumber.Length];
        tMPro = GetComponentInChildren<TextMeshPro>();
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
                    Debug.Log("Correct code");
                    index = 0;
                    locked = false;
                    lockUnlocked.Raise();
                }
                else
                {
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
        BoxCollider collider = GetComponent<BoxCollider>();
        gameManager.ToggleObjectFocus(lockCamera, collider, 0f);
    }
}

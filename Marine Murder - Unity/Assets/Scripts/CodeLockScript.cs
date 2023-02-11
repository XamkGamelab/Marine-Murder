using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeLockScript : MonoBehaviour
{
    [SerializeField]
    private int[] correctNumber;
    private int[] givenNumber;
    private int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        givenNumber = new int[correctNumber.Length];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnterNumber(int number)
    {
        givenNumber[index] = number;

        // enough numbers given, check if they are correct
        if(index >= correctNumber.Length-1)
        {
            if(CompareArray(correctNumber, givenNumber))
            {
                Debug.Log("Correct code");
                index = 0;
            }
            else
            {
                Debug.Log("Wrong code");
                index = 0;
            }
        }
        else
        {
            index++;
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
}

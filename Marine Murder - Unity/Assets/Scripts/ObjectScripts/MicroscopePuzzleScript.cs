using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicroscopePuzzleScript : MonoBehaviour
{
    [Header("Can change")]
    [SerializeField] private List<RotationDirection> correctRotationOrder;
    [SerializeField] private float objectiveMoveTime = 0.5f;
    [SerializeField] private float objectiveResetTime = 1f;
    [SerializeField] private float totalRotation = 180f;
    [Space(10)]
    [Header("Don't change")]
    [SerializeField] private PlayerSM playerSM;
    [SerializeField] private GameObject objective;
    [SerializeField] private GameEventSO puzzleSolvedEvent;
    [SerializeField] private float startingZ;
    [SerializeField] private float targetZ;

    private int index = 0;
    private float moveStep, rotationStep;
    private bool animating = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartPuzzle()
    {
        index = 0;
        objective.transform.localPosition = new Vector3(objective.transform.localPosition.x,
                                                        objective.transform.localPosition.y,
                                                        startingZ);

        moveStep = (targetZ - startingZ) / correctRotationOrder.Count;
        rotationStep = totalRotation / correctRotationOrder.Count;
    }

    public void Rotate(int integer)
    {
        RotationDirection direction = (RotationDirection)integer;

        if (!animating)
        {
            if (direction == correctRotationOrder[index])
            {
                Debug.Log("Correct turn");
                index++;
                // rotation animation
                StartCoroutine(RotateAnimation(true, direction));

                if (index == correctRotationOrder.Count)
                    PuzzleSolved();
            }
            else
            {
                index = 0;
                Debug.Log("Wrong turn");
                // reset animation
                StartCoroutine(RotateAnimation(false, direction));
            }
        }
    }

    private void PuzzleSolved()
    {
        Debug.Log("Puzzle solved");
        puzzleSolvedEvent.Raise();
        playerSM.ChangeState(playerSM.defaultState);
    }

    IEnumerator RotateAnimation(bool correct, RotationDirection direction)
    {
        animating = true;

        if (correct)
        {

            Vector3 startPos = objective.transform.localPosition;
            Vector3 targetPos = objective.transform.localPosition + Vector3.forward * moveStep;
            float timer = 0;

            while (timer < objectiveMoveTime)
            {
                objective.transform.localPosition = Vector3.Lerp(startPos, targetPos, timer / objectiveMoveTime);

                if (direction == RotationDirection.clockwise)
                    objective.transform.Rotate(Vector3.forward, rotationStep);
                else
                    objective.transform.Rotate(Vector3.back, rotationStep*Time.deltaTime);

                timer += Time.deltaTime;
                yield return null;
            }


        }
        else
        {
            Vector3 startPos = objective.transform.localPosition;
            Vector3 targetPos = new Vector3(objective.transform.localPosition.x,
                                            objective.transform.localPosition.y,
                                            startingZ);
            float timer = 0;

            while(timer < objectiveResetTime)
            {
                objective.transform.localPosition = Vector3.Lerp(startPos, targetPos, timer / objectiveResetTime);
                objective.transform.Rotate(Vector3.back, rotationStep * Time.deltaTime);

                timer += Time.deltaTime;
                yield return null;
            }
        }

        animating = false;
    }
}

public enum RotationDirection { clockwise, counterClockwise }

﻿/* Original code by Mina Pêcheux
 * https://medium.com/c-sharp-progarmming/make-a-basic-fsm-in-unity-c-f7d9db965134
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public BaseState currentState;

    void Start()
    {
        currentState = GetInitialState();
        if (currentState != null)
            currentState.Enter();
    }

    void Update()
    {
        if (currentState != null)
            currentState.UpdateLogic();
    }

    void LateUpdate()
    {
        if (currentState != null)
            currentState.UpdatePhysics();
    }

    public void ChangeState(BaseState newState)
    {
        currentState.Exit();

        currentState = newState;
        currentState.Enter();
    }

    protected virtual BaseState GetInitialState()
    {
        return null;
    }

    //private void OnGUI()
    //{
    //    string content = currentState != null ? currentState.name : "(no current state)";
    //    GUILayout.Label($"<color='black'><size=40>{content}</size></color>");
    //}
}
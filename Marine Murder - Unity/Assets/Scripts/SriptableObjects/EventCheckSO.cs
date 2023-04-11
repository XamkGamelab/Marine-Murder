using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EventCheckSO : ScriptableObject
{
    [field: SerializeField] public bool eventHasHappened { get; set; }
}

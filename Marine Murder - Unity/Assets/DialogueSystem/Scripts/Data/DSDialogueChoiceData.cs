using System;
using UnityEngine;

namespace DS.Data
{
    using ScriptableObjects;

    [Serializable]
    public class DSDialogueChoiceData
    {
        [field: SerializeField] public string Text { get; set; }
        [field: SerializeField] public DSDialogueSO NextDialogue { get; set; }

        //added stuff
        [field: SerializeField] public ItemSO NeedsItem { get; set; }
        [field: SerializeField] public EventCheckSO NeedsEventCheck { get; set; }
    }
}
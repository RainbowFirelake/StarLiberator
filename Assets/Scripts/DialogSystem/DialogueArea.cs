using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueArea : MonoBehaviour
{
    public static event Action<Dialogue> OnEnteringDialogue;

    [SerializeField] private Dialogue dialogue;
    [SerializeField]
    private bool isActivateOnStart;

    private void Start()
    {
        if (isActivateOnStart)
        {
            ActivateDialogue();
        }
    }

    public void ActivateDialogue()
    {
        OnEnteringDialogue?.Invoke(dialogue);
    }
}

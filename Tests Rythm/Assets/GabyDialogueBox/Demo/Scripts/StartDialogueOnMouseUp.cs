using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DialogueComponent),typeof(CircleCollider2D))]
public class StartDialogueOnMouseUp : MonoBehaviour
{
    private void OnMouseUpAsButton()
    {
        GetComponent<DialogueComponent>().StartDialogue();
    }
}

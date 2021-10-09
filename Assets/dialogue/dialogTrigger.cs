using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogTrigger : MonoBehaviour
{
    public dialog dialgue;

    public void startDialog()
    {
        //FindObjectOfType<dialogueManager>().startDialogue(dialgue);
        dialogueManager.instance.SetText(dialgue);
    }
    public void countine()
    {
        FindObjectOfType<dialogueManager>().NextDialog();
    }
}

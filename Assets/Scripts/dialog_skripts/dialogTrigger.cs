using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogTrigger : MonoBehaviour
{
    public Dialog dialog;

    public bool FirstConvers = true;

    public void TriggerDialog()
    {
        if(FirstConvers == true)
        {
            FindObjectOfType<DialogManager>().StartDialog(dialog);
            FirstConvers = false;
        }
        else
        {
            FindObjectOfType<DialogManager>().StartLastDialog(dialog);
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class back : MonoBehaviour
{
    public void pushed()
    {
        if(FindObjectOfType<cmaST>().focused)
        {
            FindObjectOfType<cmaST>().outfocus();
        }
        else
        {
            GetComponent<loadScene>().loadSCene();
        }
    }
}

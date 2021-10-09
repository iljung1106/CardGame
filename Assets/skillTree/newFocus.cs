using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class newFocus : MonoBehaviour
{
    GameObject cama;
    public GameObject cardPre;
    public string namea, infoi;
    private void Start()
    {
        cama = FindObjectOfType<cmaST>().gameObject;
    }
    public void focus(skillNode node)
    {
        cama.GetComponent<cmaST>().Focus(namea,infoi,cardPre,node);
    }
}

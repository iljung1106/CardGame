using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class focus : MonoBehaviour
{
    public Transform tar, outa, cardCallS;
    GameObject cama, copyed;
    public GameObject cardpre;
    private void Start()
    {
        cama = FindObjectOfType<cmaST>().gameObject;
    }
    public void Focus()
    {
        print("focus");
        outa = cama.GetComponent<cmaST>().target;
        cama.GetComponent<cmaST>().target = tar;
        copyed = Instantiate(cardpre, cardCallS.position, cardpre.transform.rotation);
    }
    public void outFo()
    {
        cama.GetComponent<cmaST>().target = outa;
        Destroy(copyed);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cmaST : MonoBehaviour
{
    skillNode bod;
    GameObject copyed;
    public Text tex, namef;
    public Transform prefabCAll;
    public GameObject infocard;
    public Transform target;
    public bool focused;
    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, target.position, 70.0f);
    }
    public void Focus(string name, string info, GameObject cardprefab,skillNode nod)
    {
        namef.text = name;
        tex.text = info;
        copyed = Instantiate(cardprefab, prefabCAll.position, cardprefab.transform.rotation);
        infocard.SetActive(true);
        focused = true;
        bod = nod;
    }
    public void outfocus()
    {
        Destroy(copyed);
        infocard.SetActive(false);
        focused = false;
        bod.focused = false;
    }
}

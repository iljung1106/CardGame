using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fadeinout : MonoBehaviour
{
    float colo;
    public bool fadein, fadeout;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (fadein)
        {
            if (colo <= 1)
            {
                colo += Time.deltaTime * 2;
                GetComponent<Image>().color = new Color(0, 0, 0, colo);
            }
            else
            {
                fadein = false;
            }
        }
        else if (fadeout)
        {
            if (colo >= 0)
            {
                colo -= Time.deltaTime * 2;
                GetComponent<Image>().color = new Color(0, 0, 0, colo);
            }
            else
            {
                fadeout = false;
            }
        }
    }
}

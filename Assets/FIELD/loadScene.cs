using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadScene : MonoBehaviour
{
    GameObject info;
    public string scenename;
    public void loadSCene()
    {
        SceneManager.LoadScene(scenename);
        info = FindObjectOfType<playerinfo>().gameObject;
        if(!info.GetComponent<playerinfo>().ondesblabla)
        {
            DontDestroyOnLoad(info);
        }
        
    }
}

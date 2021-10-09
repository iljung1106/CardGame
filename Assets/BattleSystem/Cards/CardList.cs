using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class CardList : MonoBehaviour
{
    public static CardList instance = null;
    public List<GameObject> cardObjects;
    public Dictionary<Card, GameObject> cardDictionary;
    public List<Card> cards;

    void OnEnable()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

#if UNITY_EDITOR
        string[] b = AssetDatabase.FindAssets("t:prefab");
        string[] a = Array.ConvertAll<string, string>(b, AssetDatabase.GUIDToAssetPath);
        foreach (string sAssetPath in a)
        {
            GameObject p = AssetDatabase.LoadAssetAtPath(sAssetPath, typeof(GameObject)) as GameObject;
            Debug.Log("1");
            Debug.Log(sAssetPath);
            if (p!=null && p.GetType().Equals(typeof(GameObject)))
            {
                if (((GameObject)p).GetComponent<Card>() != null)
                {
                    cardObjects.Add((GameObject)p);
                }
            }
        }
#endif

        foreach (GameObject o in cardObjects)
        {
            cards.Add(o.GetComponent<Card>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

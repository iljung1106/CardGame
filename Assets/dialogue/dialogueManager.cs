using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dialogueManager : MonoBehaviour
{
    public static dialogueManager instance;
    public Image bigcard;
    private Queue<string> sentences;
    public Text tex;
    public bool shouldOnAlways = false;
    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        sentences = new Queue<string>();
    }
    public void SetText(dialog sentencesa)
    {
        bigcard.color = Color.white;
        tex.text = sentencesa.name + "\n";
        foreach (string t in sentencesa.sentences) 
        {
            tex.text = tex.text + t + "\n";
        }
    }

    public void DeSetText()
    {
        if (!shouldOnAlways)
        {
            tex.text = " ";
            bigcard.color = Color.clear;
        }
    }
    public void startDialogue(dialog sentencesa)
    {
        foreach (string sentence in sentencesa.sentences)
        {
            sentences.Enqueue(sentence);
        }
        NextDialog();
    }
    public void NextDialog()
    {
        if (sentences.Count <= 0)
        {
            Enddialog();
        }
        else
        {
            string sentence = sentences.Dequeue();
            tex.text = sentence;
            bigcard.color = new Color(1, 1, 1, 1);
        }
    }
    public void Enddialog()
    {
        tex.text = " ";
        bigcard.color = new Color(0, 0, 0, 0);
    }
}

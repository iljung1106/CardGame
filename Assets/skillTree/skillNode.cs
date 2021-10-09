using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skillNode : MonoBehaviour
{
    [System.Serializable]
    public struct nextNode
    {
        public skillNode Node;
    }
    public List<nextNode> nextNodes;
    public skillNode previousNode;

    public bool activated;
    public bool focused = false;

    private void OnMouseDown()
    {
        print("clicked");
        if(!focused)
            focus();
    }
    private void focus()
    {
        focused = true;
        FindObjectOfType<skillTreeManager>().onFocus(this);
        GetComponent<newFocus>().focus(this);
    }
    public void activate()
    {
        if (previousNode.activated && FindObjectOfType<skillTreeManager>().skillCount > 0)
        {
            activated = true;
            GetComponent<SpriteRenderer>().color = new Color(0.9f, 0.9f, 0.9f, 0.5f);
        }
    }
}

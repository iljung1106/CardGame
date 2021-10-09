using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skillTreeManager : MonoBehaviour
{
    public Transform pagetarget;
    public skillNode selectedNode;
    public int page;
    public List<Transform> cameraFocus;
    public int skillCount;
    public GameObject UI;
    public void pageChage(int change)
    {
        page = change;
    }
    public void onFocus(skillNode targ)
    {
        selectedNode = targ;
        UI.SetActive(true);
    }
    public void onFocusOut()
    {
        selectedNode.gameObject.GetComponent<focus>().outFo();
        selectedNode.focused = false;
        UI.SetActive(false);
    }
    public void upgradeTry()
    {
        selectedNode.activate();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pageSelect : MonoBehaviour
{
    public int pagenumber;
    public void buttonPushed()
    {
        if (FindObjectOfType<skillTreeManager>().page != pagenumber)
        {
            //카메라가 추적할 페이지를 지정된 공간으로
            //n초 기다리기
            //버튼 페이지 변환
            FindObjectOfType<cmaST>().target = FindObjectOfType<skillTreeManager>().cameraFocus[pagenumber];
            FindObjectOfType<skillTreeManager>().pageChage(pagenumber);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthText : MonoBehaviour
{
    [SerializeField]
    private CardController cardController;
    [SerializeField]
    private UnityEngine.UI.Text uiText;

    private string showingText;
    void Start()
    {

    }

    void Update()
    {
        //UI에 현재 [사용가능한 마나 / 이 게임에서의 최대마나] 를 표시함
        showingText
            = cardController.battlePlayer.GetHealth().ToString();

        uiText.text = showingText;
    }
}

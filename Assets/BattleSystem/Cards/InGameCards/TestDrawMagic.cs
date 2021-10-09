using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDrawMagic : Card
{
    // Start is called before the first frame update
    void Start()
    {

    }

    override public void SetCard()
    {
        controller.battlePlayer.AddMana(-cost);
        controller.DrawCard();
        controller.SetCardOnTomb(this);
    }
    override public void SetCard(Card selectedCard)
    {

    }

    public override void UseEffecttoEnemy() 
    {

    }
}

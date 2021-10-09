using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class subject3 : Card
{
    public GameObject subject2;
    // Start is called before the first frame update
    void Start()
    {
        isMonster = true;
        isSelectMagic = false;
    }

    public override void SetCard()
    {
        base.SetCard();
        Card summonedUnit = Instantiate(subject2, transform.position, transform.rotation).GetComponent<Card>();
        controller.FieldPlayerCard.Add(summonedUnit);
        summonedUnit.controller = controller;
        summonedUnit.isUsed = true;
        summonedUnit.isPlayers = true;
    }
}


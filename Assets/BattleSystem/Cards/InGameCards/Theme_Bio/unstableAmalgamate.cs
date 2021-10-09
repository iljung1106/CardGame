using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unstableAmalgamate : Card
{
    public GameObject subject1;
    // Start is called before the first frame update
    void Start()
    {
        isMonster = true;
        isSelectMagic = false;
    }



    protected override void DeathEffect(Card attackedCard)
    {
        base.DeathEffect(attackedCard);
        Card summonedUnit = Instantiate(subject1, transform.position, transform.rotation).GetComponent<Card>();
        controller.FieldPlayerCard.Add(summonedUnit);
        summonedUnit.controller = controller;
        summonedUnit.isUsed = true;
        summonedUnit.isPlayers = true;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mutation_minor : Card
{
    // Start is called before the first frame update
    void Start()
    {

    }

    override public void SetCard()
    {
        Debug.Log("대상을 지정하십시오");
    }
    override public void SetCard(Card selectedCard)
    {
        if (isPlayers && selectedCard.IsMonster())
        {
            controller.battlePlayer.AddMana(-cost);
            isUsed = true;
            int unitCost = selectedCard.GetCost();
            GameObject effect = Instantiate(attackEffect, transform.position, transform.rotation);
            AttackEffect attack = effect.GetComponent<AttackEffect>();
            attack.Target = (Vector2)selectedCard.transform.position;
            Vector3 cardPosition = selectedCard.transform.position;
            bool isUnitPlayers = selectedCard.isPlayers;
            int cardFieldIndex = 0;
            if (isUnitPlayers)
            {
                cardFieldIndex = controller.FieldPlayerCard.IndexOf(selectedCard);
            }
            else
            {
                cardFieldIndex = controller.FieldEnemyCard.IndexOf(selectedCard);
            }
            List<GameObject> costFitCard = new List<GameObject>();
            for (int i = 0; i < CardList.instance.cardObjects.Count; i++)
            {
                if (CardList.instance.cards[i].GetCost() == unitCost && CardList.instance.cards[i].IsMonster())
                {
                    costFitCard.Add(CardList.instance.cardObjects[i]);
                }
            }
            int randomIndex = Random.RandomRange(0, costFitCard.Count);
            GameObject summonedObject;
            if (costFitCard.Count != 0)
            {
                summonedObject = Instantiate(costFitCard[randomIndex], cardPosition, transform.rotation);
                Card summonedUnit = summonedObject.GetComponent<Card>();
                summonedUnit.controller = controller;
                controller.SetCardOnTomb(selectedCard);
                if (isUnitPlayers)
                {
                    summonedUnit.isUsed = true;
                    summonedUnit.isPlayers = true;
                    controller.FieldPlayerCard.Insert(cardFieldIndex, summonedUnit);
                }
                else
                {
                    summonedUnit.isUsed = true;
                    summonedUnit.isPlayers = false;
                    controller.FieldEnemyCard.Insert(cardFieldIndex, summonedUnit);

                }
            }
            else
            {

            }


            controller.SetCardOnTomb(this);
        }
    }

    public override void UseEffecttoEnemy() //상대 명치를 직접 칠 경우 효과 발동을 따로 구현 필요
    {

    }
}

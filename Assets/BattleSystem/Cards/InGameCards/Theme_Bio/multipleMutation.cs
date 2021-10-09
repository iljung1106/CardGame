using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class multipleMutation : Card
{
    // Start is called before the first frame update
    void Start()
    {

    }

    //이 카드 상태 존나 개판이니 수정할 일 없길 빈다.
    override public void SetCard()
    {
        controller.battlePlayer.AddMana(-cost);
        Card selectedCard;
        for (int a = 0; a < controller.FieldPlayerCard.Count; a++)
        {
            selectedCard = controller.FieldPlayerCard[a];
            if (isPlayers && selectedCard.IsMonster())
            {
                int unitCost = selectedCard.GetCost() + 1;
                Vector3 cardPosition = selectedCard.transform.position;
                int cardFieldIndex = controller.FieldPlayerCard.IndexOf(selectedCard);
           
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
                    summonedUnit.isUsed = true;
                    summonedUnit.isPlayers = true;
                    controller.FieldPlayerCard.Insert(cardFieldIndex, summonedUnit);
                }
                else
                {
                    selectedCard.SetCost(selectedCard.GetCost() + 1);
                    selectedCard.SetAttackDamage(selectedCard.GetAttackDamage() + 1);
                    selectedCard.SetHealth(selectedCard.GetHealth() + 1);
                }
            }
        }

        controller.SetCardOnTomb(this);
    }
}

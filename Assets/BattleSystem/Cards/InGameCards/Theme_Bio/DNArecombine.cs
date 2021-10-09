using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNArecombine : Card
{
    // Start is called before the first frame update
    void Start()
    {

    }

    override public void SetCard()
    {
        List<Card> handUnit = new List<Card>();
        foreach(Card c in controller.handCard)
        {
            if (c.IsMonster())
                handUnit.Add(c);
        }
        if(handUnit.Count >= 2)
        {
            int n1 = Random.Range(0, handUnit.Count);
            Card c1 = handUnit[n1];
            handUnit.Remove(c1);
            int n2 = Random.Range(0, handUnit.Count);
            Card c2 = handUnit[n2];
            handUnit.Remove(c2);

            List<GameObject> costMatchCardObjects1 = new List<GameObject>();
            for (int i = 0; i < CardList.instance.cards.Count; i++)
            {
                if (CardList.instance.cards[i].GetCost() == c1.GetCost() + 1 && CardList.instance.cards[i].IsMonster())
                {
                    costMatchCardObjects1.Add(CardList.instance.cardObjects[i]);
                }
            }
            List<GameObject> costMatchCardObjects2 = new List<GameObject>();
            for (int i = 0; i < CardList.instance.cards.Count; i++)
            {
                if (CardList.instance.cards[i].GetCost() == c2.GetCost() + 1 && CardList.instance.cards[i].IsMonster())
                {
                    costMatchCardObjects2.Add(CardList.instance.cardObjects[i]);
                }
            }

            Card summonedCard1;
            Card summonedCard2;
            if (costMatchCardObjects1.Count > 0)
            {
                summonedCard1 = Instantiate(
                    costMatchCardObjects1[Random.Range(0, costMatchCardObjects1.Count)], 
                    c1.transform.position, 
                    c1.transform.rotation)
                    .GetComponent<Card>();
                controller.SetCardOnTomb(c1);
                summonedCard1.controller = controller;
                controller.hand.Add(summonedCard1.gameObject);
                controller.handCard.Add(summonedCard1);
            }
            if (costMatchCardObjects2.Count > 0)
            {
                summonedCard2 = Instantiate(
                    costMatchCardObjects2[Random.Range(0, costMatchCardObjects2.Count)],
                    c2.transform.position,
                    c2.transform.rotation)
                    .GetComponent<Card>();
                controller.SetCardOnTomb(c2);
                summonedCard2.controller = controller;
                controller.hand.Add(summonedCard2.gameObject);
                controller.handCard.Add(summonedCard2);
            }
            controller.battlePlayer.AddMana(-cost);
            controller.SetCardOnTomb(this);
        }
        else if (handUnit.Count == 1)
        {
            Card c1 = handUnit[0];
            List<GameObject> costMatchCardObjects1 = new List<GameObject>();
            for (int i = 0; i < CardList.instance.cards.Count; i++)
            {
                if (CardList.instance.cards[i].GetCost() == c1.GetCost() + 1)
                {
                    costMatchCardObjects1.Add(CardList.instance.cardObjects[i]);
                }
            }
            Card summonedCard1;
            if (costMatchCardObjects1.Count > 0)
            {
                summonedCard1 = Instantiate(
                    costMatchCardObjects1[Random.Range(0, costMatchCardObjects1.Count)],
                    c1.transform.position,
                    c1.transform.rotation)
                    .GetComponent<Card>();
                controller.SetCardOnTomb(c1);
                summonedCard1.controller = controller;
                controller.hand.Add(summonedCard1.gameObject);
                controller.handCard.Add(summonedCard1);
            }
            controller.battlePlayer.AddMana(-cost);
            controller.SetCardOnTomb(this);
        }
        else
        {
            return;
        }
    }
    override public void SetCard(Card selectedCard)
    {
    }

    public override void UseEffecttoEnemy() //상대 명치를 직접 칠 경우 효과 발동을 따로 구현 필요
    {

    }
}

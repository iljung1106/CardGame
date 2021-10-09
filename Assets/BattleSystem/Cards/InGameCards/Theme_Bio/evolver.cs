using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class evolver : Card
{
    // Start is called before the first frame update
    void Start()
    {
        isMonster = true;
        isSelectMagic = false;
    }

    protected override void AttackEffect(Card selectedCard)
    {//selectedCard가 적의 몬스터 카드이고, 소환된 카드일 때
        if (selectedCard.IsMonster() && selectedCard.isUsed && selectedCard.isPlayers != isPlayers)
        {
            bool shouldEvolve = false;
            selectedCard.GiveDamage(attackDamage, this); 
            if (selectedCard.GetHealth() <= attackDamage)
            {
                shouldEvolve = true;
            }
            GiveDamage(selectedCard.GetAttackDamage(), selectedCard);
            canAttack = false;
            GameObject effect = Instantiate(attackEffect, transform.position, transform.rotation);
            AttackEffect attack = effect.GetComponent<AttackEffect>();
            attack.Target = (Vector2)selectedCard.transform.position;
            if (shouldEvolve)
            {
                int unitCost = this.GetCost() + 2;
                Vector3 cardPosition = this.transform.position;
                bool isUnitPlayers = this.isPlayers;
                int cardFieldIndex = 0;
                if (isUnitPlayers)
                {
                    cardFieldIndex = controller.FieldPlayerCard.IndexOf(this);
                }
                else
                {
                    cardFieldIndex = controller.FieldEnemyCard.IndexOf(this);
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
                    controller.SetCardOnTomb(this);
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
                    this.cost += 2;
                    this.SetAttackDamage(this.GetAttackDamage() + 2);
                    this.SetHealth(this.GetHealth() + 1);
                }
            }
        }
    }
}



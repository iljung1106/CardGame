using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class predation : Card
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
            selectedCard.GiveDamage(-2, this);
            if (controller.FieldPlayerCard.Contains(selectedCard))
            {
                int indexOfCard = controller.FieldPlayerCard.IndexOf(selectedCard);
                int countOfCards = controller.FieldPlayerCard.Count;
                if (indexOfCard < countOfCards - 1)
                {
                    controller.FieldPlayerCard[indexOfCard + 1].GiveDamage(1, this);
                }
                if (indexOfCard > 0)
                {
                    controller.FieldPlayerCard[indexOfCard - 1].GiveDamage(1, this);
                }
            }
            else if (controller.FieldEnemyCard.Contains(selectedCard))
            {
                int indexOfCard = controller.FieldEnemyCard.IndexOf(selectedCard);
                int countOfCards = controller.FieldEnemyCard.Count;
                if (indexOfCard < countOfCards - 1)
                {
                    controller.FieldEnemyCard[indexOfCard + 1].GiveDamage(1, this);
                }
                if (indexOfCard > 0)
                {
                    controller.FieldEnemyCard[indexOfCard - 1].GiveDamage(1, this);
                }
            }
            GameObject effect = Instantiate(attackEffect, transform.position, transform.rotation);
            AttackEffect attack = effect.GetComponent<AttackEffect>();
            attack.Target = (Vector2)selectedCard.transform.position;
            controller.SetCardOnTomb(this);
        }
    }

    public override void UseEffecttoEnemy() //상대 명치를 직접 칠 경우 효과 발동을 따로 구현 필요
    {

    }
}

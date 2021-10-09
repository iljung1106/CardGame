using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class predator : Card
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
            selectedCard.GiveDamage(attackDamage, this);
            if (selectedCard.GetHealth() <= attackDamage)
            {
                GiveDamage(-2, this);
            }
            GiveDamage(selectedCard.GetAttackDamage(), selectedCard);
            canAttack = false;
            GameObject effect = Instantiate(attackEffect, transform.position, transform.rotation);
            AttackEffect attack = effect.GetComponent<AttackEffect>();
            attack.Target = (Vector2)selectedCard.transform.position;
        }

    }
}


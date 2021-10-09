using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMagic : Card
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
        if (isPlayers && selectedCard.isPlayers == false && selectedCard.IsMonster())
        {
            controller.battlePlayer.AddMana(-cost);
            isUsed = true;
            selectedCard.GiveDamage(2, this);
            GameObject effect = Instantiate(attackEffect, transform.position, transform.rotation);
            AttackEffect attack = effect.GetComponent<AttackEffect>();
            attack.Target = (Vector2)selectedCard.transform.position;
            controller.SetCardOnTomb(this);
        }
    }

    public override void UseEffecttoEnemy() //상대 명치를 직접 칠 경우 효과 발동을 따로 구현 필요
    {
        base.UseEffecttoEnemy();
        controller.battlePlayer.AddMana(-cost);
        isUsed = true;
        GameObject effect = Instantiate(attackEffect, transform.position, transform.rotation);
        AttackEffect attack = effect.GetComponent<AttackEffect>();
        attack.Target = (Vector2)controller.enemyTarget.transform.position;
        controller.battleEnemy.GiveDamage(2);
        Debug.Log("giveDamage to Enemy");
        controller.SetCardOnTomb(this);
    }
}

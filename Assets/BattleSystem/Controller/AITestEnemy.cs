using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITestEnemy : MonoBehaviour
{
    [SerializeField]
    BattleEnemy enemy;

    public GameObject testCard;
    private int turnCount = 0;
    private List<GameObject> availableCardObjects = new List<GameObject>();
    GameObject selectedObject;
    bool didTurn = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(enemy.IsTurn() && !didTurn)
        {
            didTurn = true;
            StartCoroutine(waitUntilPlayerReady());
        }
    }

    IEnumerator waitUntilPlayerReady()
    {
        yield return new WaitForSeconds(1f);
        TurnAct();
    }
    IEnumerator waitUntilTurnEnd()
    {
        yield return new WaitForSeconds(1f);
        enemy.EndTurn();
        didTurn = false;
    }

    void TurnAct()
    {
        availableCardObjects.Clear();
        turnCount++;
        for (int i = 0; i < CardList.instance.cards.Count; i++)
        {
            if (CardList.instance.cards[i].GetCost() == Mathf.Clamp(turnCount, 1, 8) && CardList.instance.cards[i].IsMonster())
            {
                availableCardObjects.Add(CardList.instance.cardObjects[i]);
            }
        }
        if (availableCardObjects.Count == 0)
        {
            availableCardObjects.Add(testCard);
        }
        Card cardTmp = Instantiate(availableCardObjects[Random.Range(0, availableCardObjects.Count)], transform).GetComponent<Card>();

        foreach (Card c in enemy.cardController.FieldEnemyCard)
        {
            if (c.canAttack)
            {
                if (enemy.cardController.FieldPlayerCard.Count == 0)
                {
                    c.UseCardtoPlayer();
                }
                else
                {
                    c.UseCard(enemy.cardController.FieldPlayerCard[Random.Range(0, enemy.cardController.FieldPlayerCard.Count)]);
                }
            }
        }

        cardTmp.controller = enemy.cardController;
        enemy.cardController.FieldEnemyCard.Add(cardTmp);
        cardTmp.isPlayers = false;
        cardTmp.UseCard();
        enemy.cardController.enemyHandCount--;
        StartCoroutine(waitUntilTurnEnd());
    }
}

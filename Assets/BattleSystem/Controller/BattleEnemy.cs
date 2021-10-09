using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleEnemy : MonoBehaviour
{
    public CardController cardController;
    [SerializeField]
    public int health = 50;
    [SerializeField]
    private int mana = 0;
    [SerializeField]
    private int maxMana = 10;


    [SerializeField]
    private Text healthText;

    private bool isTurn = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(healthText != null)
        {
            healthText.text = health.ToString() ;
        }
    }

    public void StartTurn()
    {
        isTurn = true;
    }

    public bool IsTurn()
    {
        return isTurn;
    }

    public void EndTurn()
    {
        cardController.EnemyTurnEnd();
        isTurn = false;
    }

    public void GiveDamage(int dmg)
    {
        health -= dmg;
    }
}

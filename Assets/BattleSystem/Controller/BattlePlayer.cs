using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePlayer : MonoBehaviour
{
    [SerializeField]
    private int health = 50;
    [SerializeField]
    private int mana = 0;
    [SerializeField]
    private int maxMana = 1;
    [SerializeField]
    private int maxManaLimit = 10;


    public int maxHand = 7;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetMana()
    {
        return mana;
    }

    public void AddMana(int add)
    {
        mana += add;
        if (mana > maxMana)
        {
            mana = maxMana;
        }
        else if (mana < 0)
        {
            mana = 0;
        }
    }

    public void IncreaseMaxMana(int add)
    {
        maxMana += add;
        if(maxManaLimit < maxMana)
        {
            maxMana = maxManaLimit;
        }
    }
    public int GetMaxMana()
    {
        return maxMana;
    }

    public int GetHealth()
    {
        return health;
    }

    public void AddHealth(int add)
    {
        health += add;
        if (health < 0)
        {

        }
    }
}

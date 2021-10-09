using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Card : MonoBehaviour
{
    //황수호 추가
    [SerializeField]
    bool ispickeduppast = false;

    [SerializeField]
    TextMesh costText;
    [SerializeField]
    TextMesh attackText;
    [SerializeField]
    TextMesh healthText;
    [SerializeField]
    GameObject cardActiveVeil;

    public GameObject deathEffect;
    public GameObject attackEffect;

    protected Vector2 mouseRelativePos;
    Collider2D col;
    public CardController controller;
    [SerializeField]
    protected bool isMoving = false;

    protected bool isUsed = false;

    [SerializeField]
    protected int cost = 1;

    [SerializeField]
    protected bool isMonster = true;
    [SerializeField]
    protected int health = 5;
    [SerializeField]
    protected int attackDamage = 2;

    public bool isPlayers = true;
    public bool canAttack = false;
    [SerializeField]
    protected bool isSelectMagic = false;

    void Start()
    {
        col = GetComponent<Collider2D>();
    }

    void Update()
    {
        costText.text = cost.ToString();
        if (isMonster)
        {
            attackText.text = GetAttackDamage().ToString();
            healthText.text = GetHealth().ToString();
        }
        else
        {
            attackText.text = "";
            healthText.text = "";
        }

        //카드의 가시성을 높히기 위해 카드를 움직이는 도중에는 크기를 키웠음
        //todo : 카드를 바로 1.5배로 키우는것과, 보간하는 것 중 나은 쪽을 선택
        if (IsMoving())
        {
            transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (ispickeduppast)
        {
            if (!isMoving)
            {
                print("movestop");

                FindObjectOfType<dialogueManager>().GetComponent<dialogueManager>().Enddialog();
                ispickeduppast = false;
            }
        }

        //카드가 사용 가능한지 표시하는 기능 (카드 위에 검은 반투명 스프라이트를 올렸음)
        //임시
        bool notAttackableMonster = isMonster && !canAttack &&isUsed;
        bool notUseableHandCard = !isUsed && !controller.IsCardAble(cost);
        if (notAttackableMonster || notUseableHandCard)
        {
            cardActiveVeil.SetActive(true);
        }
        else
        {
            cardActiveVeil.SetActive(false);
        }
    }

    protected void OnMouseDown()
    {

    }

    protected void OnMouseDrag()
    {
        isMoving = true;
        Vector3 tmpVector = transform.position;
        tmpVector.z = -3f;
        transform.position = tmpVector;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //지금 들고있는 카드의 위치보다 아래(레이어 느낌으로)에서 레이캐스트를 시작하기 위해 z좌표 수정
        mousePosition.z = -2.7f;
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, transform.forward, 100);

        //마우스 위치가 카드일 경우 실행
        if (isSelectMagic && hit && hit.collider.GetComponent<Card>() != null && hit.collider.GetComponent<Card>().isUsed)
        {
            controller.SelectPosition(hit.point);

        }
        else if (hit && hit.collider.CompareTag("Card"))  //터치하고 있는 부분이 카드일 경우
        {
            Card hitCard = hit.collider.GetComponent<Card>();
            if (hitCard.isMonster && hitCard.isUsed && hitCard.isPlayers != isPlayers)
            {
                controller.SelectPosition(hit.point);
            }
            GetComponent<dialogTrigger>().startDialog();
            ispickeduppast = true;
        }
        else if (hit && hit.collider.CompareTag("EnemyTarget")) //터치하고 있는 부분이 명치인 경우
        {
            controller.SelectPosition(hit.point);
        }

        //아직 사용하지 않은 카드인 경우에
        if (!isUsed && isPlayers)
        {
            //마우스의 위치를 월드좌표계로 변환, 카드를 그 위치로 이동
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = -3;
            if (!isSelectMagic)  //선택해서 발동하는 카드인 경우
            {
                transform.position = mousePosition;
            }
            else
            {

            }
        }


    }
    protected void OnMouseUp()
    {
        Vector3 tmpVector = transform.position;
        tmpVector.z = -0.4f;
        transform.position = tmpVector;
        if (!isPlayers)
        {
            return;
        }

        isMoving = false;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //지금 들고있는 카드의 위치보다 아래(레이어 느낌으로)에서 레이캐스트를 시작하기 위해 z좌표 수정
        mousePosition.z = -2.7f;
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, transform.forward, 100);

        //마우스 위치가 카드일 경우 실행
        if (hit && hit.collider.GetComponent<Card>() != null)
        {
            Card selectedCard = hit.collider.GetComponent<Card>();
            UseCard(selectedCard);
        }
        else if (hit && hit.collider.CompareTag("EnemyTarget"))
        {
            //선택된게 적 본체일 경우
            UseCardtoEnemy();
        }
        else if (Input.mousePosition.y > Screen.height / 2 && controller.IsCardAble(cost))
        {
            //드래그하고 뗀 위치가 화면의 중간에서 위쪽인 경우
            UseCard();
        }
    }
    protected void OnMouseExit()
    {
        isMoving = false;
    }

    public bool IsMoving()
    {
        return isMoving;
    }

    public void UseCard()
    {
        //사용 안한 카드면 필드에 세트
        if (!isUsed)
        {
            SetCard();
        }
        else
        {

        }
    }
    public void UseCardtoEnemy() //적 명치 때리기
    {
        //공격 가능한 몬스터인 경우
        if (isUsed && isMonster && canAttack)
        {
            controller.battleEnemy.GiveDamage(attackDamage);
            GameObject effect = Instantiate(attackEffect, transform.position, transform.rotation);
            AttackEffect attack = effect.GetComponent<AttackEffect>();
            attack.Target = (Vector2)controller.enemyTarget.transform.position;
            UseEffecttoEnemy();
        }
        else if (!isUsed && controller.IsCardAble(cost) && isSelectMagic)
        {
            UseEffecttoEnemy();
        }
    }

    virtual public void UseEffecttoEnemy() //상대 명치를 직접 칠 경우 효과 발동을 따로 구현 필요
    {

    }

    public void StartOfTurn()
    {
        canAttack = true;
    }

    public void UseCard(Card selectedCard)
    {
        //사용 안한 카드면 필드에 세트
        if (!isUsed && controller.IsCardAble(cost) && isSelectMagic)
        {
            SetCard(selectedCard);
        }
        else if (!isUsed && controller.IsCardAble(cost) && !isSelectMagic && Input.mousePosition.y > Screen.height / 2)
        {
            SetCard();
        }
        else if (isMonster && isUsed && canAttack)   //몬스터카드고 이미 소환한 카드일 경우
        {
            //selectedCard가 적의 몬스터 카드이고, 소환된 카드일 때
            if (selectedCard.IsMonster() && selectedCard.isUsed && !selectedCard.isPlayers)
            {
                selectedCard.GiveDamage(attackDamage);
                GiveDamage(selectedCard.GetAttackDamage());
                canAttack = false;
                GameObject effect = Instantiate(attackEffect, transform.position, transform.rotation);
                AttackEffect attack = effect.GetComponent<AttackEffect>();
                attack.Target = (Vector2)selectedCard.transform.position;
            }
        }
    }

    public bool IsMonster()
    {
        return isMonster;
    }
    public int GetAttackDamage()
    {
        return attackDamage;
    }
    public void SetAttackDamage(int newDamage)
    {
        attackDamage = newDamage;
    }
    public int GetHealth()
    {
        return health;
    }
    public void SetHealth(int newHealth)
    {
        health = newHealth;
    }

    public void GiveDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            StartCoroutine(Die());
        }
    }

    //이 카드가 패에 있을 때, 필드에 카드를 냄
    virtual public void SetCard()
    {
        isUsed = true;
        if (isPlayers)
        {
            controller.battlePlayer.AddMana(-cost);
            controller.SetCardOnField(this);
            //마나를 쓰고 
        }
    }

    //즉발효과를 위함
    virtual public void SetCard(Card selectedCard)
    {
        isUsed = true;
        if (isPlayers)
        {
            controller.battlePlayer.AddMana(-cost);
            //즉발효과 발동시 어떻게 구현해야 할 지에 대한 기본형
        }
    }

    protected IEnumerator Die() //TODO 카드 파괴시 발동 효과를 어느쪽에 구현하는게 바람직한지에 대해서 코드 확인 필요
    {
        //임시
        yield return new WaitForEndOfFrame();/*
        controller.FieldPlayerCard.Remove(this);
        controller.FieldEnemyCard.Remove(this);
        controller.hand.Remove(gameObject);
        controller.handCard.Remove(this);
        Destroy(gameObject);*/
        controller.SetCardOnTomb(this);
        //카드를 묘지로 보냄
    }

    public IEnumerator DestroyCard()
    {
        yield return new WaitForSeconds(0.2f);
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, transform.rotation);
        }
        else
        {
            Instantiate(controller.defaultCardDeathEffect, transform.position, transform.rotation);
        }
        transform.position = new Vector3(0, -200, 0);
        gameObject.SetActive(false);
    }
}

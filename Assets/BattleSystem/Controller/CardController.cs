using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CardController : MonoBehaviour
{
    private bool isGameDone = false;


    static float drawSpeed = 3;

    public List<GameObject> gameDeck;

    [SerializeField]
    private GameObject cardBack;
    [SerializeField]
    private Vector3 drawPosition;

    public bool isPlayerTurn = false;

    public bool isMoving = false;

    //이 아래 임시로 만든 변수
    [SerializeField]
    private GameObject turnEndButton;
    //이 위 임시로 만든 변수

    public List<GameObject> hand;
    public List<Card> handCard;

    public List<Card> FieldPlayerCard;
    public List<Card> FieldEnemyCard;

    public List<GameObject> enemyHand = new List<GameObject>(10);
    public bool isEnemyBoss = false;
    public int enemyHandCount = 5;

    public List<GameObject> deck;

    public BattlePlayer battlePlayer;
    public BattleEnemy battleEnemy;


    [SerializeField]
    GameObject selectMarker;
    public GameObject enemyTarget;

    [SerializeField]
    public GameObject defaultCardDeathEffect;
    // Start is called before the first frame update
    void Start()
    {
        battlePlayer = GetComponent<BattlePlayer>();
        if (DeckController.instance != null)
        {
            deck = DeckController.instance.deck;
        }
        SetGameDeck();
        StartDraw();
    }


    void Update()
    {
        if (!isGameDone)
        {
            //이 아래 테스트 빌드용 임시 코드
            if (battlePlayer.GetHealth() <= 0)
            {
                SceneManager.LoadScene("Lose");
                isGameDone = true;
            }
            else if (battleEnemy.health <= 0)
            {
                SceneManager.LoadScene("Win");
                isGameDone = true;
            }
        }

        //이 위 테스트 빌드용 임시 코드



        //todo
        //Update문에서 처리하는 정렬이 보기에 불편하므로 코드 정리

        if (FieldPlayerCard.Count > 6)
        {
            SetCardOnTomb(FieldPlayerCard[6]);
        }
        if (FieldEnemyCard.Count > 6)
        {
            SetCardOnTomb(FieldEnemyCard[6]);
        }

        if (FieldEnemyCard.Count > 1)
        {
            for (int i = 0; i < FieldEnemyCard.Count; i++)
            {
                if (FieldEnemyCard[i].IsMoving())
                {
                    continue;
                }
                FieldEnemyCard[i].transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
                FieldEnemyCard[i].transform.position = 
                    Vector3.Lerp(
                        FieldEnemyCard[i].transform.position, 
                        new Vector3(3.2f * (i - ((float)FieldEnemyCard.Count / 2.0f)) + 0.9f, 11f, -0.02f * i), 
                        drawSpeed * Time.deltaTime);
            }
        }
        else if (FieldEnemyCard.Count == 1)
        {
            FieldEnemyCard[0].transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
            FieldEnemyCard[0].transform.position = Vector3.Lerp(FieldEnemyCard[0].transform.position, new Vector3(0, 11f, 0), drawSpeed * Time.deltaTime);

        }
        if (isEnemyBoss)
        {
            //적에게 실제로 손패가 구현된 경우에 사용할 부분, 구현 필요
        }
        else
        {
            for(int i = 0; i < enemyHandCount; i++)
            {
                if(enemyHand.Count < enemyHandCount)
                {
                    enemyHand.Add(Instantiate(cardBack, new Vector3(0.6f, 2f, 0.6f), transform.rotation));
                }
                enemyHand[i].transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                enemyHand[i].transform.position =
                    Vector3.Lerp(
                        enemyHand[i].transform.position,
                        new Vector3(1.2f * (i - ((float)enemyHand.Count / 2.0f)) + 0.9f, 15f, -0.02f * i + 0.15f),
                        drawSpeed * Time.deltaTime);
            }
            for(int i = enemyHandCount - 1; i < enemyHand.Count; i++)
            {
                enemyHand[i].transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
                enemyHand[i].transform.position = new Vector3(0.9f, 30f, 0);
            }
        }


        //-9 ~9 x좌표로 플레이어 필드 정렬 
        if (FieldPlayerCard.Count > 1)
        {
            for (int i = 0; i < FieldPlayerCard.Count; i++)
            {
                if (FieldPlayerCard[i].IsMoving())
                {
                    continue;
                }
                FieldPlayerCard[i].transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                FieldPlayerCard[i].transform.position = 
                    Vector3.Lerp(
                        FieldPlayerCard[i].transform.position, 
                        new Vector3(3.7f * (i - ((float)FieldPlayerCard.Count / 2.0f)) + 1.1f, 3f, -0.02f * i), 
                        drawSpeed * Time.deltaTime);
                //FieldPlayerCard[i].transform.position = Vector3.Lerp(FieldPlayerCard[i].transform.position, new Vector3((16.0f / (FieldPlayerCard.Count - 1)) * i - 8, 5f, 0), drawSpeed * Time.deltaTime);
            }
        }
        else if(FieldPlayerCard.Count == 1)
        { 
            FieldPlayerCard[0].transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            FieldPlayerCard[0].transform.position = Vector3.Lerp(FieldPlayerCard[0].transform.position, new Vector3(0, 3f, 0), drawSpeed * Time.deltaTime);

        }

        //-9 ~9 x좌표로 손패 정렬 
        if (hand.Count > 1)
        {
            for (int i = 0; i < hand.Count; i++)
            {
                if (handCard[i].IsMoving())
                {
                    continue;
                }
                hand[i].transform.position = Vector3.Lerp(hand[i].transform.position, 
                    new Vector3(2.2f * (i- ((float)hand.Count / 2.0f) - 0.8f), -8f, -0.1f - 0.02f*i), 
                    drawSpeed * Time.deltaTime);
                Vector3 tmpPos = hand[i].transform.position;
                tmpPos.z = -0.1f - 0.02f * i;
                hand[i].transform.position = tmpPos;
            }
        }
        else if (hand.Count == 1)
        {
            hand[0].transform.position = Vector3.Lerp(hand[0].transform.position, new Vector3(0, -2, -0.1f), drawSpeed * Time.deltaTime);

        }

    }

    private void LateUpdate()
    {
        if (!isMoving)
        {
            dialogueManager.instance.DeSetText();
        }
        isMoving = false;
        StartCoroutine(EndOfFrame());
    }

    IEnumerator EndOfFrame()
    {
        yield return new WaitForEndOfFrame();

        //선택표시 비활성화
        selectMarker.SetActive(false);
    }

    //비전투 상황에서 구축한 덱을 전투용으로 복제
    public void SetGameDeck()
    {
        gameDeck.AddRange(deck.ToArray());
    }

    public void StartDraw()
    {
        //덱 셔플
        for (int i = 0; i < gameDeck.Count; i++)
        {
            int randomIndex = Random.Range(0, gameDeck.Count);
            GameObject tmpObject = gameDeck[randomIndex];
            gameDeck[randomIndex] = gameDeck[i];
            gameDeck[i] = tmpObject;
        }
        for(int i = 0; i < 5 ; i++)
        {
            DrawCard();
        }

        //테스트 코드
        PlayerTurnStart();
    }

    //카드를 손패로 가져옴. 
    //덱에서 카드를 가져와 instantiate해 hand리스트에 게임오브젝트, handCard리스트에 Card 클래스 저장
    //그 후 gameDeck에서 그 카드는 삭제됨
    public void DrawCard()
    {
        //일부 수정됨 - 위의 StartDraw 함수 참고
        if (gameDeck.Count == 0) 
        {
            //todo 덱 없을 경우 탈진 데미지 추가
            Debug.Log("덱 없음");
            return;
        }
        GameObject cardTemp = Instantiate(gameDeck[0], drawPosition, transform.rotation);
        Card cardCard = cardTemp.GetComponent<Card>();
        cardCard.controller = this;

        //패 최대 제한과 비교해서 현재 패가 같거나 많을 경우
        if (battlePlayer.maxHand <= hand.Count)
        {
            SetCardOnTomb(cardCard);
            Debug.Log("패 과다");
        }
        else
        {
            hand.Add(cardTemp);
            handCard.Add(cardCard);
        }
        gameDeck.RemoveAt(0);
    }

    //인자로 전달받은 카드를 hand와 handCard에서 제거, FieldPlayerCard에 추가
    //FieldPlayerCard에 들어간 카드는 자동으로 정렬됨
    public void SetCardOnField(Card card) 
    {
        hand.Remove(card.gameObject);
        handCard.Remove(card);
        FieldPlayerCard.Add(card);
    }

    public void SetCardOnTomb(Card card)
    {
        hand.Remove(card.gameObject);
        handCard.Remove(card);
        FieldPlayerCard.Remove(card);
        FieldEnemyCard.Remove(card);
        card.StartCoroutine(card.DestroyCard());
        /*
        if (card.deathEffect != null)
        {
            Instantiate(card.deathEffect, card.transform.position, card.transform.rotation);
        }
        else
        {
            Instantiate(defaultCardDeathEffect, card.transform.position, card.transform.rotation);
        }
        card.transform.position = new Vector3(0, -200, 0);
        card.gameObject.SetActive(false);*/
    }

    public bool IsCardAble(int cost)
    {
        if(cost <= battlePlayer.GetMana())
        {
            return true;
        }
        return false;
    }


    public void PlayerTurnStart()
    {
        turnEndButton.SetActive(true);
        isPlayerTurn = true;
        DrawCard();
        battlePlayer.IncreaseMaxMana(1);
        battlePlayer.AddMana(battlePlayer.GetMaxMana());
        foreach(Card c in FieldPlayerCard)
        {
            c.StartOfTurn();
        }
    }

    public void PlayerTurnEnd()
    {
        turnEndButton.SetActive(false);
        if (isPlayerTurn)
        {
            isPlayerTurn = false;
            EnemyTurnStart();
        }
    }

    public void EnemyTurnStart()
    {
        //테스트용 코드
        //EnemyTurnEnd();
        foreach (Card c in FieldEnemyCard)
        {
            c.StartOfTurn();
        }
        enemyHandCount++;
        battleEnemy.StartTurn();
    }

    public void EnemyTurnEnd()
    {
        PlayerTurnStart();
    }

    public void SelectPosition(Vector2 p)
    {
        selectMarker.transform.position = (Vector3)(p) + new Vector3(0,0,-2f);
        selectMarker.SetActive(true);
    }
}

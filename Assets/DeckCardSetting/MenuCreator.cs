using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuCreator : MonoBehaviour
{
    [SerializeField]
    private Text cardCountText;
    public GameObject cardHolder;
    public GameObject selectedCard;
    RectTransform content;
    // Start is called before the first frame update
    void Start()
    {
        if(content == null)
        {
            content = GetComponent<RectTransform>();
        }
        int i = 0;
        foreach (GameObject g in CardList.instance.cardObjects)
        {
            GameObject holder = Instantiate(cardHolder, transform);
            holder.GetComponent<RectTransform>().localPosition = new Vector3(i * 700, -500, 0);
            CardImageHolder c = holder.GetComponent<CardImageHolder>();
            c.menu = this;
            c.original = g;
            c.Show();
            i++;
            content.SetSizeWithCurrentAnchors(new RectTransform.Axis(), i * 700 - 1000);
        }
    }

    // Update is called once per frame

    //이 아래 업데이트 전체가 매우 끔찍하게 구성되어있음
    //조속한 수정이 필요함
    void Update()
    {
        if (selectedCard != null) 
        {
            int count = 0;
            foreach(GameObject g in DeckController.instance.deck)
            {
                if(g == selectedCard)
                {
                    count++;
                }
            }
            cardCountText.text = count.ToString();
        }
    }
    public void AddCardToDeck()
    {
        if (selectedCard != null)
        {
            int count = 0;
            foreach (GameObject g in DeckController.instance.deck)
            {
                if (g == selectedCard)
                {
                    count++;
                }
            }
            if (count < 3)
            {
                DeckController.instance.deck.Add(selectedCard);
            }
        }
    }
    public void RemoveCardFromDeck()
    {
        if (selectedCard != null)
        {
            int count = 0;
            foreach (GameObject g in DeckController.instance.deck)
            {
                if (g == selectedCard)
                {
                    count++;
                }
            }
            if (count > 0)
            {
                DeckController.instance.deck.Remove(selectedCard);
            }
        }
    }
}

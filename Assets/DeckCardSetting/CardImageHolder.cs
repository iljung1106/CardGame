using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardImageHolder : MonoBehaviour
{
    public GameObject original;
    public Card summonedCard;
    public MenuCreator menu;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void Show()
    {
        summonedCard = Instantiate(original, transform).GetComponent<Card>();
        summonedCard.transform.localScale = new Vector3(110, 110, 1);
        summonedCard.transform.localPosition = (transform as RectTransform).rect.center;
        summonedCard.isImage = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (summonedCard.IsMoving())
        {
            menu.selectedCard = original;
        }
    }
}

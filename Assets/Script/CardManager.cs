using Coffee.UIExtensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{

    //Input data
    [SerializeField] private Vector2Int boardSize;


    [SerializeField] private Transform paritcleParent;
    [SerializeField] private RectTransform cardParent;

    [SerializeField] private List<Sprite> cardSprites;
    [SerializeField] private List<UIParticle> hintParticle;

    // Prefab Object
    [SerializeField] private Card cardObject;

    [SerializeField] private UIParticle removeParticle; 

    // Local Data
    private bool _isStop;

    private Card _curSelectCard;

    private List<Card> cardList;
    private List<Card> _removeCardList;

    public static CardManager Instance;

    public void PreInit()
    {
        Instance = this;

        cardList = new List<Card>();
        _removeCardList = new List<Card>();
    }

    public void Init()
    {
       
        if (boardSize.x * boardSize.y % 2 != 0) ++boardSize.y;

        int cardTypeCount = boardSize.x * boardSize.y / 2;

        float offsetX = 200 / 2;
        float offsetY = 300 / 2;


        for (int y = 0; y < boardSize.y; y++)
        {
            for(int x = 0;x<boardSize.x;x++)
            {
                Card card = Instantiate(cardObject, cardParent);

                card.MyRect.anchoredPosition =
                    new Vector2(x * 200 + offsetX, y * -300 - offsetY);

                cardList.Add(card);
            }
        }

        cardParent.sizeDelta = new Vector2(boardSize.x * 200, boardSize.y * 300);

        ShuffleCardSprites();

        List<Card> tempCardList = new List<Card>(cardList);

        for(int i = 0;i< cardTypeCount; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                int randomIndex = Random.Range(0, tempCardList.Count);
                Card card = tempCardList[randomIndex];

                card.Init(i, cardSprites[i]);
                tempCardList.RemoveAt(randomIndex);
            }
        }

    }

    public void Relase()
    {
        foreach(var card in cardList)
        {
            Destroy(card.gameObject);
        }
        cardList.Clear();

        foreach(var card in _removeCardList)
        {
            Destroy(card.gameObject);
        }
        _removeCardList.Clear();
    }

    public bool OnPressdCard(Card card)
    {
        if (_isStop) return false;
        if (card.IsLookAtFront) return false;

        if(_curSelectCard == null)
        {
            _curSelectCard = card;

            return true;
        }

        else
        {
            _removeCardList.Add(_curSelectCard);
            _removeCardList.Add(card);

            if (_curSelectCard.myType == card.myType)
            {
                
                // 지우는 함수
                Invoke("RemoveCard",0.5f);
            }

            else
            {
                // 돌려주는 함수
                Invoke("RotateCard", 0.5f);
            }

            _isStop = true;

            _curSelectCard = null;

            return true;
        }
    }

    private void RemoveCard()
    {
        foreach(var card in _removeCardList)
        {
            cardList.Remove(card);

            Instantiate(removeParticle, paritcleParent).transform.position = card.transform.position;

            card.SetRemoveAnimation();
        }

        _removeCardList.Clear();

        _isStop = false;

        if (cardList.Count <= 0)
            GameManager.Instance.OnGameEnd();
    }

    private void RotateCard()
    {
        foreach(var card in _removeCardList)
        {
            card.SetRotateAnimation(false);
        }

        _removeCardList.Clear();

        _isStop = false;
    }

    public void ShuffleCardSprites()
    {
        int random1;
        int random2;

        for(int index = 0; index < cardSprites.Count; ++ index)
        {
            random1 = Random.Range(0, cardSprites.Count);
            random2 = Random.Range(0, cardSprites.Count);

            var tmp = cardSprites[random1];
            cardSprites[random1] = cardSprites[random2];
            cardSprites[random2] = tmp;
        }
    }

    public void OnPressedHitntButton()
    {
        int randomIdx = Random.Range(0, cardList.Count);
        Card target = cardList[randomIdx];
        Card target2 = null;

        for (var i = 0; i < cardList.Count; i++)
        {
            if (randomIdx == i) continue;
            if (cardList[i].myType != target.myType) continue;

            target2 = cardList[i];
            break;
        }

        target.PlayShinyEffect();
        target2.PlayShinyEffect();

        hintParticle[0].transform.position = target.transform.position;
        hintParticle[0].Stop();
        hintParticle[0].Play();

        hintParticle[1].transform.position = target2.transform.position;
        hintParticle[1].Stop();
        hintParticle[1].Play();
    }

 

}

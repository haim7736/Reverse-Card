using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Coffee.UIExtensions;

public class Card : MonoBehaviour
{
    // Input Data
    [SerializeField] private Image cardImage;
    [SerializeField] private Sprite backCardSprite;
    [SerializeField] private UIShiny uIShinyEffect;

    // Local Data
    [HideInInspector] public int myType;

    private bool _isLookAtFront;
    public bool IsLookAtFront => _isLookAtFront;
 
    
        

    private RectTransform _myRect;

    public RectTransform MyRect
    {
        get
        {
            if (_myRect == null) _myRect = GetComponent<RectTransform>();

            return _myRect;
        }

        private set
        {
            _myRect = value;
        }
    }

    private Sprite _frontCardSprite;

    public void Init(int type, Sprite cardSprite)
    {
        _frontCardSprite = cardSprite;
        myType = type;

        _isLookAtFront = false;
    }

    public void OnPressedCard()
    {
        if(CardManager.Instance.OnPressdCard(this))
        {
            SetRotateAnimation(true);
        }
        
    }

    public void RotateCard(bool isFront)
    {
        if (isFront) cardImage.sprite = _frontCardSprite;
        else cardImage.sprite = backCardSprite;
    }

    public void SetRotateAnimation(bool isFront)
    {
        _isLookAtFront = isFront;

        Sequence rotateSeq = DOTween.Sequence();

        rotateSeq.Append(MyRect.DORotate(new Vector3(0, 90), 0.2f).SetEase(Ease.Linear));
        rotateSeq.Join(MyRect.DOScale(new Vector3(1.2f,1.2f), 0.2f).SetEase(Ease.Linear));
        rotateSeq.AppendCallback(() => RotateCard(_isLookAtFront));
        rotateSeq.Append(MyRect.DORotate(new Vector3(0, 0), 0.2f).SetEase(Ease.Linear));
        rotateSeq.Join(MyRect.DOScale(new Vector3(1f, 1f), 0.2f).SetEase(Ease.Linear));

        transform.SetAsLastSibling();


    }

    public void SetRemoveAnimation()
    {
        MyRect.DORotate(new Vector3(0, 0, 480), 0.3f, RotateMode.FastBeyond360);

        MyRect.DOScale(Vector3.zero, 0.4f).OnComplete(() => Destroy(gameObject));
    }

    public void PlayShinyEffect()
    {
        uIShinyEffect.Play();
    }

}

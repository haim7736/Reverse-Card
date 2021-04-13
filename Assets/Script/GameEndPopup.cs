using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Coffee.UIExtensions;

public class GameEndPopup : MonoBehaviour
{
    // Input Data
    [SerializeField] private Text scoreText;
    [SerializeField] private Text bestText;

    [SerializeField] private UIParticle fanfare;
    private int _score;
    private int Score
    {
        get => _score;
        set
        {
            _score = value;
            scoreText.text = value.ToString();
        }
    }


    private int _best;

    private int Best
    {
        get => _best;
        set
        {
            _best = value;
            bestText.text = value.ToString();
        }
    }

    public void Init(int score)
    {
        int best = PlayerPrefs.GetInt("Best", 0);
        bool isBest = false;

        fanfare.Stop();

        Score = 0;
        DOTween.To(() => Score, x => Score = x, score, 0.5f);

        if(best < score)
        {
            best = score;
            PlayerPrefs.SetInt("Best", score);
            isBest = true;
        }

        Best = 0;
        DOTween.To(() => Best, x => Best = x, best, 0.5f).SetDelay(0.5f).SetDelay(0.5f).OnComplete(() => PlayFanfareEffect(isBest));

    }

    public void OnPressedRestat()
    {
        GameManager.Instance.RestartGame();
    }

    public void OnPressedHome()
    {
        GameManager.Instance.Release();
        Main.Instance.ChangeScene(true);
    }

    private void PlayFanfareEffect(bool isBest)
    {
        if (isBest) fanfare.Play();
    }
}

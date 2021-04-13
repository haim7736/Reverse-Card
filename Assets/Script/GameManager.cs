using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Input Data
    public float maxPlayTime;

    public Text playTimeText;

    public CardManager cardManager;
    public GameEndPopup gameEndPopup;
    public GameObject pusePopup;

    // Local Data
    private bool _isGame;

    private float _playTime;
    private float playTime
    {
        get => _playTime;
        set
        {
            _playTime = value;
            playTimeText.text = Math.Ceiling(_playTime).ToString();
        }
    }

    public static GameManager Instance;
    
    public void PreInit()
    {
        Instance = this;

        cardManager.PreInit();

    }

    public void Release()
    {
        cardManager.Relase();
    }

    public void Init()
    {
        _isGame = true;
        playTime = maxPlayTime;
        gameEndPopup.gameObject.SetActive(false);
        SetPause(false);

        cardManager.Init();

    }

    void Update()
    {
        if (!_isGame) return;

        playTime -= Time.deltaTime;

        if (playTime > 0) return;

        playTime = 0;
        OnGameEnd();
    }

    public void OnGameEnd()
    {
        _isGame = false;

        gameEndPopup.gameObject.SetActive(true);
        gameEndPopup.Init((int)Math.Ceiling(playTime));

    }

    public void RestartGame()
    {
        Release();
        Init();
    }

    public void SetPause(bool isOn)
    {
        Time.timeScale = isOn ? 0 : 1;

        pusePopup.SetActive(isOn);
    }

}

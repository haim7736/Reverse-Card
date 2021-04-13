using Coffee.UIExtensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    // Input Data
    [SerializeField] private GameObject title;
    [SerializeField] private GameObject inGame;

    [SerializeField] private GameManager gameManager;

    [SerializeField] private Transform touchParticleParent;

    // Prefab Object
    [SerializeField] private UIParticle touchParticle;

    // Local Data
    public static Main Instance;

    void Start()
    {
        Instance = this;

        gameManager.PreInit();

        ChangeScene(true);
    }

    public void ChangeScene(bool isTitle)
    {
        title.SetActive(isTitle);
        inGame.SetActive(isTitle == false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(touchParticle, touchParticleParent).transform.position = Input.mousePosition;
        }
    }
}

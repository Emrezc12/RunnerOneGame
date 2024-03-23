using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject gameoverPanel;
    public static bool gameover;
    public static bool isGameStarted;
    void Start()
    {
        gameover = false;
        isGameStarted = false;
    }


    void Update()
    {
        if (gameover)
        {
            Time.timeScale = 0;
            gameoverPanel.SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public float timerAmount = 20f;
    public float gameTimerStartTime;
    public float timeRemaning;

    public UIController uiController;

    public GameObject GameGroup;
    public GameGroup gameGroup;

    public int redScore = 0;
    public int blueScore = 0;
    public enum GameState
    {
        Default,
        GameStart,
        Playing,
        RedWin,
        draw,
        BlueWin
    }

    public TextMeshProUGUI Timer;

    public GameState State { get; private set; } = GameState.GameStart;

    private void Awake()
    {
        gameGroup = GameGroup.GetComponent<GameGroup>();
    }
    void Start()
    {
        uiController.ShowGameStart();
    }

    // Update is called once per frame
    void Update()
    {
        TimeRemaining();

        Timer.text = timeRemaning.ToString("00:00");
        if (State==GameState.Playing)
        {
            redScore = gameGroup.redScore;
            blueScore = gameGroup.blueScore;
            Debug.Log(redScore + "" + blueScore);
            if (timeRemaning <= 0f)
            {
                EndGame();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }

    }

    void StartGame()
    {
        uiController.Clean();
        State = GameState.Playing;
        gameTimerStartTime = Time.time;
        redScore = 0;
        blueScore = 0;
        gameGroup.ResetGame();
    }

    public void EndGame()
    {
        Debug.Log(redScore + "" + blueScore);
        if (redScore>blueScore)
        {
            State = GameState.RedWin;
            uiController.ShowRedWin();
        }
        else if(redScore<blueScore)
        {
            State = GameState.BlueWin;
            uiController.ShowBlueWin();
        }
        else
        {
            State = GameState.draw;
            uiController.ShowDraw();
        }
    }
    public void TimeRemaining()
    {
            if (State == GameState.Playing)
            {
            timeRemaning = timerAmount - (Time.time - gameTimerStartTime);
            }
            else
            {
            timeRemaning= 0f;
            }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum GameState {Menu, InGame, Result}
public class GameManager : MonoBehaviour
{
    public delegate void GameEvent();
    public GameEvent event_BackToMenu;
    GameState gameState = GameState.Menu;

    float gameTimer = 10;
    public float gameTimerTotal = 5;

    float cookieTimer = 0;
    public float cookieTimerTotal = 1f;

    
    public int score;

    [SerializeField]
    RectTransform cookie;

    [SerializeField]
    GameObject[] pages;

    [SerializeField]
    TextMeshProUGUI tX_Score;
    [SerializeField]
    TextMeshProUGUI tX_TimeLeft;


    // Update is called once per frame
    void Update()
    {
        if(gameState == GameState.InGame)
		{
            tX_TimeLeft.text = "Time Left: " + Mathf.Round(gameTimer);
            if (gameTimer <= 0)
			{
                gameTimer = 0;
                GameOver();
            }
            else
			{
                gameTimer -= Time.deltaTime;

                if (cookieTimer >= cookieTimerTotal)
                {
                    CookieReset();
                }
                else
                {
                    cookieTimer += Time.deltaTime;
                }
            }
		}
    }

    public void GameStart()
    {
        gameState = GameState.InGame;
        score = 0;
        gameTimer = gameTimerTotal;


        tX_Score.text = "Score: " + score;
        tX_TimeLeft.text = "Time Left: " + Mathf.Round(gameTimer);

        pages[0].SetActive(false);
        pages[1].SetActive(true);
        pages[2].SetActive(false);
    }

    void GameOver()
    {
        gameState = GameState.Result;

        pages[0].SetActive(false);
        pages[1].SetActive(true);
        pages[2].SetActive(true);
    }

    public void BackToMenu()
    {
        if (gameState != GameState.Menu)
        {
            gameState = GameState.Menu;
            Invoke("WaitForEndingAnimation", 1.5f);
        }
    }

    void WaitForEndingAnimation()
    {
        //Wait for ending animation

        pages[0].SetActive(true);
        pages[1].SetActive(false);
        pages[2].SetActive(false);

        event_BackToMenu?.Invoke();
    }

    public void CookieClicked()
	{
        score++;
        CookieReset();

        tX_Score.text = "Score: " + score;
    }

    void CookieReset()
	{
        cookieTimer = 0;
        cookie.anchoredPosition = new Vector2(Random.Range(200, 1800) - Screen.width / 2, Random.Range(100, 800) - Screen.height / 2);
    }
}

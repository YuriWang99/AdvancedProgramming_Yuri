using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerShanzhai : MonoBehaviour
{
    // Start is called before the first frame update
    [Tooltip("Game ends when an agent collects this much nectar")]
    public float maxNectar = 10f;

    [Tooltip("Game ends after this many seconds have elapsed")]
    public float timerAmount = 60f;

    [Tooltip("The UI Controller")]
    public UIControllerShanzhai uiControllerShanzhai;

    [Tooltip("The player hummingbird")]
    public HummingbirdAgent player;

    [Tooltip("The ML-Agent opponent hummingbird")]
    public HummingbirdAgent opponent;

    [Tooltip("The flower area")]
    public FlowerManager flowerManager;

    [Tooltip("The main camera for the scene")]
    public Camera mainCamera;

    private float gameTimerStartTime;

    public enum GameState
    {
        Default, MainMenu, Preparing, Playing, Gameover
    }
    public GameState State { get; private set; }=GameState.Default;

    public float TimeRemaining
    {
        get
        {
            if (State == GameState.Playing)
            {
                float timeRemaining = timerAmount - (Time.time-gameTimerStartTime);
                return Mathf.Max(0f,timeRemaining);
            }
            else
            {
                return 0f;
            }
        }
    }
    void Start()
    {
        uiControllerShanzhai.OnButtonClicked += ButtonClicked;

        MainMenu();
    }
    private void OnDestroy()
    {
        uiControllerShanzhai.OnButtonClicked -= ButtonClicked;
    }

    public void ButtonClicked()
    {
        if (State == GameState.Gameover)
        {
            MainMenu();
        }
        else if (State == GameState.MainMenu)
        {
            StartCoroutine(StartGame());

        }
        else
        {
            Debug.LogWarning("Button clicked in unexpected state: " + State.ToString());
        }
    }
    void MainMenu()
    {
        State = GameState.MainMenu;
        uiControllerShanzhai.ShowBanner("");
        uiControllerShanzhai.ShowButton("Play");
        uiControllerShanzhai.HideAIScreen();
        mainCamera.gameObject.SetActive(true);
        player.agentCamera.gameObject.SetActive(false);
        opponent.agentCamera.gameObject.SetActive(false);

        flowerManager.ResetFlowers();

        player.OnEpisodeBegin();
        opponent.OnEpisodeBegin();

        player.FreezeAgent();
        opponent.FreezeAgent();
    }
    IEnumerator StartGame()
    {
        State = GameState.Preparing;

        uiControllerShanzhai.ShowBanner("");
        uiControllerShanzhai.HideButton();
        mainCamera.gameObject.SetActive(false);
        player.agentCamera.gameObject.SetActive(true);
        opponent.agentCamera.gameObject.SetActive(true);
        uiControllerShanzhai.ShowAIScreen();

        uiControllerShanzhai.ShowBanner("3");
        yield return new WaitForSeconds(1f);
        uiControllerShanzhai.ShowBanner("2");
        yield return new WaitForSeconds(1f);
        uiControllerShanzhai.ShowBanner("1");
        yield return new WaitForSeconds(1f);
        uiControllerShanzhai.ShowBanner("Go!");
        yield return new WaitForSeconds(1f);
        uiControllerShanzhai.ShowBanner("");



        State = GameState.Playing;

        gameTimerStartTime = Time.time;

        player.UnfreezeAgent();
        opponent.UnfreezeAgent();
    }

    void EndGame()
    {
        State = GameState.Gameover;
        uiControllerShanzhai.HideAIScreen();
        player.FreezeAgent();
        opponent.FreezeAgent();

        if (player.NectarObtained >= opponent.NectarObtained)
        {
            uiControllerShanzhai.ShowBanner("You win!");
        }
        else
        {
            uiControllerShanzhai.ShowBanner("AI wins!");
        }

        uiControllerShanzhai.ShowButton("Main Menu");
    }

    // Update is called once per frame
    void Update()
    {
        if (State == GameState.Playing)
        {
            // Check to see if time has run out or either agent got the max nectar amount
            if (TimeRemaining <= 0f ||
                player.NectarObtained >= maxNectar ||
                opponent.NectarObtained >= maxNectar)
            {
                EndGame();
            }

            // Update the timer and nectar progress bars
            uiControllerShanzhai.SetTimer(TimeRemaining);
            uiControllerShanzhai.SetPlayerNectar(player.NectarObtained / maxNectar);
            uiControllerShanzhai.SetOpponentNectar(opponent.NectarObtained / maxNectar);
        }
        else if (State == GameState.Preparing || State == GameState.Gameover)
        {
            // Update the timer
            uiControllerShanzhai.SetTimer(TimeRemaining);
        }
        else
        {
            // Hide the timer
            uiControllerShanzhai.SetTimer(-1f);

            // Update the progress bars
            uiControllerShanzhai.SetPlayerNectar(0f);
            uiControllerShanzhai.SetOpponentNectar(0f);
        }
    }
}

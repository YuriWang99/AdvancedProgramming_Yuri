using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject AIPrefab;
    public GameObject Cube;

    [Header("UI")]
    public GameObject GameStart;
    public GameObject GameProcess;
    public GameObject GameOver;

    [Header("Team Property")]
    public TextMeshProUGUI Score_RedTeam;

    public TextMeshProUGUI Score_BlueTeam;

    [Header("Timer")]
    public TextMeshProUGUI Timer;
    public float Total_Time;
    public float round_Time;
    public float cur_Time;
    public TextMeshProUGUI WinMessage;
    [Header("AIs")]
    public int AI_Nums;
    public Material M_red;
    public Material M_blue;
    public Transform StartPosition;
    public float Field;
    public GameObject player;

    public float Speed_AI;
    [Header("Cube")]
    public int Cube_Num;
    [Header("FSM")]
    public FSM<LevelManager> _fsm;

    [Header("FSM")]
    public int TotalWave;
    public int curWave;
    void Awake()
    {
        ServiceManager.levelManager = this;
        ServiceManager.Initlization();
        ServiceManager.event_Manager.Register<Event_OnGameStart>(OnGameStart);
        ServiceManager.event_Manager.Register<Event_OnTimeUp>(OnGameEnd);
    }

    void Start()
    {
        _fsm.TransitionTo<State_GameStart>();
    }
    private void Update()
    {
        _fsm.Update();
    }

    class State_GameStart : FSM<LevelManager>.State
    {
        public override void OnEnter()
        {
            base.OnEnter();
            Context.GameStart.SetActive(true);
        }

        public override void Update()
        {
            base.Update();
            if (Input.GetKeyDown(KeyCode.S))
            {
                ServiceManager.event_Manager.Fire(new Event_OnGameStart());
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            Context.GameStart.SetActive(false);
        }
    }

    class State_GameProcess : FSM<LevelManager>.State
    {
        public override void OnEnter()
        {
            base.OnEnter();
            Context.cur_Time = 0;
            Context.curWave = 0;
            ServiceManager.levelManager.Score_RedTeam.text = "Red: " + 0;
            ServiceManager.levelManager.Score_BlueTeam.text = "Blue: " + 0;
            ServiceManager.AI_Lcm.Creation();
            ServiceManager.Score_ManagerInGame.Creation();
            Context.GameProcess.SetActive(true);
            Context.player.transform.position = Context.StartPosition.position;
        }

        public override void Update()
        {
            base.Update();

            ServiceManager.Cube_Lcm.Updating();
            ServiceManager.AI_Lcm.Updating();
            Context.cur_Time += Time.deltaTime;
            Context.Timer.text = "Time Left: " + Mathf.Floor(Context.Total_Time - Context.cur_Time);
            if (Context.cur_Time >= Context.Total_Time)
            {
                int _blueTeamScore = ServiceManager.Score_ManagerInGame.Score_Blue;
                int _redTeamScore = ServiceManager.Score_ManagerInGame.Score_Red;

                ServiceManager.event_Manager.Fire(new Event_OnTimeUp(_blueTeamScore, _redTeamScore));
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            ServiceManager.AI_Lcm.Destruction();
            ServiceManager.Score_ManagerInGame.Destruction();
            ServiceManager.Cube_Lcm.Destruction();
            Context.GameProcess.SetActive(false);
        }
    }
    class State_GameEnd : FSM<LevelManager>.State
    {
        public override void OnEnter()
        {

            base.OnEnter();
            Context.GameOver.SetActive(true);
        }

        public override void Update()
        {
            base.Update();
            if (Input.GetKeyDown(KeyCode.S))
            {
                ServiceManager.event_Manager.Fire(new Event_OnGameStart());
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            Context.GameOver.SetActive(false);
        }
    }

    public void OnGameStart(AGPEvent e)
    {
        ServiceManager.levelManager._fsm.TransitionTo<State_GameProcess>();
    }
    public void OnGameEnd(AGPEvent e)
    {
        ServiceManager.levelManager._fsm.TransitionTo<State_GameEnd>();
    }
}

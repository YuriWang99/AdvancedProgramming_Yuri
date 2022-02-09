using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceManager
{
    public static AI_LCM AI_Lcm;
    public static Event_Manager event_Manager;
    public static PlayerController PlayerControllerInGame;
    public static LevelManager levelManager;
    public static Cube_LCM Cube_Lcm;
    public static Score_Manager Score_ManagerInGame;
    public static void Initlization()
    {
        AI_Lcm = new AI_LCM();
        Cube_Lcm = new Cube_LCM();
        event_Manager = new Event_Manager();
        Score_ManagerInGame = new Score_Manager();

        levelManager._fsm = new FSM<LevelManager>(levelManager);

    }
}

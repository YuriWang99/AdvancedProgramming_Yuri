using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score_Manager
{
    public int Score_Red,Score_Blue;

    public void Creation()
    {
        Score_Red = 0;
        Score_Blue = 0;
        ServiceManager.event_Manager.Register<Event_OnScore>(OnScore);
        ServiceManager.event_Manager.Register<Event_OnTimeUp>(OnTimeUp);
    }
    public void Destruction()
    {
        ServiceManager.event_Manager.Unregister<Event_OnScore>(OnScore);
        ServiceManager.event_Manager.Unregister<Event_OnTimeUp>(OnTimeUp);
    }
    private void OnScore(AGPEvent e)
    {
        var scoredTeam = (Event_OnScore)e;
        Debug.Log("scored!");
        if (scoredTeam.teamIDScored == 0)
        {
            Score_Red++;
            ServiceManager.levelManager.Score_RedTeam.text = "Red: " + Score_Red;
        }
        else
        {
            Score_Blue++;
            ServiceManager.levelManager.Score_BlueTeam.text = "Blue: " + Score_Blue;
        }

    }
    private void OnTimeUp(AGPEvent e)
    {
        if (Score_Blue > Score_Red)
        {
            ServiceManager.levelManager.WinMessage.text = "Team blue win!";
        }
        else if (Score_Blue < Score_Red)
        {
            ServiceManager.levelManager.WinMessage.text = "Team red win!";
        }
        else
        {
            ServiceManager.levelManager.WinMessage.text = "Draw";
        }

    }

}

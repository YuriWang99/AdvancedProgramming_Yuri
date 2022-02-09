using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_LCM
{
    public  List<GameObject> AIs = new List<GameObject>();
    // Start is called before the first frame update

    private void Awake()
    {
        ServiceManager.AI_Lcm = this;
    }

    public void Creation()
    {
        ServiceManager.event_Manager.Register<Event_OnScore>(AI_OnScoer);
        ServiceManager.event_Manager.Register<Event_OnGenerateCube>(AI_OnScoer);
        CreateAI();
    }
    public void Updating()
    {
        foreach (var AI in AIs)
        {
            Tracking(AI);
        }
    }
    public void Destruction()
    {
        ServiceManager.event_Manager.Unregister<Event_OnScore>(AI_OnScoer);
        ServiceManager.event_Manager.Unregister<Event_OnGenerateCube>(AI_OnScoer);
        foreach (var AI in AIs)
        {
            Object.Destroy(AI);
        }
        AIs.Clear();
    }
    void Tracking(GameObject AI)
    {
        if(AI.GetComponent<AI_Property>().curTarget!=null)
        {
            var newPos = AI.GetComponent<AI_Property>().curTarget.transform.position;
            AI.GetComponent<Rigidbody>().velocity =
                (newPos - AI.transform.position).normalized * ServiceManager.levelManager.Speed_AI;
        }
    }
    public void CreateAI()
    {
        for (int i = 0; i < ServiceManager.levelManager.AI_Nums * 2; i++)
        {
            GameObject thisAI = UnityEngine.Object.Instantiate(ServiceManager.levelManager.AIPrefab,
            new Vector3(ServiceManager.levelManager.StartPosition.position.x+Random.Range(-ServiceManager.levelManager.Field, ServiceManager.levelManager.Field),
            ServiceManager.levelManager.StartPosition.position.y,
            ServiceManager.levelManager.StartPosition.position.z + Random.Range(-ServiceManager.levelManager.Field, ServiceManager.levelManager.Field)),
            Quaternion.identity);
            if (i > ServiceManager.levelManager.AI_Nums)
            {
                thisAI.GetComponent<Renderer>().material = ServiceManager.levelManager.M_red;
                thisAI.GetComponent<AI_Property>().teamTag = 0;
            }
            else
            {
                thisAI.GetComponent<Renderer>().material = ServiceManager.levelManager.M_blue;
                thisAI.GetComponent<AI_Property>().teamTag = 1;
            }
            AIs.Add(thisAI);
        }
    }
    public void minDistance(GameObject AI)
    {

        if (ServiceManager.Cube_Lcm.Cubes.Count == 0)
        {
            AI.GetComponent<AI_Property>().curTarget = null;
        }
        else
        {
            GameObject curTarget = ServiceManager.Cube_Lcm.Cubes[0];
            float minDist = float.MaxValue;
            for (int i = 0; i < ServiceManager.Cube_Lcm.Cubes.Count; i++)
            {
                float temp = Vector3.Distance(ServiceManager.Cube_Lcm.Cubes[i].transform.position,
                    AI.transform.position);
                if (temp < minDist)
                {
                    curTarget = ServiceManager.Cube_Lcm.Cubes[i];
                    minDist = temp;
                }
            }
            AI.GetComponent<AI_Property>().curTarget = curTarget;
        }
/*        float temp;
        if (ServiceLocator.cube_Scatter.Cubes.Count > 0)
        {
            minDist = Vector3.Distance(ServiceLocator.cube_Scatter.Cubes[curIndex].transform.position, players[0].transform.position);
            for (int i = 0; i < ServiceLocator.cube_Scatter.Cubes.Count; i++)
            {
                if (ServiceLocator.cube_Scatter.Cubes[i].activeSelf)
                {
                    temp = Vector3.Distance(ServiceLocator.cube_Scatter.Cubes[i].transform.position, players[0].transform.position);
                    if (temp < minDist)
                    {
                        minDist = temp;
                        curIndex = i;
                    }
                }
            }
            curTarget = ServiceLocator.cube_Scatter.Cubes[curIndex];
        }*/
    }
    public void AI_OnScoer(AGPEvent e)
    {
        foreach (var AI in AIs)
        {
            minDistance(AI);
        }
    }
}

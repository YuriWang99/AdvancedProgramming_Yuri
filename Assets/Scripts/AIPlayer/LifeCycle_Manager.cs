using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LifeCycle_Manager
{
    // Start is called before the first frame update
    
    public List<GameObject> players = new List<GameObject>();
    //public List<GameObject> targets = new List<GameObject>();
    float playerSpeed = 100f;
    Vector3 moveDirection;
    public GameObject curTarget;
    int curIndex=0;
    float minDist = 0;


    public void Creation(GameObject startPoint,GameObject newAI)
    {
        Debug.Log("Creation");
        players.Add(newAI);
        curTarget = ServiceLocator.cube_Scatter.Cubes[curIndex];
        minDistance();
        moveDirection = curTarget.transform.position.normalized - players[0].transform.position.normalized;
        moveDirection.y = 0;
    }
    public void Updating()
    {
        if (ServiceLocator.cube_Scatter.Cubes!=null)
        {
            Debug.Log("updating");
            //Updating target
            ServiceLocator.cube_Scatter.Destroy(curIndex);
            curIndex = 0;

            /*        if (curIndex < ServiceLocator.cube_Scatter.Cubes.Count)
                    {
                        minDistance();
                    }
                    else
                    {
                        Destruction();
                    }*/
            minDistance();
            moveDirection = (curTarget.transform.position - players[0].transform.position).normalized;
            Debug.Log(moveDirection);
            moveDirection.y = 0;
        }


    }
    public void Destruction()
    {
        SceneManager.LoadScene(0);
        Debug.Log("Destruction");
    }

    public void Tracking()
    {
        //store the current target pos
        Debug.Log(moveDirection);
        if(players.Count>0&&curIndex < ServiceLocator.cube_Scatter.Cubes.Count)
        {
            players[0].transform.position += moveDirection * playerSpeed * Time.deltaTime;
            if (Vector3.Distance(curTarget.transform.position, players[0].transform.position) < 10)
            {
                Updating();
            }
            //Debug.Log(Vector3.Distance(curTarget.transform.position, players[0].transform.position));
        }
        else if(ServiceLocator.cube_Scatter.Cubes.Count==0)
        {

            Destruction();
        }
    }

    public void minDistance()
    {
        float temp;
        if(ServiceLocator.cube_Scatter.Cubes.Count>0)
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
        }    

    }
}

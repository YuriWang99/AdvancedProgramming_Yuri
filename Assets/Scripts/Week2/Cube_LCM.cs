using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube_LCM
{
    public List<GameObject> Cubes = new List<GameObject>();
    private void Awake()
    {
        ServiceManager.Cube_Lcm = this;
    }
    public void Creation()
    {

    }

    public void Updating()
    {
        if (ServiceManager.levelManager.curWave == 0)
        {
            ServiceManager.levelManager.curWave++;
            CreatCubes();
        }
        //update wave
        if (ServiceManager.levelManager.curWave * ServiceManager.levelManager.round_Time < ServiceManager.levelManager.cur_Time)
        {
            ServiceManager.levelManager.curWave++;
            CreatCubes();
        }
    }
    public void Destruction()
    {
        if (Cubes.Count == 0)
        {
            return;
        }
        else
        {
            foreach (var cube in Cubes)
            {
                Object.Destroy(cube);
            }
            Cubes.Clear();
        }
    }
    public void CreatCubes()
    {
        // Set some positions
        Vector3 playerPos = ServiceManager.levelManager.StartPosition.position;

        for (int i = 0; i < ServiceManager.levelManager.Cube_Num; i++)
        {
            Cubes.Add(
                Object.Instantiate(ServiceManager.levelManager.Cube,
                new Vector3(
                    playerPos.x + Random.Range(-ServiceManager.levelManager.Field*5, ServiceManager.levelManager.Field * 5),
                playerPos.y, 
                playerPos.z + Random.Range(-ServiceManager.levelManager.Field * 5, ServiceManager.levelManager.Field * 5)),
                Quaternion.identity)
                );
        }
        ServiceManager.event_Manager.Fire(new Event_OnGenerateCube());
    }

/*    public void Destroy(int index)
    {
        //Cubes[index].SetActive(false);
        Destroy(Cubes[index]);
        Cubes.RemoveAt(index);
    }*/
}

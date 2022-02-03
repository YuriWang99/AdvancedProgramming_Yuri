using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube_Scatter : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> Cubes = new List<GameObject>();

    public GameObject Cube_prefab;
    private void Awake()
    {
        ServiceLocator.cube_Scatter = this;
    }
    public void CreatCubes(GameObject startPoint, GameObject Cube_prefab,int cube_nums,float scatter_field)
    {
        // Set some positions
        Vector3 playerPos = startPoint.transform.position;

        for (int i= 0;i<cube_nums;i++)
        {
            Cubes.Add(
                Instantiate(Cube_prefab,
                new Vector3(playerPos.x + Random.Range(-scatter_field, scatter_field), playerPos.y, playerPos.z + Random.Range(-scatter_field, scatter_field)), 
                Quaternion.identity)
                );
        }
    }

    public void Destroy(int index)
    {
        //Cubes[index].SetActive(false);
        Destroy(Cubes[index]);
        Cubes.RemoveAt(index);
    }

}

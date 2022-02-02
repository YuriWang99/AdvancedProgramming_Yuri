using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Service Data")]
    public GameObject Cube_prefab;
    public int Cube_nums;
    public float Scatter_field;
    [Header("Player property")]
    public GameObject player_prefab;
    public GameObject startPoint;
    public GameObject Button;
    //public List<GameObject> players = new List<GameObject>();
    //public List<GameObject> targets = new List<GameObject>();
    public float Player_Speed;
    private void Awake()
    {
        ServiceLocator.Initlization();
    }

    void Start()
    {
        ServiceLocator.cube_Scatter.CreatCubes(startPoint, Cube_prefab, Cube_nums, Scatter_field);
        //ServiceLocator.lifecycle_Manager.Creation();
    }

    // Update is called once per frame
    void Update()
    {
        ServiceLocator.lifecycle_Manager.Tracking();
    }
    public void cerateAI()
    {
        GameObject newAI = Instantiate(player_prefab, startPoint.transform);
        Debug.Log(ServiceLocator.lifecycle_Manager.curTarget);
        ServiceLocator.lifecycle_Manager.Creation(startPoint, newAI);
        Button.SetActive(false);
    }


}

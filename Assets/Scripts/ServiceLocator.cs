using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator: MonoBehaviour
{
    public static LifeCycle_Manager lifecycle_Manager;
    public static Cube_Scatter cube_Scatter;
    public static void Initlization()
    {
        //cube_Scatter = new Cube_Scatter();
        lifecycle_Manager = new LifeCycle_Manager();
    }
    private void Update()
    {
    }
}

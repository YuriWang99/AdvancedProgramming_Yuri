using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCollectable : MonoBehaviour
{
    private void OnCollisionEnter(Collision Object)
    {
        if (Object.gameObject.CompareTag("Player"))
        {
            ServiceManager.Cube_Lcm.Cubes.Remove(this.gameObject);
            int teamNumber = Object.gameObject.GetComponent<AI_Property>().teamTag;
            ServiceManager.event_Manager.Fire(new Event_OnScore(teamNumber));
            Destroy(this.gameObject);
        }
    }
}

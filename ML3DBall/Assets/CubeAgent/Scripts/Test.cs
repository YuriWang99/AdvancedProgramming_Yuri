using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject Top;
    public Plane plane;
    public float distance;
    void Start()
    {
        
        //distance = plane.GetDistanceToPoint(Cube.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        #region
        /*        plane = new Plane(transform.up, transform.position + 1f * transform.up);
                distance = plane.GetDistanceToPoint(Top.transform.position);
                //Debug.Log(Mathf.Sqrt(3) / 2);
                Debug.Log(distance);
                Debug.DrawRay(transform.position+1f* transform.up, transform.up, Color.red,10f);
                //Debug.Log(Vector3.Distance(transform.position,Cube.transform.position));

                if(distance < Mathf.Sqrt(2) / 2)
                {
                    Top.transform.rotation = Quaternion.Euler(38, 0, 45);
                    Top.transform.position = new Vector3(Random.Range(-.2f, .2f), 4f, Random.Range(-.2f, .2f))
                        + gameObject.transform.position;
                    Top.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
                    Top.GetComponent<Rigidbody>().angularVelocity = new Vector3(0f, 0f, 0f);
                }*/
        #endregion
        Debug.Log(Top.transform.position.y - gameObject.transform.position.y);
    }
}

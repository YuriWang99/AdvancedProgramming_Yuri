using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class CubeAgent2 : Agent
{
    public GameObject Top;
    Rigidbody rb_Top;

    public Plane plane;
    public float distance;
    public float FailDist,PrefectDist;
    public GameObject Space;

    public override void Initialize()
    {
        rb_Top = Top.GetComponent<Rigidbody>();
        SetResetParameters();
        PrefectDist = Mathf.Sqrt(3) / 2;
        FailDist = 1;
    }
    void SetResetParameters()
    {
        SetTop();
    }
    void SetTop()
    {
        //reset the ball scale to 1
        var scale = 1f;
        rb_Top.mass = scale;
        Top.transform.localScale = new Vector3(scale, scale, scale);
    }

    //each game cycle it executes once
    public override void OnEpisodeBegin()
    {

        //reset the rotation of cube
        gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
        //add dynamic random rotation to the cube
        gameObject.transform.Rotate(new Vector3(1, 0, 0), Random.Range(-5f, 5f));
        gameObject.transform.Rotate(new Vector3(0, 0, 1), Random.Range(-5f, 5f));

        plane = new Plane(transform.up, transform.position + 1f * transform.up);
        //distance = plane.GetDistanceToPoint(Top.transform.position);
        //Debug.DrawRay(transform.position + 1f * transform.up, transform.up, Color.red, 10f);
        
        
        //reset the velocity of test Top
        Top.transform.rotation = Quaternion.Euler(0, 0, 0);
        Top.transform.position = new Vector3(Random.Range(-0.5f,0.5f), 3f, Random.Range(-0.5f, 0.5f)) 
            + transform.position;
        rb_Top.velocity = new Vector3(0f, 0f, 0f);
        rb_Top.angularVelocity = new Vector3(0f, 0f, 0f);

        SetResetParameters();
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        //collect the data from observation

        //rotation x of the cube
        //float = 1 space size
        sensor.AddObservation(gameObject.transform.rotation.x);

        //rotation z of the cube
        //float = 1 space size
        sensor.AddObservation(gameObject.transform.rotation.z);

        //relative position between Ball&Cube
        //Vector3(x,y,z) = 3 space size
        //float = 1 space size
        sensor.AddObservation(plane.GetDistanceToPoint(Top.transform.position));

        //speed,direction of the ball
        //Vector3(x,y,z) = 3 space size
        sensor.AddObservation(rb_Top.velocity);



    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        //two variable store the value for action
        //rotate the cube around axis Z
        var actionZ = 2f * Mathf.Clamp(actions.ContinuousActions[0], -1f, 1f);
        //rotate the cube around axis X
        var actionX = 2f * Mathf.Clamp(actions.ContinuousActions[1], -1f, 1f);


/*        if((gameObject.transform.rotation.z < 0.25f && actionZ >0f)||(gameObject.transform.rotation.z>-0.25f&&actionZ<0f))
        {
            gameObject.transform.Rotate(new Vector3(0, 0, 1), actionZ);
        }
        if ((gameObject.transform.rotation.x < 0.25f && actionX > 0f) || (gameObject.transform.rotation.x > -0.25f && actionX < 0f))
        {
            gameObject.transform.Rotate(new Vector3(1, 0, 0), actionX);
        }*/

        //Ball fall down too far, or ball too far from the cube x z
        if (plane.GetDistanceToPoint(Top.transform.position) < FailDist|| 
            Mathf.Abs(Top.transform.position.x -transform.position.x)>5f||
            Mathf.Abs(Top.transform.position.z - transform.position.z) > 5f)
        {
            SetReward(-1f);
            EndEpisode();
        }
        else
        {
            SetReward(.5f);
        }

    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {

        var continousActionOut = actionsOut.ContinuousActions;
        continousActionOut[0] = Input.GetAxis("Horizontal");
        continousActionOut[1] = Input.GetAxis("Vertical");
    }
}

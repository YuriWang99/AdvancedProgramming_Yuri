using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class CubeAgent : Agent
{
    public GameObject Ball;
    Rigidbody rb_Ball;

    public override void Initialize()
    {
        rb_Ball = Ball.GetComponent<Rigidbody>();
        SetResetParameters();
    }
    void SetResetParameters()
    {
        SetBall();
    }
    void SetBall()
    {
        //reset the ball scale to 1
        var scale = Random.Range(0, 2f);
        rb_Ball.mass = scale; ;
        Ball.transform.localScale = new Vector3(scale, scale, scale);
    }

    //each game cycle it executes once
    public override void OnEpisodeBegin()
    {
        //reset the rotation of cube
        gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
        //add dynamic random rotation to the cube
        gameObject.transform.Rotate(new Vector3(1, 0, 0), Random.Range(-10f, 10f));
        gameObject.transform.Rotate(new Vector3(0, 0, 1), Random.Range(-10f, 10f));

        //reset the velocity of test ball
        rb_Ball.velocity = new Vector3(0f, 0f, 0f);

        Ball.transform.position = new Vector3(Random.Range(-.2f, .2f), 4f, Random.Range(-.2f, .2f)) 
            + gameObject.transform.position;
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
        sensor.AddObservation(Ball.transform.position - gameObject.transform.position);

        //speed,direction of the ball
        //Vector3(x,y,z) = 3 space size
        sensor.AddObservation(rb_Ball.velocity);



    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        //two variable store the value for action
        //rotate the cube around axis Z
        var actionZ = 2f * Mathf.Clamp(actions.ContinuousActions[0], -1f, 1f);
        //rotate the cube around axis X
        var actionX = 2f * Mathf.Clamp(actions.ContinuousActions[1], -1f, 1f);


        if((gameObject.transform.rotation.z < 0.25f && actionZ >0f)||(gameObject.transform.rotation.z>-0.25f&&actionZ<0f))
        {
            gameObject.transform.Rotate(new Vector3(0, 0, 1), actionZ);
        }
        if ((gameObject.transform.rotation.x < 0.25f && actionX > 0f) || (gameObject.transform.rotation.x > -0.25f && actionX < 0f))
        {
            gameObject.transform.Rotate(new Vector3(1, 0, 0), actionX);
        }

        //Ball fall down too far, or ball too far from the cube x z
        if ((Ball.transform.position.y - gameObject.transform.position.y)<-rb_Ball.mass/2+1 || 
            Mathf.Abs(Ball.transform.position.x -transform.position.x)>5f||
            Mathf.Abs(Ball.transform.position.z - transform.position.z) > 5f)
        {
            SetReward(-1f);
            EndEpisode();
        }
        else
        {
            SetReward(.1f);
        }

    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {

        var continousActionOut = actionsOut.ContinuousActions;
        continousActionOut[0] = Input.GetAxis("Horizontal");
        continousActionOut[1] = Input.GetAxis("Vertical");
    }
}

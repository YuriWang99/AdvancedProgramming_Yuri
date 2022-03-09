using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using UnityEngine;

public class HummingbirdAgentInClass : Agent
{
    //force to apply to bird
    float moveForce = 2;
    //for rotation, pitch dir
    float pitchSpeed = 100f;
    //for rotation, yaw dir
    float yawSpeed = 100f;

    //Bird's mouth tip
    public Transform beakTip;
    //Agent's camera
    public Camera agentCamer;
    //For game mode trainingMode check
    public bool trainingMode;

    //Flower manager have all the data of flower
    FlowerManager flowerManager;
    //save the nearest flower
    Flower nearestFlower;
    //Bird rigidbody
    new Rigidbody rigidbody;

    //smoothere pitch change
    private float smoothPitchChange = 0f;
    //moothere pitch change
    private float smoothYawChange = 0f;

    private const float MaxPitchAngle = 80f;
    private const float beakTipRadius = .008f;
    //whether the agent is frozen
    bool frozen = false;
    public float NectarObtained { get; private set; }
    public override void Initialize()
    {
        rigidbody = GetComponent<Rigidbody>();
        flowerManager = GetComponent<FlowerManager>();

        if (!trainingMode) MaxStep = 0;
    }
    public override void OnEpisodeBegin()
    {
        if(trainingMode)
        {
            flowerManager.ResetFlowers();
        }

        NectarObtained = 0;
        //Zeor out velocities so that movement steps before a new episode begins
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;

        //Default to spawning in front a flower
        bool inFrontOfFlower = true;
        if(trainingMode)
        {
            //Spawn in front of flower 50% of the time during training
            inFrontOfFlower = UnityEngine.Random.value > .5f;
        }
        MoveToSafeRandomPosition(inFrontOfFlower);

        UpdateNearestFlower();
    }
    void MoveToSafeRandomPosition(bool inFrontOfFlower)
    {
        bool safePositionFound = false;
        int attemptsRemaining = 100;

        Vector3 potentialPosition = Vector3.zero;
        Quaternion potentialRotation = new Quaternion();

        //look until a safe position is found or out of attemps
        while (!safePositionFound&& attemptsRemaining > 0)
        {
            attemptsRemaining--;
            if(inFrontOfFlower)
            {
                //pick a random flower
                Flower randomFlower = flowerManager.Flowers[UnityEngine.Random.Range(0,flowerManager.Flowers.Count)];

                //Position 10 to 20 cm in front of the flower
                float distanceFromFlower = Random.Range(.1f,.2f);
                potentialPosition = randomFlower.transform .position + randomFlower.FlowerUpVector * distanceFromFlower;


                Vector3 toFlower = randomFlower.FlowerCenterPosition - potentialPosition;
                potentialRotation = Quaternion.LookRotation(toFlower, Vector3.up);

            }
            else
            {
                float height = Random.Range(1.2f, 2.5f);
                float radius = Random.Range(2f, 7f);

                //Pick random dir rotated around y axis
                Quaternion direction = Quaternion.Euler (0f,Random.Range(-180f,180f) , 0f);

                //Combine height, radius, direction
                potentialPosition = flowerManager.transform.position + Vector3.up * height + direction*Vector3.forward *radius ;

                float pitch = UnityEngine.Random.Range(-60f, 60f);
                float yaw = UnityEngine.Random.Range(-180f, 180f);
                potentialRotation = Quaternion.Euler(pitch, yaw, 0f);
            }

            //check if the agent is too close to anything
            Collider[] colliders = Physics.OverlapSphere(potentialPosition, .05f);

            safePositionFound = colliders.Length==0;
        }

        transform.position = potentialPosition;
        transform.rotation = potentialRotation;
    }
    private void UpdateNearestFlower()
    {
        foreach(Flower flower in flowerManager.Flowers)
        {
            if(nearestFlower ==null&&flower.HasNectar)
            {

            }
            else if(nearestFlower.HasNectar)
            {
                float distToFlower = Vector3.Distance(flower.transform.position, beakTip.position);
                float distToCurNearestFlower = Vector3.Distance (nearestFlower.transform .position, beakTip.position);  
                
                //if nearestFlower is empty of nector OR the flower we loop through is closer
                //use the new flower as the nearest flower
                if(distToFlower < distToCurNearestFlower||!nearestFlower.HasNectar)
                {
                    nearestFlower = flower;
                }
            }
        }

    }
    public void FreezeAgent()
    {

    }
    public void UnFreezeAgent()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        TriggerEnterOrStay(other);
    }
    private void OnTriggerStay(Collider other)
    {
        TriggerEnterOrStay(other);

    }

    void TriggerEnterOrStay(Collider collider)
    {
        if(collider.CompareTag("nectar"))
        {
            //Check if the closes collision point is close to the beak tip
            //Notice: a collision with anything but the beak tio should not count

            Vector3 closestPointToBeakTip = collider.ClosestPoint(beakTip.position);
            if (Vector3.Distance(beakTip.position, closestPointToBeakTip) < beakTipRadius)
            {
                Flower flower = flowerManager.GetFlowerFromNectar(collider);
                float nectarReceived = flower.Feed(.01f);

                NectarObtained += nectarReceived;

                if (trainingMode)
                {
                    //calculate reward for getting nectar
                    //if the bird actually facing the same dir to the flower bonus
                    float bonus = .02f * Mathf.Clamp01(Vector3.Dot(transform.forward.normalized, -nearestFlower.FlowerUpVector.normalized));
                    AddReward(.01f + bonus);
                }

                if (!flower.HasNectar)
                {
                    UpdateNearestFlower();
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (trainingMode && collision.gameObject.CompareTag("boundary"))
        {
            AddReward(-.5f);
        }
    }

    private void Update()
    {
        if (nearestFlower != null)
        {
            Debug.DrawLine(beakTip.position, nearestFlower.FlowerCenterPosition, Color.green);
        }

    }

    private void FixedUpdate()
    {
        if (nearestFlower != null && !nearestFlower.HasNectar)
        {
            UpdateNearestFlower();
        }
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        if (nearestFlower == null)
        {
            sensor.AddObservation(new float[10]);
            return;
        }

        //rotation of the bird
        //size 3
        sensor.AddObservation(transform.localRotation.normalized);

        Vector3 toFlower = nearestFlower.FlowerCenterPosition - beakTip.position;
        //size 3
        sensor.AddObservation(toFlower.normalized);

        //Dot return 1 or -1 check two vetor is in the same dir or not
        //a dot product beak tip in the front of the flower
        //size 1
        sensor.AddObservation(Vector3.Dot(toFlower.normalized, -nearestFlower.FlowerUpVector.normalized));
        //whether the beak is pointing toward the flower
        //size 1
        sensor.AddObservation(Vector3.Dot(beakTip.forward.normalized, -nearestFlower.FlowerUpVector.normalized));

        //Relative distance from the beak tip to the flower
        //size 1
        sensor.AddObservation(toFlower.magnitude / FlowerManager.AreaDiameter);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        if (frozen) return;

        Vector3 move = new Vector3(actions.ContinuousActions[0], actions.ContinuousActions[1], actions.ContinuousActions[2]);

        rigidbody.AddForce(move * moveForce);

        Vector3 rotationVector = transform.rotation.eulerAngles;

        float pitchChange = actions.ContinuousActions[3];
        float yawChange = actions.ContinuousActions[4];

        smoothPitchChange = Mathf.MoveTowards(smoothPitchChange, pitchChange, 2f * Time.fixedDeltaTime);
        smoothYawChange = Mathf.MoveTowards(smoothYawChange, yawChange, 2f * Time.fixedDeltaTime);

        float pitch = rotationVector.x + smoothPitchChange * Time.fixedDeltaTime * pitchSpeed;
        if (pitch > 180f) pitch -= 360f;
        pitch = Mathf.Clamp(pitch, -MaxPitchAngle, MaxPitchAngle);

        float yaw = rotationVector.y + smoothYawChange * Time.fixedDeltaTime * yawSpeed;

        transform.rotation = Quaternion.Euler(pitch, yaw, 0);
    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        Vector3 forward = Vector3.zero;
        Vector3 left = Vector3.zero;
        Vector3 up = Vector3.zero;

        float pitch = 0f;
        float yaw = 0f;

        if (Input.GetKey(KeyCode.W)) forward = transform.forward;
        else if (Input.GetKey(KeyCode.S)) forward = -transform.forward;

        if (Input.GetKey(KeyCode.A)) left = -transform.right;
        else if (Input.GetKey(KeyCode.D)) left = transform.right;

        if (Input.GetKey(KeyCode.E)) up = transform.up;
        else if (Input.GetKey(KeyCode.C)) up = -transform.up;

        if (Input.GetKey(KeyCode.UpArrow)) pitch = 1f;
        else if (Input.GetKey(KeyCode.DownArrow)) pitch = -1f;

        if (Input.GetKey(KeyCode.LeftArrow)) yaw = -1f;
        else if (Input.GetKey(KeyCode.RightArrow)) yaw = 1f;

        Vector3 combined = (forward + left + up).normalized;

        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = combined.x;
        continuousActionsOut[1] = combined.y;
        continuousActionsOut[2] = combined.z;
        continuousActionsOut[3] = pitch;
        continuousActionsOut[4] = yaw;
    }
}

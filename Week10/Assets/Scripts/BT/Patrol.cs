using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorTree;

public class Patrol : MonoBehaviour
{
    private BehaviorTree.Tree<Patrol> _tree;
    public NavMeshAgent Guard;
    public float WalkSpeed = 2f;
    //public float RunSpeed = 3f;
    //public float HungryBar = 10f;
    //public bool isEnemy;
    public  List<Transform> wayPoints = new List<Transform>();
    public GameObject cube;
    public Transform curDestination;

    public float Timer;
    public float startTime;
    public float curTime;
    public Vector3 moveDir;

    void Start()
    {
        RestTimer();
        Guard = GetComponent<NavMeshAgent>();

        curDestination = wayPoints[Random.Range(0, wayPoints.Count)];
        Move();


        var detectEnemy = new Tree<Patrol>
            (
            new Sequence<Patrol>
            (
                 new Condition<Patrol>(IsEnemy),
                 new Do<Patrol>(ChaseEnemy),
                 new Do<Patrol>(DestroyEnemy)

                )
            );
        var mouseDownTree = new Tree<Patrol>
        (
            new Sequence<Patrol>
            (
                new IsMouseDown(),
                new Do<Patrol>(GenerateCube)
            )
        );

        _tree = new Tree<Patrol>
         (
            new Sequence<Patrol>
           (
             new Condition<Patrol>(TimeIsUp),
             new Do<Patrol>(WalkAround)
            )
          );

        //Chase if enemy

    }

    // Update is called once per frame
    private void Update()
    {
        curTime += Time.deltaTime;
        _tree.Update(this);
        //IsEnemy();
        Debug.Log(Vector3.Distance(curDestination.position, this.transform.position));
    }

    #region CustomizeMethods
    public class IsMouseDown : BehaviorTree.Node<Patrol>
    {
        public override bool Update(Patrol context)
        {
            return Input.GetMouseButton(0);
        }
    }
    public bool GenerateCube(Patrol context)
    {
        
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 1;
        
        Debug.Log(pos);
        Instantiate(cube, pos, Quaternion.identity);
        return true;
    }

    private bool IsEnemy(Patrol context)
    {
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, 5f);
        if (hitColliders.Length > 0)
        {
            Guard.speed = WalkSpeed * 3;
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.gameObject.tag == "Enemy")
                {
                    Debug.Log(hitCollider.gameObject.name);
                    curDestination = hitCollider.transform;
                    break;
                }
            }
        }
        if(curDestination.gameObject.tag=="Enemy")
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    private bool TimeIsUp(Patrol context)
    {
        if(curTime-startTime > Timer)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void RestTimer()
    {
        Timer = Random.Range(1f, 3f);
        startTime = curTime;
        moveDir = new Vector3(Random.Range(0f,360f),0, Random.Range(0f, 360f));
    }

    private bool WalkAround(Patrol context)
    {

        if (Vector3.Distance(curDestination.position, this.transform.position) < 1.5f)
        {

            curDestination = wayPoints[Random.Range(0, wayPoints.Count)];
        }
        Guard.speed = WalkSpeed;
        Move();
        return true;
    }

    private bool ChaseEnemy(Patrol context)
    {
        Guard.speed = WalkSpeed*3;
        Move();
        return true;
    }
    private bool DestroyEnemy(Patrol context)
    {
        if(Vector3.Distance(curDestination.position, this.transform.position) < 1.5f&& curDestination.gameObject.tag=="Enemy")
        {
            Destroy(curDestination.gameObject);
            curDestination = wayPoints[Random.Range(0, wayPoints.Count)];
        }
        return true;
    }
    private bool PatrolMode(Patrol context)
    {

        if (Vector3.Distance(curDestination.position, this.transform.position)<1.5f)
        {

            curDestination = wayPoints[Random.Range(0, wayPoints.Count)];
        }
        Guard.speed = WalkSpeed;
        Move();
        return true;
    }

    private void Move()
    {
        Guard.SetDestination(curDestination.position);
    }


    #endregion
}
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
    public Transform curDestination;

    void Start()
    {
        Guard = GetComponent<NavMeshAgent>();

        curDestination = wayPoints[Random.Range(0, wayPoints.Count)];
        Move();


        var detectEnemy = new Tree<Patrol>
            (
            new Sequence<Patrol>
            (
                 new Condition<Patrol>(IsEnemy),
                 new Do<Patrol>(ChaseEnemy)

                )
            );
        var _tree = new Tree<Patrol>
         (
            new Selector<Patrol>
           (
             detectEnemy,
             new Do<Patrol>(PatrolMode)
            )
          );

        //Chase if enemy

    }

    // Update is called once per frame
    private void Update()
    {
        _tree.Update(this);
        //IsEnemy();
    }

    #region CustomizeMethods

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
            return true;
        }
        else
        {
            return false;
        }

    }
    private bool ChaseEnemy(Patrol context)
    {
        Move();
        return true;
    }
    private bool PatrolMode(Patrol context)
    {
        curDestination = wayPoints[Random.Range(0, wayPoints.Count)];
        float minDist = float.MaxValue;
        foreach(var point in wayPoints)
        {
            if(Vector3.Distance(point.position, this.transform.position) < minDist)
            {
                minDist = Vector3.Distance(point.position, this.transform.position);
                curDestination = point;
            }
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
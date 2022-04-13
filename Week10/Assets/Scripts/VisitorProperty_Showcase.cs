using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorTree;

public class VisitorProperty_Showcase : MonoBehaviour
{
	[SerializeField]
	LocalReference reference;
	[SerializeField]
	string referenceCameraName = "Main Camera";
	[SerializeField]
	CharacterSetDB characterSetDB;
	[SerializeField]
	DressingUp_Visitor dressingUp_Visitor;
	[SerializeField]
	//CharacterWayPointsDB waypointsDB;
	public List<Vector3> waypoints = new List<Vector3>();

	[SerializeField]
	Transform mainBody;
	[SerializeField]
	Animator anim;

	Transform mainCamera;

	NavMeshAgent nav;

	string currentDirection = "Right";
	float timer_Direction = 0;
	float timer_DirectionTotal = 1;

	float strength = 1000;
	bool isExiting = false;

	[HideInInspector]
	public int dressCode = 0;

	float moveTimer = 0;
	float moveTimerTotal = 0;

	private BehaviorTree.Tree<VisitorProperty_Showcase> _tree;
	public Vector3 curDestination;


	// Use this for initialization
	void Start()
	{
		mainCamera = reference.GetReference<Transform>(referenceCameraName);
		strength = Random.Range(500, 1000);
		nav = GetComponent<NavMeshAgent>();
		nav.enabled = true;

		dressCode = Random.Range(0, characterSetDB.cosplayerSet.Count);
		CharacterSpriteSet c = characterSetDB.cosplayerSet[dressCode];
		dressingUp_Visitor.ClothesSetUp(c.sBody,
										c.sHair,
										c.sHead,
										c.sFace,
										c.sLegsL,
										c.sLegsR,
										c.sHandL,
										c.sHandR);
		AddPoints();
		curDestination = waypoints[0];

/*		var WalkTree = new Tree<VisitorProperty_Showcase>
			(
						new Sequence<VisitorProperty_Showcase>
			(
				new Condition<VisitorProperty_Showcase>(hasWaypoint),
				new Do<VisitorProperty_Showcase>(Walk),
				new Do<VisitorProperty_Showcase>(DestroyPoint)
			)
			);*/
		_tree = new Tree<VisitorProperty_Showcase>
		(
			new Sequence<VisitorProperty_Showcase>
			(
				new Do<VisitorProperty_Showcase>(Walk)
				//new Do<VisitorProperty_Showcase>(waypointsReset)
			)

			);

	}

	// Update is called once per frame
	void Update()
	{
		_tree.Update(this);
		if (!isExiting)
		{
			if (strength > 0)
			{
				strength -= 0.01f * Time.deltaTime;
				if (moveTimer > moveTimerTotal)
				{
					moveTimer = 0;
					moveTimerTotal = Random.Range(1.1f, 3.3f);

					nav.SetDestination(transform.position + new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)));
				}
				else
				{
					moveTimer += Time.deltaTime;
				}
			}
			else
			{
				isExiting = true;
				//nav.SetDestination(visitorManager.exitPoints[Random.Range(0, visitorManager.exitPoints.Length)].transform.position);
			}
		}


		if (nav.velocity.magnitude < 0.1f)
		{
			anim.SetInteger("AnimationState", 0);
		}
		else
		{
			anim.SetInteger("AnimationState", 1);
			anim.SetFloat("Velocity", nav.velocity.magnitude);
		}

		//Direction Change Detection
		if (timer_Direction > timer_DirectionTotal)
		{
			timer_Direction = 0;
			timer_DirectionTotal = Random.Range(0.2f, 1.23f);
			float currentRelativeDirection = Vector3.Dot(nav.velocity, mainCamera.right);
			if (currentRelativeDirection > 0 && currentDirection == "Right")
			{
				currentDirection = "Left";
				var localS = mainBody.localScale;
				localS.x *= -1;
				mainBody.localScale = localS;
			}
			else if (currentRelativeDirection < 0 && currentDirection == "Left")
			{
				currentDirection = "Right";
				var localS = mainBody.localScale;
				localS.x *= -1;
				mainBody.localScale = localS;
			}
		}
		else
		{
			timer_Direction += Time.deltaTime;
		}
		//Actually
		mainBody.LookAt(mainCamera);
	}

	public void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("ExitArea"))
		{

			//visitorManager.RemoveVisitor(this);
			Destroy(gameObject);
		}
	}

    #region Customize Methods
	private bool Walk(VisitorProperty_Showcase context)
    {
		if(Vector3.Distance(curDestination, this.transform.position) < 1f)
        {
			Invoke("waypointsReset", 3);
			//curDestination = new Vector3(Random.Range(-28f, 28f), 0, Random.Range(-28f, 28f));
		}
        else
        {
			nav.SetDestination(curDestination);
		}
		return true;
	}
	private bool hasWaypoint(VisitorProperty_Showcase context)
    {
		if(waypoints.Count>0)
        {
			return true;
		}
		else
        {
			return false;
		}

    }
	private void waypointsReset()
	{
		curDestination = new Vector3(Random.Range(-28f, 28f), 0, Random.Range(-28f, 28f));
	}
	private void AddPoints()
    {
		waypoints.Add(new Vector3(Random.Range(-28f, 28f), 0, Random.Range(-28f, 28f)));
		waypoints.Add(new Vector3(Random.Range(-28f, 28f), 0, Random.Range(-28f, 28f)));
		waypoints.Add(new Vector3(Random.Range(-28f, 28f), 0, Random.Range(-28f, 28f)));
		waypoints.Add(new Vector3(Random.Range(-28f, 28f), 0, Random.Range(-28f, 28f)));
	}

	private bool DestroyPoint(VisitorProperty_Showcase context)
	{
		if (Vector3.Distance(curDestination, this.transform.position) < 1f)
		{
			waypoints.Remove(waypoints[waypoints.Count-1]);
			//curDestination = 
			return true;
		}
		else
        {
			return false;
		}
	}
	#endregion
}

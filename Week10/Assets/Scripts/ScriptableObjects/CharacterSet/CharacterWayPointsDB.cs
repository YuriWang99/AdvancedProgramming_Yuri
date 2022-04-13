using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWayPointsDB : ScriptableObject
{
    public List<Vector3> waypoints = new List<Vector3>();
	public void AddPoints()
	{
		waypoints.Add(new Vector3(Random.Range(-28f, 28f), 0, Random.Range(-28f, 28f)));
		waypoints.Add(new Vector3(Random.Range(-28f, 28f), 0, Random.Range(-28f, 28f)));
		waypoints.Add(new Vector3(Random.Range(-28f, 28f), 0, Random.Range(-28f, 28f)));
		waypoints.Add(new Vector3(Random.Range(-28f, 28f), 0, Random.Range(-28f, 28f)));
	}
	public int Count()
	{
		return waypoints.Count;
	}
	public void Clear()
	{
		waypoints.Clear();
	}
}

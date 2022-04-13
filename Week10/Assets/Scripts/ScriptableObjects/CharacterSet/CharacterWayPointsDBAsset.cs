using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CharacterWayPointsDBAsset
{
    [MenuItem("Assets/Create/ScriptableObjects/CharacterWayPointsDB")]

    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<CharacterWayPointsDB>();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameLevelType
{
    Menu,InGame,EndScreen

}
//Editor function in Assets/Create menu
[CreateAssetMenu(menuName ="Asset/Simple Scriptable Object")]
public class SimpleScriptableObject : ScriptableObject
{
    // Data to save
    public float timeAllowed;
    public int levelID;
    public GameObject[] levelMonster;
    public GameObject[] levelItems;
    public GameLevelType gameLevelType;

    public void Reset()
    {
        levelID = 0;
        timeAllowed = -1;
    }
}

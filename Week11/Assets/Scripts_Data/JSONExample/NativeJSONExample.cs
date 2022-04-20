using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class PlayerInfo
{
    public string name;
    public int lives;
    public float health;
    public Skills skills;

    [System.Serializable]
    public struct Skills
	{
        public string skill1ID;
        public string skill2ID;
        public string skill3ID;
    }

    public static PlayerInfo CreateFromJSON(string path)
    {
        string jsonString = File.ReadAllText(Application.streamingAssetsPath + path);
        return JsonUtility.FromJson<PlayerInfo>(jsonString);
    }
    public static void WriteToJSON(string path, PlayerInfo playerInfo)
    {
        string jsonString = JsonUtility.ToJson(playerInfo);
        File.WriteAllText(Application.streamingAssetsPath + path, jsonString);
    }
}

public class NativeJSONExample : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Save
        PlayerInfo newPlayer = new PlayerInfo();
        newPlayer.name = "Bob";
        newPlayer.lives = 10;
        newPlayer.health = 9999;
        newPlayer.skills.skill1ID = "FireBall";
        newPlayer.skills.skill2ID = "IceBall";
        newPlayer.skills.skill3ID = "DragonBall";

        PlayerInfo.WriteToJSON("/JSON/Playerinfo.json", newPlayer);



        //Create
        PlayerInfo newPlayer2 = PlayerInfo.CreateFromJSON("/JSON/Playerinfo2.json");

        Debug.Log(newPlayer2.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class NativeCSVExample : MonoBehaviour
{
    [HideInInspector]
    public List<string[]> currentAvailableName;

    void Awake()
    {
        string[] writeName = { "Snow", "Bob", "Tom", "Lee", "Lily", "Mei" };
        WriteCsv(writeName, getPath());



        currentAvailableName = ReadCsv(getPath());
    }

    public string GetRandomName()
	{
        return currentAvailableName[Random.Range(1, currentAvailableName.Count)][1];
    }

    private string getPath()
    {
#if UNITY_EDITOR
        return Application.streamingAssetsPath;
#else
        return Application.streamingAssetsPath;
#endif
    }

    private void WriteCsv(string[] strs, string path)
    {
        if (path == null)
            return;
        path += "/CSV/PlayerName.csv";
        StreamWriter stream = new StreamWriter(path);
        stream.Write("Player Name,");
        stream.WriteLine("All the name will be here:");

        for (int i = 0; i < strs.Length; i++)
        {
            if (strs[i] != null)
            {
                //stream.WriteLine(strs[i]);
                stream.WriteLine($"{(i + 1).ToString()},{strs[i]}");
            }

        }
        stream.Close();
        stream.Dispose();
    }

    private List<string[]> ReadCsv(string path)
    {
        List<string[]> list = new List<string[]>();
        string line;
        StreamReader stream = Read(path);
        while ((line = stream.ReadLine()) != null)
        {
            list.Add(line.Split(','));
        }
        stream.Close();
        stream.Dispose();
        return list;
    }
    private StreamReader Read(string path)
    {
        if (path == null)
            return null;
        path += "/CSV/PlayerName.csv";
        if (!File.Exists(path))
            File.CreateText(path);
        return new StreamReader(path);
    }
}

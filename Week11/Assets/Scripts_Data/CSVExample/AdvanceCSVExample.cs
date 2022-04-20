using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Jitbit.Utils;

public class AdvanceCSVExample : MonoBehaviour
{
    string currentAvailableName;

    void Awake()
    {
        //WriteCsv(Application.streamingAssetsPath + "/CSV/PlayerNameAdvance.csv");
        WriteCsv(Application.streamingAssetsPath + "/CSV/Week11.csv");

        //string dataString = Read(Application.streamingAssetsPath + "/CSV/PlayerNameAdvance.csv");
        string Week11String = Read(Application.streamingAssetsPath + "/CSV/Week11.csv");

        //string[] stringSeparators = new string[] { "PlayerName,", ",Health" };
        string[] stringSeparatorsWeek11 = new string []{"Name,", "Lable"};

        //string health = dataString.Split(stringSeparators, System.StringSplitOptions.None)[1];
        string editor = Week11String.Split(stringSeparatorsWeek11, System.StringSplitOptions.None)[1];

        Debug.Log(editor);
        Debug.Log(Read(Application.streamingAssetsPath + "/CSV/Week11.csv"));
    }


    private void WriteCsv(string path)
    {
        StreamWriter stream = new StreamWriter(path);

        var myExport = new CsvExport();

        myExport.AddRow();
        myExport["Name"] = "AssetCube";
        myExport["Lable"] = "Game";
        //myExport["Health"] = 1200;
        //myExport["Any other comments?"] = "Good at archery";

        myExport.AddRow();
        myExport["Name"] = "BlueCube";
        myExport["Lable"] = "Game";
        //myExport["Health"] = 8000;
        //myExport["Any other comments?"] = "She is a lancer";
        myExport.AddRow();
        myExport["Name"] = "RedCube";
        myExport["Lable"] = "Game";

        stream.WriteLine(myExport.Export());

        stream.Close();
        stream.Dispose();
    }

    private string Read(string path)
    {
        return new StreamReader(path).ReadToEnd();
    }
}

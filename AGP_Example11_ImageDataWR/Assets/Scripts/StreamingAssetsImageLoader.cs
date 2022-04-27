using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StreamingAssetsImageLoader : MonoBehaviour
{
    public Image[] m_EditableImages;
    public List<string> m_ImagesNames = new List<string>();
    [SerializeField]
    private void Awake()
    {
        foreach (var image in m_EditableImages)
        {
            m_ImagesNames.Add(image.name);
        }

        if (m_EditableImages.Length == m_ImagesNames.Count) //Si es 0 dejamos las que estaban ya
        {

            LoadImages();

        }
        else
        {
            Debug.LogWarning("==Failed to load images==");
        }

    }
    private void Update()
    {
        if(Input.GetKey(KeyCode.S))
        {
            SwitchImage();
        }

    }

    void SwitchImage()
    {
        for (int i = 0; i < m_EditableImages.Length; i++)
        {
            if (m_EditableImages[i] != null)
            {
                string path = GetImagePath(m_ImagesNames[Random.Range(0,m_ImagesNames.Count)]);
                m_EditableImages[i].sprite = GetSpritefromImage(path);
                //m_EditableImages[i].preserveAspect = true;
            }
            else
            {
                Debug.LogWarning("Null references in EditableImages");
            }
        }
    }
    private void LoadImages()
    {

        for (int i = 0; i < m_EditableImages.Length; i++)
        {
            if (m_EditableImages[i] != null)
            {
                string path = GetImagePath(m_ImagesNames[i]);
                m_EditableImages[i].sprite = GetSpritefromImage(path);
                //m_EditableImages[i].preserveAspect = true;
            }
            else
            {
                Debug.LogWarning("Null references in EditableImages");
            }
        }

    }

    private string GetImagePath(string imgName)
    {

        //Create an array of file paths from which to choose
        string folderPath = Application.streamingAssetsPath + "/ImageAssets/";  //Get path of folder
        //string[] filePaths = Directory.GetFiles(folderPath, "*.png"); // Get all files of type .png in this folder
        string path = folderPath + imgName + ".jpg"; //han de ser formato png

        return path;
    }

    private Sprite GetSpritefromImage(string imgPath)
    {

        //Converts desired path into byte array
        byte[] pngBytes = System.IO.File.ReadAllBytes(imgPath);

        //Creates texture and loads byte array data to create image
        Texture2D tex = new Texture2D(10, 10);
        tex.LoadImage(pngBytes);

        //Creates a new Sprite based on the Texture2D
        Sprite fromTex = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);

        return fromTex;

    }

}
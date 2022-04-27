using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageLoader : MonoBehaviour
{
    public delegate void OnLoadFinished();

    public OnLoadFinished onLoadFinished;

    [SerializeField] private CharacterSetDB characterSetDB;

    private void Start()
    {
        StartCoroutine(LoadingClothes());
    }

    public IEnumerator LoadingClothes()
    {
        Debug.Log("Work");
        yield return IMG2Sprites.LoadAndCropAllFilesToSprites(Application.streamingAssetsPath + "/Image/Cosplayers/");
        List<Sprite> allCustomCosplayer_StreamingAsset = IMG2Sprites.returnResults_Sprites;

        Sprite[] allEmbedCosplayer_Resourecs = Resources.LoadAll<Sprite>("Cosplayers/Random/");

        Sprite[] allCosplayerSprite;

        int currentIndex = 0;

        if (allCustomCosplayer_StreamingAsset.Count > 0)
        {
            //Load Sprite into allCosplayerSprites
            allCosplayerSprite = new Sprite[allEmbedCosplayer_Resourecs.Length + allCustomCosplayer_StreamingAsset.Count];

            for (int i = 0; i < allCustomCosplayer_StreamingAsset.Count; i++)
            {
                allCosplayerSprite[i] = allCustomCosplayer_StreamingAsset[i];
            }
            currentIndex = allCustomCosplayer_StreamingAsset.Count;
        }
        else
        {
            allCosplayerSprite = new Sprite[allEmbedCosplayer_Resourecs.Length];
        }

        for (int i = 0; i < allEmbedCosplayer_Resourecs.Length; i++)
        {
            allCosplayerSprite[currentIndex + i] = allEmbedCosplayer_Resourecs[i];
        }

        characterSetDB.cosplayerSet.Clear();

        for (int i = 0; i < allCosplayerSprite.Length; i += 8)
        {
            CharacterSpriteSet characterSpriteSet = new CharacterSpriteSet();
            characterSpriteSet.ClothesSetUp(allCosplayerSprite[i + 7], allCosplayerSprite[i + 5],
                allCosplayerSprite[i + 4], allCosplayerSprite[i + 6], allCosplayerSprite[i + 3],
                allCosplayerSprite[i + 2], allCosplayerSprite[i + 1], allCosplayerSprite[i + 0]);
            characterSetDB.cosplayerSet.Add(characterSpriteSet);
        }

        yield return "Loaded Clothes";

        onLoadFinished?.Invoke();
    }
}
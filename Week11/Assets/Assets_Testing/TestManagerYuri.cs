using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

internal class TestManagerYuri : MonoBehaviour
{
    [SerializeField]private string assetAddress;
    public List<string> AssetAddress;
    StreamReader strReader;

    private AsyncOperationHandle<GameObject> handle;
    void Start()
    {
        strReader = new StreamReader (Application.streamingAssetsPath + "/CSV/Week11.csv");

        for (int i = 0; i < int.MaxValue ; i++)
        {

            var ReadValueReader = strReader.ReadLine();      
            //Debug.Log(ReadValueReader + i);
            if (ReadValueReader != null)
            {
                var ReadValue = ReadValueReader.Split(',');
                if (ReadValue[0] != ""&& ReadValue[0] != "Name")
                {
                    AssetAddress.Add(ReadValue[0]+".prefab");
                }
                
            }
            else
            {
                break;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            foreach (var AssetAddress in AssetAddress)
            {
                handle = Addressables.LoadAssetAsync<GameObject>(assetAddress + AssetAddress);
                Debug.Log(assetAddress + AssetAddress);
                handle.Completed += Handle_Completed;
            }
        }

    }

    private void Handle_Completed(AsyncOperationHandle<GameObject> operation)
    {
        if (operation.Status == AsyncOperationStatus.Succeeded)
        {
            Instantiate(operation.Result, transform);
        }
        else
        {
            Debug.LogError($"Asset for {assetAddress + AssetAddress} failed to load.");
        }
    }
    private void OnDestroy()
    {

        Addressables.Release(handle);
    }
}

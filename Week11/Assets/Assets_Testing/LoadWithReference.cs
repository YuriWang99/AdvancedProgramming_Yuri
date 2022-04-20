using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

internal class LoadWithReference : MonoBehaviour
{
/*    //assign in editor
    public AssetReference reference;
    
    void Start()
    {
        AsyncOperationHandle handle = reference.LoadAssetAsync<GameObject>();
        handle.Completed += Handle_Completed;

    }

    private void Handle_Completed(AsyncOperationHandle obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            Instantiate(reference.Asset, transform);
        }
        else
        {
            Debug.LogError($"AssetReference {reference.RuntimeKey} failed to load.");
        }
    }

    private void OnDestroy()
    {
        reference.ReleaseAsset();
    }*/
}

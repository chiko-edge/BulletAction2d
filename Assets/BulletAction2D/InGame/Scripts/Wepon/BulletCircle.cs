using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class BulletCircle : MonoBehaviour
{
    private SpriteRenderer spriteRenderer = null;

    private AsyncOperationHandle<Sprite> circleSpriteHandle;

    private string assetName;

    // Start is called before the first frame update
    void Start()
    {
        assetName = "circle_1";

        spriteRenderer = GetComponent<SpriteRenderer>();
        Addressables.LoadAssetAsync<Sprite>(assetName).Completed += handle =>
        {
            circleSpriteHandle = handle;
            if (circleSpriteHandle.Result == null)
            {
                Debug.LogError("load error = " + assetName);
                return;
            }
            spriteRenderer.sprite = circleSpriteHandle.Result;
        };


    }

    private void OnDestroy()
    {
        if (circleSpriteHandle.IsValid())
        {
            Addressables.Release(circleSpriteHandle);
        }
    }


    public void SetCirclePos(Vector2 position)
    {
        transform.position = Camera.main.ScreenToWorldPoint( new Vector3(position.x, position.y, 1));
    }

}

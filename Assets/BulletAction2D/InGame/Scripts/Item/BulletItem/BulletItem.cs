using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class BulletItem : MonoBehaviour
{
    //�e�f�[�^�̃t�@�C�������w��
    [SerializeField]
    private string bulletDataName;
    [SerializeField]
    private SpriteRenderer spriteRenderer = null;

    private AsyncOperationHandle<BulletData> bulletDataHandle;

    private BulletData data;

    /// <summary>
    /// �v���p�e�B�Q
    /// </summary>
    public BulletData Data
    {
        get { return data; }
        set { data = value; }
    }

    private void Awake()
    {
        Addressables.LoadAssetAsync<BulletData>(bulletDataName).Completed += handle =>
        {
            bulletDataHandle = handle;
            if (bulletDataHandle.Result == null)
            {
                Debug.LogError("load error = " + bulletDataName);
                Destroy(gameObject);
                return;
            }

            Data = bulletDataHandle.Result;
            spriteRenderer.sprite = data.bulletIconSprite;

        };
    }

}

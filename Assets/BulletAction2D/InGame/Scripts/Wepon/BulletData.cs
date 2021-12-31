using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BulletData : ScriptableObject
{
    /// <summary>
    /// 詳細説明
    /// </summary>
    public string discription;

    /// <summary>
    /// 弾の移動速度
    /// </summary>
    public float moveSpeed;

    /// <summary>
    /// 攻撃力
    /// </summary>
    public float attacPower;

    /// <summary>
    /// 発射間隔
    /// </summary>
    public float recastTime;

    /// <summary>
    /// 最大移動距離
    /// </summary>
    public float maxMoveRange;

    /// <summary>
    /// 発射する弾のタイプ
    /// </summary>
    public BulletType bulletType;

    /// <summary>
    /// 弾道タイプ
    /// </summary>
    public BulletBallisticType BulletBallisticType;

    /// <summary>
    /// 弾速タイプ
    /// </summary>
    public BulletAccelerationType bulletAccelerationType;

    /// <summary>
    /// 着弾動作タイプ
    /// </summary>
    public BulletImpactMovementType bulletImpactMovementType;

    /// <summary>
    /// 弾が起こす効果タイプ
    /// </summary>
    public BulletEffectType bulletEffectType;

    /// <summary>
    /// 弾の画像
    /// </summary>
    public Sprite bulletSprite;

    /// <summary>
    /// 弾のアイコン
    /// </summary>
    public Sprite bulletIconSprite;

    /// <summary>
    /// 弾道タイプが落下のときに使用
    /// </summary>
    public float downForce;

    /// <summary>
    /// 加速度
    /// 1Fで増える値
    /// </summary>
    public float acceleration;

    /// <summary>
    /// 最大加速度
    /// </summary>
    public float maxAcceleration;

    /// <summary>
    /// 加速開始距離
    /// </summary>
    public float starAccelerationDistance;

    /// <summary>
    /// 発射する弾の個数
    /// </summary>
    public int bulletQuantity;

    /// <summary>
    /// 何度間隔で発射するか
    /// </summary>
    public float bulletAngle;

    /// <summary>
    /// wave時の動き幅
    /// </summary>
    public float waveWidth;


}

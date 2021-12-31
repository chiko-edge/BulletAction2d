using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BulletData : ScriptableObject
{
    /// <summary>
    /// �ڍא���
    /// </summary>
    public string discription;

    /// <summary>
    /// �e�̈ړ����x
    /// </summary>
    public float moveSpeed;

    /// <summary>
    /// �U����
    /// </summary>
    public float attacPower;

    /// <summary>
    /// ���ˊԊu
    /// </summary>
    public float recastTime;

    /// <summary>
    /// �ő�ړ�����
    /// </summary>
    public float maxMoveRange;

    /// <summary>
    /// ���˂���e�̃^�C�v
    /// </summary>
    public BulletType bulletType;

    /// <summary>
    /// �e���^�C�v
    /// </summary>
    public BulletBallisticType BulletBallisticType;

    /// <summary>
    /// �e���^�C�v
    /// </summary>
    public BulletAccelerationType bulletAccelerationType;

    /// <summary>
    /// ���e����^�C�v
    /// </summary>
    public BulletImpactMovementType bulletImpactMovementType;

    /// <summary>
    /// �e���N�������ʃ^�C�v
    /// </summary>
    public BulletEffectType bulletEffectType;

    /// <summary>
    /// �e�̉摜
    /// </summary>
    public Sprite bulletSprite;

    /// <summary>
    /// �e�̃A�C�R��
    /// </summary>
    public Sprite bulletIconSprite;

    /// <summary>
    /// �e���^�C�v�������̂Ƃ��Ɏg�p
    /// </summary>
    public float downForce;

    /// <summary>
    /// �����x
    /// 1F�ő�����l
    /// </summary>
    public float acceleration;

    /// <summary>
    /// �ő�����x
    /// </summary>
    public float maxAcceleration;

    /// <summary>
    /// �����J�n����
    /// </summary>
    public float starAccelerationDistance;

    /// <summary>
    /// ���˂���e�̌�
    /// </summary>
    public int bulletQuantity;

    /// <summary>
    /// ���x�Ԋu�Ŕ��˂��邩
    /// </summary>
    public float bulletAngle;

    /// <summary>
    /// wave���̓�����
    /// </summary>
    public float waveWidth;


}

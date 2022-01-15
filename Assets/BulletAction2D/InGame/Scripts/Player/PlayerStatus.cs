using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    PlayerStatusType status = PlayerStatusType.NONE;

    public PlayerStatusType Status
    {
        get { return status; }
        set { status = value; }
    }

    public bool isMove()
    {
        if (Status.Equals(PlayerStatusType.INVENTORY_OPEN))
        {
            return false;
        }

        return true;
    }

    public bool isJump()
    {
        if (Status.Equals(PlayerStatusType.INVENTORY_OPEN))
        {
            return false;
        }

        return true;
    }

    public bool isFire()
    {
        if (Status.Equals(PlayerStatusType.INVENTORY_OPEN))
        {
            return false;
        }

        return true;
    }

    public bool isCircle()
    {
        if (Status.Equals(PlayerStatusType.INVENTORY_OPEN))
        {
            return false;
        }

        return true;
    }

    public bool isItemGet()
    {
        if (Status.Equals(PlayerStatusType.INVENTORY_OPEN))
        {
            return false;
        }

        return true;
    }

    public bool isBulletChange()
    {
        if (Status.Equals(PlayerStatusType.INVENTORY_OPEN))
        {
            return false;
        }

        return true;
    }

    public bool isInventoryOpen()
    {

        return true;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wepon : MonoBehaviour
{
    private BulletCircle bulletCircle = null;


    private void Start()
    {
        bulletCircle = GetComponentInChildren<BulletCircle>();
    }

    public void SetCircle(Vector2 position)
    {
        bulletCircle.SetCirclePos(position);
    }
}

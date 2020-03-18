using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandeler : MonoBehaviour
{
    BoatMovementV01 boat;
    private void Start()
    {
        boat = FindObjectOfType<BoatMovementV01>();
    }
    public void KnockingBack(int i)
    {
        if(i == 1)
        {
            boat.KnockbackLand(1);
        }
        if(i == 2)
        {
            boat.KnockbackLand(2);
        }
        if (i == 3)
        {
            boat.KnockbackLand(3);
        }
        if (i == 4)
        {
            boat.KnockbackLand(4);
        }
    }
}

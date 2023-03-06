using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Grass grass))
        {
            if (GameController.instance.weaponActivate == true)
            {
                GameController.instance.weaponActivate = false;

                grass.Hit();
            }
        }
    }
}

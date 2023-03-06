using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheatBlock : MonoBehaviour
{
    bool key;

    private void Start()
    {
        key = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            if (key == true)
            {
                key = false;

                if (BlockManager.instance.backpackItems < BlockManager.instance.maxItems)
                {
                    
                    GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.None;                   

                    UIManager.instance.ChangeWheatUIPlus();

                    BlockManager.instance.Collect(gameObject);

                    SoundManager.instance.PlaySound(SoundManager.AudioType.loot, 0.8f, Random.Range(0.95f, 1.1f), false);
                }
                else
                {
                    print("MaxItem");
                }
            }
        }
    }
}

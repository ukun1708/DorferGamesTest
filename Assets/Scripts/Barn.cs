using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barn : MonoBehaviour
{
    float timer;
    bool key = false;
    private void Update()
    {
        if (key == true)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                key = false;
                CoinManager.instance.CoinCreator(transform);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<WheatBlock>())
        {
            VfxManager.instance.PlayVFX(VfxManager.VfxType.blockFX, other.transform.position);

            SoundManager.instance.PlaySound(SoundManager.AudioType.loot, 0.8f, Random.Range(0.95f, 1.1f), false);

            CoinManager.instance.coinCount++;

            key = true;

            timer = 0.75f;

            Destroy(other.gameObject);
        }
    }
}

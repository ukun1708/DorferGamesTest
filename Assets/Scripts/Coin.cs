using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    GameController controller;
    bool collect;

    public GameObject target;

    private void Start()
    {
        collect = false;
        controller = GameController.instance;

        StartCoroutine(Collect());
    }
    private void Update()
    {
        transform.Rotate(Vector3.up * -500 * Time.deltaTime);
    }
    IEnumerator Collect()
    {
        yield return new WaitForSeconds(0.75f);

        GetComponent<Collider>().isTrigger = true;

        float randomTime = Random.Range(0.1f, 0.3f);
        yield return new WaitForSeconds(randomTime);

        collect = true;

        Vector3 targetPos = Camera.main.ScreenToWorldPoint(target.transform.position);

        yield return StartCoroutine(LerpManager.instance.LerpPos(gameObject, transform.position, targetPos, 5f));        

        SoundManager.instance.PlaySound(SoundManager.AudioType.coin, 0.8f, Random.Range(0.95f, 1.1f), false);

        VfxManager.instance.PlayVFX(VfxManager.VfxType.coin, transform.position);

        UIManager.instance.currentCoin += 15f;

        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Nature : MonoBehaviour
{
    private void Start()
    {
        transform.rotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0f);

        if (transform.localScale != Vector3.one)
        {
            StartCoroutine(Recovery());
        }
    }
    public void Hit()
    {
        GameController.instance.weaponActivate = false;

        VfxManager.instance.PlayVFX(VfxManager.VfxType.hit, transform.position);

        SoundManager.instance.PlaySound(SoundManager.AudioType.hit, 0.3f, Random.Range(0.95f, 1.1f), false);

        BlockManager.instance.CreateBlocks(transform);

        GameObject wheat = Instantiate(GameController.instance.wheatDestroyer, transform.position, transform.rotation);

        List<GameObject> fragments = new List<GameObject>();

        for (int i = 0; i < wheat.transform.childCount; i++)
        {
            fragments.Add(wheat.transform.GetChild(i).gameObject);
        }

        foreach (var fragment in fragments)
        {
            fragment.transform.parent = null;
            fragment.AddComponent<Rigidbody>();
            fragment.AddComponent<Piece>();
            fragment.AddComponent<MeshCollider>();
            fragment.GetComponent<MeshCollider>().convex = true;
            Rigidbody rigidbody = fragment.GetComponent<Rigidbody>();
            rigidbody.AddExplosionForce(200f, transform.position, 10f);
        }

        transform.tag = "Untagged";

        transform.localScale = Vector3.zero;

        TargetManager.instance.target = null;

        TargetManager.instance.FindClosestTarget();

        GameObject grass = Instantiate(GameController.instance.wheat, transform.position, Quaternion.identity);

        grass.transform.localScale = Vector3.zero;

        grass.tag = "Untagged";

        Destroy(gameObject);
    }

    IEnumerator Recovery()
    {
        yield return new WaitForSeconds(10f);
        yield return StartCoroutine(LerpManager.instance.LerpScale(gameObject, Vector3.zero, Vector3.one, 0.1f));

        transform.tag = "Target";

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Scale());

        Physics.IgnoreCollision(GetComponent<Collider>(), GameController.instance.player.GetComponent<Collider>(), true);
    }
    IEnumerator Scale()
    {
        yield return new WaitForSeconds(Random.Range(1f, 2f));

        yield return StartCoroutine(LerpManager.instance.LerpScale(gameObject, transform.localScale, Vector3.zero, 1f));

        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    #region Singleton
    public static BlockManager instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    [SerializeField]
    GameObject[] blocks;
    [SerializeField]
    GameObject backpack;

    public int maxItems = 8;
    public int backpackItems = 0;

    [SerializeField]
    int maxItemsColumn = 4;

    public void CreateBlocks(Transform grass)
    {
        for (int i = 0; i < Random.Range(1,1); i++)
        {
            Vector3 createPos = new Vector3(grass.position.x, grass.position.y + 1f, grass.position.z);

            GameObject currentLog = Instantiate(blocks[Random.Range(0, blocks.Length)], createPos, Quaternion.identity);

            Rigidbody rigidbody = currentLog.GetComponent<Rigidbody>();
            rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
            rigidbody.AddForce(new Vector3(Random.Range(-1, 1f), 1, Random.Range(-1, 1f)), ForceMode.Impulse);
        }
    }

    public void Collect(GameObject block)
    {
        block.GetComponent<Rigidbody>().isKinematic = true;
        block.GetComponent<Collider>().enabled = false;
        block.transform.SetParent(backpack.transform);

        int currentCountItems = backpackItems;
        Vector3 pos = Vector3.zero;

        if (currentCountItems < 1)
        {
            backpackItems++;
            StartCoroutine(Lerper(block, pos));
        }
        if (currentCountItems > 0)
        {
            pos = new Vector3(0f, currentCountItems * 0.0055f - currentCountItems / maxItemsColumn * 0.0055f * maxItemsColumn, -currentCountItems / maxItemsColumn * 0.0055f);
            backpackItems++;
            StartCoroutine(Lerper(block, pos));
        }
    }
    IEnumerator Lerper(GameObject block, Vector3 pos)
    {
        float time = 0f;
        while (time < 1)
        {
            time += Time.deltaTime;
            block.transform.localPosition = Vector3.Lerp(block.transform.localPosition, pos, time);
            block.transform.localRotation = Quaternion.Lerp(block.transform.localRotation, Quaternion.Euler(0f, 0f, 90f), time);
            yield return null;
        }
    }
    public void PumpingBlock(Vector3 targetPos)
    {

        StartCoroutine(Pumping(targetPos));

    }
    IEnumerator Pumping(Vector3 targetPos)
    {
        if (backpack.transform.childCount > 0)
        {
            GameObject block = backpack.transform.GetChild(backpack.transform.childCount - 1).gameObject;
            block.transform.parent = null;

            Physics.IgnoreCollision(block.GetComponent<Collider>(), GameController.instance.player.GetComponent<Collider>(), true);

            block.GetComponent<Collider>().enabled = true;

            UIManager.instance.ChangeWheatUIMinus();

            backpackItems--;            

            StartCoroutine(LerpManager.instance.LerpPosCurve(block, block.transform.position, targetPos, 3f));

            yield return new WaitForSeconds(0.1f);

            StartCoroutine(Pumping(targetPos));
        }
    }
}

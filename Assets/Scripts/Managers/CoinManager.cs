using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    #region Singleton
    public static CoinManager instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    [SerializeField]
    GameObject coin;

    [SerializeField]
    GameObject coinUI;

    public int coinCount;

    private void Start()
    {
        coinCount = 0;
    }

    public void CoinCreator(Transform barn)
    {
        StartCoroutine(Creator(barn));
    }

    IEnumerator Creator(Transform barn)
    {
        for (int i = 0; i < coinCount; i++)
        {
            Vector3 createPos = new Vector3(barn.position.x, barn.position.y + 1f, barn.position.z);

            GameObject currentCoin = Instantiate(coin, createPos, Quaternion.identity);
            currentCoin.transform.parent = null;
            currentCoin.transform.rotation = Quaternion.Euler(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360));

            currentCoin.GetComponent<Rigidbody>().AddRelativeForce(Vector3.up * 10f, ForceMode.Impulse);

            currentCoin.GetComponent<Coin>().target = coinUI;

            yield return new WaitForSeconds(0.05f);
        }

        coinCount = 0;
    }
}

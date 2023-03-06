using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    #region Singleton
    public static UIManager instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    [SerializeField]
    TextMeshProUGUI coinCountTMPro;

    [SerializeField]
    GameObject coinIcon;

    [SerializeField]
    TextMeshProUGUI WheatCountTMPro;

    float coinCount = 0;

    public float currentCoin = 0f;

    Vector3 coinIconStartPos;

    private void Start()
    {
        coinCountTMPro.text = coinCount.ToString();
        coinIconStartPos = coinIcon.transform.position;

        WheatCountTMPro.text = BlockManager.instance.backpackItems + "/" + BlockManager.instance.maxItems.ToString();
    }

    private void Update()
    {
        if (coinCount < currentCoin)
        {
            coinCount += Time.deltaTime * 100f;
            int value = (int)coinCount;
            coinCountTMPro.text = value.ToString();

            coinIcon.transform.position += Random.insideUnitSphere * 1f;
        }
        else
        {
            coinIcon.transform.position = coinIconStartPos;
        }
    }
    public void ChangeWheatUIPlus()
    {
        int CountText = BlockManager.instance.backpackItems;

        CountText++;

        WheatCountTMPro.text = CountText.ToString() + "/" + BlockManager.instance.maxItems.ToString();
    }
    public void ChangeWheatUIMinus()
    {
        int CountText = BlockManager.instance.backpackItems;

        CountText--;

        WheatCountTMPro.text = CountText.ToString() + "/" + BlockManager.instance.maxItems.ToString();
    }
}

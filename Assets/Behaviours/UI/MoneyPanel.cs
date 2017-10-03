using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TransactionTypes
{
    COLLISION = -100,
    SPEEDING = -50,
    FAILED_DELIVERY = -1000,
    DELIVERY = 2000
}

public class MoneyPanel : MonoBehaviour
{
    public int money;

    [Header("Parameters")]
    [SerializeField] GameObject transaction_prefab;
    [SerializeField] float transaction_fade_delay;
    [SerializeField] float transaction_fade_duration;

    [Header("References")]
    [SerializeField] Text money_display;
    [SerializeField] Transform grid_transform;


    public void LogTransaction(int _amount, string _note)
    {
        GameObject clone = Instantiate(transaction_prefab, grid_transform);
        TransactionUIElement t = clone.GetComponent<TransactionUIElement>();

        money += _amount;
        money_display.text = money.ToString();

        t.Init(_amount, _note, transaction_fade_delay, transaction_fade_duration);
        t.transform.SetAsFirstSibling();
    }

}

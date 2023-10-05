using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Stock_Data : MonoBehaviour
{
    [SerializeField] private TMP_Text stock_name;
    [SerializeField] private TMP_Text symbol_name;
    [SerializeField] private TMP_Text stock_price;
    [SerializeField] private TMP_Text stock_percent;

    protected internal void SetData
        (string stock_name, 
        string symbol_name, 
        float stock_price, 
        float stock_percent)
    {
        this.stock_name.text = stock_name;
        this.symbol_name.text = "Symbol : " + symbol_name;
        this.stock_price.text = "$ " + stock_price.ToString();
        this.stock_percent.text = "+ % " + stock_percent.ToString();
    }
}

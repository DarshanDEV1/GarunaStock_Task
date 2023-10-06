using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class FetchData : MonoBehaviour
{
    [SerializeField] private Transform _stockParent; // Parent of the UI object instances
    [SerializeField] private GameObject _stockPrefab; // UI object to display the data
    [SerializeField] private TMP_InputField _searchInput; // Input field for stock symbol
    [SerializeField] private Button _searchButton; // Search button
    [SerializeField] private Button _clearButton; // Clear button

    [SerializeField] private List<GameObject> _stockList;

    [SerializeField] private string _json_url;

    void Start()
    {
        // Add a click listener to the search button
        _searchButton.onClick.AddListener(() =>
        {
            foreach(var stock in _stockList)
            {
                Destroy(stock);
            }
            _stockList.Clear();
            GetStockList();
        });

        _clearButton.onClick.AddListener(() =>
        {
            _searchInput.text = "";
        });
    }

    void GetStockList()
    {
#if UNITY_EDITOR
    _json_url = "http://localhost:3000/api/stocks";
#elif UNITY_ANDROID
    _json_url = "https://raw.githubusercontent.com/DarshanDEV1/GarunaStock_Task/master/NodeJS/StockAPI/stocks%20vr%20api/stocks.json";
#elif UNITY_IOS
    _json_url = "https://raw.githubusercontent.com/DarshanDEV1/GarunaStock_Task/master/NodeJS/StockAPI/stocks%20vr%20api/stocks.json";
#else
    _json_url = "http://localhost:3000/api/stocks";
#endif
        //StartCoroutine(GetList("http://localhost:3000/api/stocks"));
        StartCoroutine(GetList(_json_url));
    }

    IEnumerator GetList(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                Debug.Log(": Error: " + webRequest.error);
            }
            else
            {
                // Parse the JSON data
                var stocksDataWrapper = JsonUtility.FromJson<StocksDataWrapper>(webRequest.downloadHandler.text);

                // Display the data on the Unity canvas
                foreach (var stock in stocksDataWrapper.stocks)
                {
                    if(stock.symbol.ToLower() == _searchInput.text.ToLower())
                    {
                        var _stock = Instantiate(_stockPrefab, _stockParent);
                        _stock.GetComponent<Stock_Data>().SetData
                            (stock.name,
                            stock.symbol,
                            stock.price,
                            stock.percent_change);

                        _stockList.Add(_stock);
                    }
                    //displayText.text += (stock.percent_change).ToString() + "\n";
                }
            }
        }
    }
}

[System.Serializable]
public class StocksDataWrapper
{
    public StocksData[] stocks;
}

[System.Serializable]
public class StocksData
{
    public string symbol;
    public string name;
    public string exchange;
    public float price;
    public float change;
    public float percent_change;
    public string url;


    public override string ToString()
    {
        return $"Symbol: {symbol}\nName: {name}\nPrice: {price}";
    }
}

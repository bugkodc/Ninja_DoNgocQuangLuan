using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;
    /*public static UiManager Instance 
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<UiManager>();
            }
            return instance;
        }
    }*/
    [SerializeField] TextMeshProUGUI textCoin;
    private void Awake()
    {
        instance = this;
    }
   public void SetTextCoin(int coin)
    {
        textCoin.text = coin.ToString();
    }
}

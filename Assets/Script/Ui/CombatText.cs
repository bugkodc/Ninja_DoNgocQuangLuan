using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CombatText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;

    void Start()
    {
       
    }
    public void OnInit(float hp)
    {
        textMeshPro.text = hp.ToString();
        Invoke(nameof(OnDespam), 1f);
    }
    public void OnDespam()
    {
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LumiTestItem : LumiScrollItem
{
    [SerializeField] private TMPro.TMP_Text txtLumi;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateData(int index)
    {
        txtLumi.text = index.ToString();
    }
}

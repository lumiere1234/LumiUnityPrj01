using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LumiScrollTest : MonoBehaviour
{
    [SerializeField] private LumiScrollList scrollList;
    private void Awake()
    {
        scrollList.SetScrollListUpdateFunc(UpdateScrollItem);
    }
    // Start is called before the first frame update
    void Start()
    {
        RefreshScrollList();    
    }

    void RefreshScrollList()
    {
        int count = 40;
        scrollList.AddItems(0, count);
        scrollList.DoForceUpdate(true);
    }

    void UpdateScrollItem(LumiScrollItem item, int index)
    {
        //Debug.Log($"lumiere index : {index}");
        item.bInitial = true;

        LumiTestItem tItem = (LumiTestItem)item;
        tItem.UpdateData(index);
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            scrollList.ScrollToBegin();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            scrollList.ScrollToEnd();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            scrollList.JumpToItem(10);
        }
    }
}

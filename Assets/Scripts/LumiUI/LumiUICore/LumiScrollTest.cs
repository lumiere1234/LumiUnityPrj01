using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LumiScrollTest : MonoBehaviour
{
    [SerializeField] private LumiScrollList scrollList;
    [SerializeField] private LumiScrollGrid scrollGrid;
    [SerializeField] private Button btnItem;
    private void Awake()
    {
        scrollList.SetScrollListUpdateFunc(UpdateScrollItem);
        scrollGrid.SetScrollGridUpdateFunc(UpdateScrollItem);
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
        scrollList.AddItems(1, 5);
        scrollList.AddItems(0, 5);
        scrollList.AddItems(1, 5);
        scrollList.AddItems(0, 5);
        scrollList.DoForceUpdate(true);
        scrollGrid.AddItems(0, 40);
        scrollGrid.DoForceUpdate(true);
    }

    void UpdateScrollItem(LumiScrollItem item, int index, int typeId)
    {
        //Debug.Log($"lumiere index : {index}");
        item.bInitial = true;

        Button btn = item.gameObject.GetComponent<Button>();
        if (btn != null ) {
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() => {
                Debug.Log($"Lumiere get {index}");
            });
        }

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

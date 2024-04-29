using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMgr : SingletonAutoMono<ItemMgr>
{
    // normal item
    private Dictionary<int, ItemData> ItemList = new Dictionary<int, ItemData>();
    
    public void ItemChange(int itemId, int itemCount)
    {

    }
    // save data
    public void DoAddSaveData(ref SaveDataInfo info)
    {
        info.itemList.Clear();
        info.itemList.Capacity = ItemList.Count;
        foreach(var item in ItemList)
        {
            ItemSaveData sData = item.Value.GetSaveData();
            info.itemList.Add(sData);
        }
    }
    public void DoLoadSaveData(SaveDataInfo data)
    {
        if (data.saveId < 0)
        {
            // ¿Õ³õÊ¼»¯
            ItemList.Clear();
        }
        else
        {
            ItemList.Clear();
            foreach(var sData in data.itemList)
            {
                ItemData item = new ItemData(sData.itemId);
                item.Count = sData.itemCount;
                ItemList.Add(item.ItemID, item);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataBase
{
    public int ItemID = 0;         // 道具Id
    public ItemCfg m_ItemCfg = null; // 道具配置
    public EItemType ItemType => m_ItemCfg != null ? (EItemType)m_ItemCfg.itemType : EItemType.Default;
    public ItemDataBase(int _itemID)
    {
        this.ItemID = _itemID;
        m_ItemCfg = GameConfigDataBase.GetConfigData<ItemCfg>(ItemID.ToString());
    }
}
public class ItemData : ItemDataBase
{
    public int Count = 0;
    public ItemData(int _itemID) : base(_itemID)
    {
        
    }

    public ItemSaveData GetSaveData()
    {
        ItemSaveData saveData = new ItemSaveData();
        saveData.itemId = ItemID;
        saveData.itemCount = Count;
        return saveData;
    }
}

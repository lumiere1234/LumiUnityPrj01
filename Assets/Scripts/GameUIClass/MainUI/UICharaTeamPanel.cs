using Newtonsoft.Json.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICharaTeamPanel : BasePanel
{
    [SerializeField] private LumiScrollList TagList;

    int currentId = 0;
    List<string> tags = new List<string>();
    private void Awake()
    {
        TagList.SetScrollListUpdateFunc(OnScrollListUpdateFunc);
        tags.Add("lumi01");
        tags.Add("lumi02");
        tags.Add("lumi03");
        tags.Add("lumi04");
        tags.Add("lumi05");
        currentId = 0;
    }
    private void RefreshPanel()
    {
        TagList.ResetList();
        TagList.AddItems(0, tags.Count);
        TagList.DoForceUpdate(true);
    }
    private void OnScrollListUpdateFunc(LumiScrollItem item, int index, int typeId)
    {
        UITeamTagItem tagItem = item.GetComponent<UITeamTagItem>();
        if (tagItem == null)
            return;

    }
}

using Newtonsoft.Json.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICharaTeamPanel : BasePanel
{
    [SerializeField] private LumiScrollList TagList;

    int currentId = 0;
    int maxteamcount = 5;
    protected override void Awake()
    {
        TagList.SetScrollListUpdateFunc(OnScrollListUpdateFunc);
        currentId = 0;
    }
    public override void DoShowPanel(params object[] args)
    {
        base.DoShowPanel(args);

        RefreshPanel();
    }
    private void RefreshPanel()
    {
        TagList.ResetList();
        TagList.AddItems(0, maxteamcount);
        TagList.DoForceUpdate(true);
    }
    private void OnScrollListUpdateFunc(LumiScrollItem item, int index, int typeId)
    {
        UITeamTagItem tagItem = item.GetComponent<UITeamTagItem>();
        if (tagItem == null)
            return;
        tagItem.SetName(StringDef.TeamPanelTagPrefix + $"{index : 00}");
    }
}

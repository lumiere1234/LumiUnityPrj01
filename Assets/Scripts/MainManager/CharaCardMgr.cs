using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaCardMgr : SingletonAutoMono<CharaCardMgr>
{
    private List<CharaCardInfo> _cardInfos = new List<CharaCardInfo>();
    public List<CharaCardInfo> CardInfos => _cardInfos;

    public void AddCharaCard(int cardId)
    {
        CharaCardInfo info = new CharaCardInfo(cardId);
        _cardInfos.Add(info);

        EventMgr.Instance.Invoke(EventDef.CharaCard_AddCard, info);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            AddCharaCard(10008);
            AddCharaCard(10012);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            AddCharaCard(10005);
            AddCharaCard(10011);
        }
    }

    // serialize
    public void DoAddSaveData(ref SaveDataInfo info)
    {
        info.cardList.Clear();
        for (int i = 0; i < CardInfos.Count; i++)
        {
            var cardData = CardInfos[i].GetSaveData();
            info.cardList.Add(cardData);
        }
    }
    public void DoLoadSaveData(SaveDataInfo data)
    {
        if (data.saveId < 0)
        {
            // ¿Õ³õÊ¼»¯
            CardInfos.Clear();
        }
        else
        {
            CardInfos.Clear();
            foreach(var cardData in data.cardList)
            {
                CharaCardInfo cardInfo = new CharaCardInfo(cardData.cardId);
                cardInfo.star = cardData.cardStar;
                CardInfos.Add(cardInfo);
            }
        }
    }
}

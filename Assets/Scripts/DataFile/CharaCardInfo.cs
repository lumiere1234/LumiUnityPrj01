using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaCardInfo
{
    // save data
    public int star { get; set; }
    public int level { get; set; }

    // cfg data
    public int CardId = 0;
    private CharaCardCfg _cardCfg;
    public CharaCardCfg CardCfg
    {
        get
        {
            if (_cardCfg == null)
            {
                _cardCfg = GameConfigDataBase.GetConfigData<CharaCardCfg>(CardId.ToString());
            }
            return _cardCfg;
        }
        set
        {
            _charaCfg = GameConfigDataBase.GetConfigData<CharacterCfg>(CardCfg.character.ToString());
        }
    }
    private CharacterCfg _charaCfg;
    public CharacterCfg CharaCfg
    { 
        get
        {
            if (_charaCfg == null)
            {
                _charaCfg = GameConfigDataBase.GetConfigData<CharacterCfg>(CardCfg.character.ToString());
            }
            return _charaCfg;
        }
    }
    public CharaCardInfo(int cardId)
    {
        this.CardId = cardId;
    }
    public void UpdateCardId(int cardId)
    {
        this.CardId = cardId;
    }
    public string GetCardName()
    {
        string name = CharaCfg.name;
        if (!CardCfg.cardName.Equals(string.Empty))
        {
            name = $"{name}.{CardCfg.cardName}";
        }
        return name;
    }
    public CharaCardSaveData GetSaveData()
    {
        CharaCardSaveData data = new CharaCardSaveData();
        data.cardId = CardId;
        data.cardStar = star;
        return data;
    }
}

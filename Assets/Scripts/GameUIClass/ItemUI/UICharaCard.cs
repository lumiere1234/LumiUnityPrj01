using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICharaCard : MonoBehaviour
{
    [SerializeField] private Image ImgBg;
    [SerializeField] private Image ImgFrame;
    [SerializeField] private Image ImgIcon;
    [SerializeField] private UIStarItem stars;
    [SerializeField] private TMPro.TMP_Text LblName;
    [SerializeField] private TMPro.TMP_Text LblType;
    [SerializeField] private Button BtnCard;

    CharaCardInfo cardInfo;
    private void Awake()
    {
        BtnCard.onClick.AddListener(OnClickBtnChara);
    }
    public void SetCardInfo(CharaCardInfo info)
    {
        cardInfo = info;

        RefreshCard();
    }
    private void RefreshCard()
    {
        if (cardInfo == null)
        {
            RefreshBlankCard();
            return;
        }

        CharaCardCfg cardCfg = cardInfo.CardCfg;
        CharacterCfg charaCfg = cardInfo.CharaCfg;
        LblName.text = cardInfo.GetCardName();
        CardTypeCfg typeCfg = GameConfigDataBase.GetConfigData<CardTypeCfg>(cardCfg.cardType.ToString());
        LblType.text = typeCfg.typeSingleName;
        ImgIcon.SetSprite(charaCfg.headIcon);
        ImgBg.SetSprite(GlobalDef.GetQualityBGStr(cardCfg.rare));
        ImgFrame.color = Color.clear;

        stars.RefreshStarFive(cardInfo.star, true);
    }
    private void RefreshBlankCard()
    {
        LblName.text = string.Empty;
        LblType.text = string.Empty;
        ImgIcon.SetSprite("Img_StarBlank01.png");
        ImgBg.SetSprite(GlobalDef.GetQualityBGStr(1));
        ImgFrame.color = Color.clear;

        stars.RefreshStarFive(0, true);
    }
    private void OnClickBtnChara()
    {
        Debug.Log($"Click Character: {cardInfo.CardId}");
    }
}

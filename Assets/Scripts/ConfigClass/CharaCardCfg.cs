// ExcelName: CharaCard.xlsx
using UnityEngine;
using System.Collections;

public partial class CharaCardCfg: GameConfigDataBase
{
	public int sn;
	public int character;
	public string cardName;
	public int rare;
	public int cardType;
	protected override string getFilePath()
	{
		return "CharaCardCfg";
	}
}

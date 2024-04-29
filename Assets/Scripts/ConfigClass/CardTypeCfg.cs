// ExcelName: CharaCard.xlsx
using UnityEngine;
using System.Collections;

public partial class CardTypeCfg: GameConfigDataBase
{
	public int sn;
	public string typeName;
	public string typeSingleName;
	protected override string getFilePath()
	{
		return "CardTypeCfg";
	}
}

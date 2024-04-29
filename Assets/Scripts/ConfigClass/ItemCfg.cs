// ExcelName: Item.xlsx
using UnityEngine;
using System.Collections;

public partial class ItemCfg: GameConfigDataBase
{
	public int sn;
	public string name;
	public string desc;
	public int itemType;
	public string useFunc;
	protected override string getFilePath()
	{
		return "ItemCfg";
	}
}

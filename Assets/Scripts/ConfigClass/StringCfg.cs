// ExcelName: String.xlsx
using UnityEngine;
using System.Collections;

public partial class StringCfg: GameConfigDataBase
{
	public int sn;
	public string str;
	protected override string getFilePath()
	{
		return "StringCfg";
	}
}

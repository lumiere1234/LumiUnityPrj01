// ExcelName: MyFirstConfig.xlsx
using UnityEngine;
using System.Collections;

public partial class LastCfg: GameConfigDataBase
{
	public int sn;
	public string name;
	public string mainImg;
	public int level;
	public int Atk;
	public int Def;
	protected override string getFilePath()
	{
		return "LastCfg";
	}
}

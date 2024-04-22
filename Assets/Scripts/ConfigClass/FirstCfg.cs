// ExcelName: MyFirstConfig.xlsx
using UnityEngine;
using System.Collections;

public partial class FirstCfg: GameConfigDataBase
{
	public int sn;
	public string desc;
	public string path;
	public string mainImg;
	public int level;
	protected override string getFilePath()
	{
		return "FirstCfg";
	}
}

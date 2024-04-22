// ExcelName: MyFirstConfig.xlsx
using UnityEngine;
using System.Collections;

public partial class SecondCfg: GameConfigDataBase
{
	public int sn;
	public string path;
	public string mainImg;
	public int level;
	protected override string getFilePath()
	{
		return "SecondCfg";
	}
}

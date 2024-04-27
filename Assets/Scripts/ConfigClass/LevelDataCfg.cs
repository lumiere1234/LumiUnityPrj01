// ExcelName: LevelData.xlsx
using UnityEngine;
using System.Collections;

public partial class LevelDataCfg: GameConfigDataBase
{
	public int sn;
	public string levelName;
	protected override string getFilePath()
	{
		return "LevelDataCfg";
	}
}

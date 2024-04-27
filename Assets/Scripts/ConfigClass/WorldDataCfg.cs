// ExcelName: LevelData.xlsx
using UnityEngine;
using System.Collections;

public partial class WorldDataCfg: GameConfigDataBase
{
	public int sn;
	public string worldName;
	public int difficulty;
	public string sceneName;
	public int worldType;
	protected override string getFilePath()
	{
		return "WorldDataCfg";
	}
}

// ExcelName: MySecondConfig.xlsx
using UnityEngine;
using System.Collections;

public partial class LumiTestConfig: GameConfigDataBase
{
	public int sn;
	public string desc;
	public string path;
	public string mainImg;
	public int level;
	public float[] position;
	protected override string getFilePath()
	{
		return "LumiTestConfig";
	}
}

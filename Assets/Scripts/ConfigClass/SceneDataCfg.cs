// ExcelName: Scene.xlsx
using UnityEngine;
using System.Collections;

public partial class SceneDataCfg: GameConfigDataBase
{
	public string sn;
	public string desc;
	public string path;
	public string mainImg;
	public int levelLimit;
	protected override string getFilePath()
	{
		return "SceneDataCfg";
	}
}

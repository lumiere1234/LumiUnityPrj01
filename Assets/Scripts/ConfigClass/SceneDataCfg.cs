// ExcelName: Scene.xlsx
using UnityEngine;
using System.Collections;

public partial class SceneDataCfg: GameConfigDataBase
{
	public string sn;
	public string desc;
	public string path;
	public string mainImg;
	public bool bNoBGM;
	public bool bForceBGM;
	public string sceneBGM;
	public int levelLimit;
	public bool bLoading;
	public int loadingFactor;
	public string loadingAtlas;
	protected override string getFilePath()
	{
		return "SceneDataCfg";
	}
}

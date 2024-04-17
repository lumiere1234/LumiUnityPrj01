using UnityEngine;
using System.Collections;

public partial class SceneDataCfg: GameConfigDataBase
{
	public string sn; // MainScene
	public string desc; // 游戏主场景
	public string path; // AssetPath/GameRes/Scene/MainScene.unity
	public string mainImg; // 
	public int levelLimit; // 0
	protected override string getFilePath()
	{
		return "SceneDataCfg";
	}
}

using UnityEngine;
using System.Collections;

public partial class SceneDataCfg: GameConfigDataBase
{
	public string sn; // sceneName
	public string desc; // 描述
	public string path; // 路径
	public string mainImg; // 图片
	public int levelLimit; // 等级限制
	protected override string getFilePath()
	{
		return "SceneDataCfg";
	}
}

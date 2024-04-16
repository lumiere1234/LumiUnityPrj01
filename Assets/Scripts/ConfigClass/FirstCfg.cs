using UnityEngine;
using System.Collections;

public partial class FirstCfg: GameConfigDataBase
{
	public int sn; // ID
	public string desc; // 描述
	public string path; // 路径
	public string mainImg; // 图片
	public int level; // 等级
	protected override string getFilePath()
	{
		return "FirstCfg";
	}
}

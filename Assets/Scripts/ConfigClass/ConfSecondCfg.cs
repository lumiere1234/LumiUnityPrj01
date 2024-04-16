using UnityEngine;
using System.Collections;

public  partial class ConfSecondCfg: GameConfigDataBase
{
	public int sn; // ID
	public string path; // 路径
	public string mainImg; // 图片
	public int level; // 等级
	protected override string getFilePath()
	{
		return "SecondCfg";
	}
}

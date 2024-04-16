using UnityEngine;
using System.Collections;

public partial class LastCfg: GameConfigDataBase
{
	public int sn; // ID
	public string name; // 名称
	public string mainImg; // 图片
	public int level; // 等级
	public int Atk; // 攻击力
	public int Def; // 防御力
	protected override string getFilePath()
	{
		return "LastCfg";
	}
}

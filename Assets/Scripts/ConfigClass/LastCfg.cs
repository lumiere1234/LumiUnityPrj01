using UnityEngine;
using System.Collections;

public partial class LastCfg: GameConfigDataBase
{
	public int sn; // 1
	public string name; // AssetPath
	public string mainImg; // main.png
	public int level; // 31
	public int Atk; // 24
	public int Def; // 26
	protected override string getFilePath()
	{
		return "LastCfg";
	}
}

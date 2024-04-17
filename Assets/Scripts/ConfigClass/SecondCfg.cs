using UnityEngine;
using System.Collections;

public partial class SecondCfg: GameConfigDataBase
{
	public int sn; // 1
	public string path; // AssetPath
	public string mainImg; // main.png
	public int level; // 31
	protected override string getFilePath()
	{
		return "SecondCfg";
	}
}

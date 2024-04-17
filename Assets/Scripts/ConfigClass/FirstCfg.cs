using UnityEngine;
using System.Collections;

public partial class FirstCfg: GameConfigDataBase
{
	public int sn; // 1
	public string desc; // this is 
	public string path; // AssetPath
	public string mainImg; // main.png
	public int level; // 3
	protected override string getFilePath()
	{
		return "FirstCfg";
	}
}

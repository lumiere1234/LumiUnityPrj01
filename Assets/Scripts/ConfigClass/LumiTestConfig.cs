using UnityEngine;
using System.Collections;

public partial class LumiTestConfig: GameConfigDataBase
{
	public int sn; // 13
	public string desc; // this is 
	public string path; // AssetPath
	public string mainImg; // main.png
	public int level; // 3
	public float[] position; // 12,14, 223
	protected override string getFilePath()
	{
		return "LumiTestConfig";
	}
}

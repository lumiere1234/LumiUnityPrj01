using UnityEngine;
using System.Collections;

public partial class GUIConfig: GameConfigDataBase
{
	public string sn; // UI-MainPlayer
	public string path; // Assets/GameRes/GameUI/UI-MainPlayer.prefab
	public int sortingType; // 1
	protected override string getFilePath()
	{
		return "GUIConfig";
	}
}

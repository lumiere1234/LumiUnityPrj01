// ExcelName: Character.xlsx
using UnityEngine;
using System.Collections;

public partial class CharacterCfg: GameConfigDataBase
{
	public int sn;
	public string name;
	public string headIcon;
	public string fullIcon;
	public string dialogBigIcon;
	public string dialogSmallIcon;
	protected override string getFilePath()
	{
		return "CharacterCfg";
	}
}

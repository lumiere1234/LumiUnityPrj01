// ExcelName: Gui.xlsx
using UnityEngine;
using System.Collections;

public partial class GUIConfig: GameConfigDataBase
{
	public string sn;
	public string path;
	public int sortingType;
	public double destroyTime;
	protected override string getFilePath()
	{
		return "GUIConfig";
	}
}

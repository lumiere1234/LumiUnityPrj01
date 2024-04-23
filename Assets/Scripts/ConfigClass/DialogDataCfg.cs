// ExcelName: DialogData.xlsx
using UnityEngine;
using System.Collections;

public partial class DialogDataCfg: GameConfigDataBase
{
	public int sn;
	public int character;
	public string dialogStr;
	public int nextDialog;
	public float autoTime;
	protected override string getFilePath()
	{
		return "DialogDataCfg";
	}
}

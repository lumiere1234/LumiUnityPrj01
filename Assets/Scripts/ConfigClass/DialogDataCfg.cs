// ExcelName: DialogData.xlsx
using UnityEngine;
using System.Collections;

public partial class DialogDataCfg: GameConfigDataBase
{
	public int sn;
	public string dialogStr;
	public int speakerType;
	public int character;
	public int nextDialog;
	public float autoTime;
	public string action;
	protected override string getFilePath()
	{
		return "DialogDataCfg";
	}
}

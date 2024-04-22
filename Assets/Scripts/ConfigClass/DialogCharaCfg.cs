// ExcelName: Character.xlsx
using UnityEngine;
using System.Collections;

public partial class DialogCharaCfg: GameConfigDataBase
{
	public int sn;
	public string name;
	public string iconName;
	protected override string getFilePath()
	{
		return "DialogCharaCfg";
	}
}

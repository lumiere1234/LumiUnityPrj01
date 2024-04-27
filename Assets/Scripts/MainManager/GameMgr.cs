using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : SingletonAutoMono<GameMgr>
{
    public void Initial()
    {

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TipsMgr.Instance.ShowConfirm(StringDef.TipsLeaveGameStr, null, () =>
            {
                Debug.Log("Lumiere leave Game");
                Application.Quit();
            });
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            TipsMgr.Instance.ShowConfirm(StringDef.TipsTitleDefaultStr, "lumiere", () =>
            {
                Debug.Log("First");
                Application.Quit();
            });
        }
    }
}

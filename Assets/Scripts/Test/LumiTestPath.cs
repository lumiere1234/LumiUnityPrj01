using CoreManager;
using System.IO;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LumiTestPath : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text txt_Path1;
    [SerializeField] private TMPro.TMP_Text txt_Path2;
    [SerializeField] private TMPro.TMP_Text txt_Path3;
    [SerializeField] private Button btn_Exit;

    // Start is called before the first frame update
    void Start()
    {
        txt_Path1.text = Application.dataPath;
        txt_Path2.text = Application.streamingAssetsPath;
        txt_Path3.text = PathDefine.AssetBundlePath;

        btn_Exit.onClick.AddListener(OnClickBtnExit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnClickBtnExit()
    {
        Application.Quit();
    }
}

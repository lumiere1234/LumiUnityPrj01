using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestSave : MonoBehaviour
{
    [SerializeField] Button btn1;
    [SerializeField] Button btn2;
    [SerializeField] TMPro.TMP_Text LblScore1;
    [SerializeField] TMPro.TMP_Text LblScore2;
    [SerializeField] Button BtnSave;
    [SerializeField] Button BtnLoad;

    private void Awake()
    {
        BtnSave.onClick.AddListener(OnClickBtnSave);
        BtnLoad.onClick.AddListener(OnClickBtnLoad);
        btn1.onClick.AddListener(OnClickBtn1);
        btn2.onClick.AddListener(OnClickBtn2);

        EventMgr.Instance.Register(EventDef.Default, OnUpdateEvent);
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        LblScore1.text = TestSaveManager.Instance.Score01.ToString();
        LblScore2.text = TestSaveManager.Instance.Score02.ToString();
    }
    void OnClickBtn1()
    {
        TestSaveManager.Instance.Score01++;
        UpdateUI();
    }
    void OnClickBtn2()
    {
        TestSaveManager.Instance.Score02++;
        UpdateUI();
    }
    void OnClickBtnSave()
    {
        TestSaveManager.Instance.DoSaveData();
    }
    void OnClickBtnLoad()
    {
        TestSaveManager.Instance.DoLoadData();
    }

    void OnUpdateEvent(params object[] args)
    {
        UpdateUI();
    }

}

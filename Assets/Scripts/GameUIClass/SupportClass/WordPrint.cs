using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TMPro.TMP_Text))]
public class WordPrint : MonoBehaviour
{
    private float speed => GameTextSpeedDef.GetCurTextSpeed();
    private int MaxCount = 0;
    private int CurIntCount = 0;
    private float CurCount = 0;
    private TMPro.TMP_Text _text;
    private TMPro.TMP_Text m_Text
    {
        get
        {
            if(_text == null)
            {
                _text = GetComponent<TMPro.TMP_Text>();
            }
            return _text;
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (CurIntCount < MaxCount)
        {
            CurCount += speed * Time.deltaTime;
            if (CurIntCount < CurCount)
            {
                CurIntCount = (int)CurCount + 1;
                m_Text.maxVisibleCharacters = CurIntCount;
            }
        }
    }

    public void SetPrintTxt(string str)
    {
        m_Text.maxVisibleCharacters = 0;
        m_Text.text = str;
        CurCount = 0;
        MaxCount = str.Length;
        CurIntCount = 0;
    }
}

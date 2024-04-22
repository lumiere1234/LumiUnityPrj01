using CoreManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LumiImage : MonoBehaviour
{
    [SerializeField] private Image imgBG;
    [SerializeField] private TMPro.TMP_Text txtCount;
    private int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        InitImage();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.A))
        {
            InitImage();
        }
    }

    void InitImage()
    {
        string imgStr = "BG01.jpg";
        Sprite sprite = ResManager.GetInstance().LoadSprite(imgStr);
        imgBG.sprite = sprite;

        txtCount.text = $"Lumiere TExt : {count++}";
    }
}

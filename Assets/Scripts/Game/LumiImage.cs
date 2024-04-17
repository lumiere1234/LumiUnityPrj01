using CoreManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LumiImage : MonoBehaviour
{
    [SerializeField] private Image imgBG;
    // Start is called before the first frame update
    void Start()
    {
        InitImage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitImage()
    {
        string imgStr = "BG01";
        Sprite sprite = ResManager.GetInstance().LoadSprite(imgStr);
        imgBG.sprite = sprite;
    }
}

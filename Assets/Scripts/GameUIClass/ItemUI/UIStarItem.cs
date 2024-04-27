using CoreManager;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStarItem : MonoBehaviour
{
    [SerializeField] private Image[] ImgBgList;
    [SerializeField] private Image[] ImgStarList;

    private int maxBlank = 0;
    private List<string> spriteNames = new List<string>();

    public void RefreshStarTen(int star, bool showBlank = true)
    {
        spriteNames.Clear();
        spriteNames.Capacity = 10;
        maxBlank = Mathf.Max(1, showBlank ? 5 : (star + 1) / 2);
        
        for (int i = 0; i < (star + 1) / 2; i++)
        {
            if (i == 0 && star % 2 == 1)
                spriteNames.Add(ImageDef.ImgStarYH);
            else
                spriteNames.Add(ImageDef.ImgStarY);
        }

        DoRefreshStarList();
    }
    public void RefreshStarFive(int star, bool showBlank = true)
    {
        spriteNames.Clear();
        spriteNames.Capacity = 5;
        maxBlank = Mathf.Max(1, showBlank ? 5 : star);

        for (int i = 0; i < star; i++)
        {
            spriteNames.Add(ImageDef.ImgStarY);
        }

        DoRefreshStarList();
    }
    private void DoRefreshStarList()
    {
        int starCount = spriteNames.Count;
        for (int i = 0; i < 5; i++)
        {
            // bg
            ImgBgList[i].gameObject.SetActive(i < maxBlank);
            // star
            ImgStarList[i].gameObject.SetActive(i < starCount);
            if (i < starCount)
            {
                ImgStarList[i].SetSprite(spriteNames[i]);
            }
        }
    }
}

using CoreManager;
using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;
using UnityEngine.UIElements;

public class Lumiere {
    public int lumi1 = 0;
    public string lumiStr = string.Empty;
}
public class LumiereGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        //TestAtlas();
        LoadMainScene();
        //LoadUI();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    void TestAtlas()
    {
        string assetKey = "Assets/GameRes/ImgAtlas/ImgScreen.spriteatlasv2";
        SpriteAtlas atlas = ResManager.GetInstance().GetAsset(assetKey, typeof(SpriteAtlas)) as SpriteAtlas;
        if (atlas != null)
        {
            Sprite[] sss = new Sprite[atlas.spriteCount];
            atlas.GetSprites(sss);
            foreach(var sprite in sss)
            {
                Debug.Log($"{sprite.name}");
            }
        }

        Sprite target = atlas.GetSprite("BG01");
        if (target != null)
        {   

        }
    }
    
    void StartGameTest()
    {
        FirstCfg data = GameConfigDataBase.GetConfigData<FirstCfg>("4");
        if (data != null)
        {
            Debug.Log($"Lumiere get Data : {data.mainImg}");
        }
        string cubePath = data.path;
        GameObject go = ResManager.GetInstance().GetAsset(cubePath, typeof(GameObject)) as GameObject; 
        var instance = Instantiate(go);
        instance.transform.parent = transform;

        LumiTestConfig testCfg = GameConfigDataBase.GetConfigData<LumiTestConfig>("13");
        if(testCfg != null ) 
        {
            instance.transform.position = testCfg.position.ToVector3();
        }

        GameObject go2;
        ResManager.GetInstance().GetAssetAsync(cubePath, typeof(GameObject), (obj) =>
        {
            go2 = obj as GameObject;
            var instance2 = Instantiate(go2);
            instance2.transform.parent = transform;
            instance2.name = "Lumiere Obj";
            instance2.transform.position = Vector3.one * 3;
        });

        //load main Scene
        //LoadMainScene();
    }

    void LoadMainScene()
    {
        SceneMgr.GetInstance().LoadScene(SceneDef.MainScene);
    }
}

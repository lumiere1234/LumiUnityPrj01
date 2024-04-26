using CoreManager;
using UnityEngine;

public class InputMgr : SingletonAutoMono<InputMgr>
{
    public void Initial()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            GameObject go = ResManager.Instance.GetAsset("NewCube.prefab", typeof(GameObject)) as GameObject;
            if (go != null) { 
                GameObject lumiGo = GameObject.Instantiate(go);
                lumiGo.name = "Lumiere Go";
            }

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoreManager
{
    public class CoreResource
    {
        public ResType ResType { get; set; }
        public string AssetKey { get; set; }
        public Object Asset { get; set; }
        public int RefCount { get; set; }

        public CoreResource(ResType resType, string assetKey, Object asset)
        {
            this.ResType = resType;
            this.AssetKey = assetKey;
            this.Asset = asset;
            RefCount = 0;
        }

        public void Reference()
        {
            RefCount++;
        }
        public void Release()
        {
            RefCount--;
        }
        public void Destroy()
        {

        }
    }
}

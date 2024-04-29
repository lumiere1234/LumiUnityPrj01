using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class LumiScrollItem : MonoBehaviour
    {
        private RectTransform _curRectT;
        public RectTransform CurrentRect
        {
            get 
            {
                if(_curRectT == null)
                    _curRectT = GetComponent<RectTransform>();
                return _curRectT;
            }
        }
        public float Height => CurrentRect.rect.height;
        public float Width => CurrentRect.rect.width;
        public float HorizonPos => CurrentRect.localPosition.y;
        public float VerticalPos => CurrentRect.localPosition.x;
        public bool bInitial = false; // 是否被赋值过
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void RescaleScrollGridItem(Vector2 cellSize)
        { 
            float factorX = cellSize.x / Width;
            float factorY = cellSize.y / Height;
            float scale = Mathf.Min(factorY, factorX);
            transform.localScale = Vector3.one * scale;
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
    public class LumiScrollList : ScrollRect
    {
        [SerializeField] private List<LumiScrollItem> m_ScrollItems = new List<LumiScrollItem>();
        [SerializeField] private int m_Padding = 0;
        public bool bHorizontal => horizontal;
        private int viewportLength => bHorizontal ? (int)viewport.rect.width : (int)viewport.rect.height;
        private float contentTarget => bHorizontal ? content.anchoredPosition.x : content.anchoredPosition.y;
        private List<int> itemTypeList = new List<int>();
        private Dictionary<int, Stack<LumiScrollItem>> scrollItemPool = new Dictionary<int, Stack<LumiScrollItem>>();
        public List<LumiScrollItem> ScrollItems
        {
            get {
                if (m_ScrollItems == null) m_ScrollItems = new List<LumiScrollItem>();
                return m_ScrollItems;
            }
            set { m_ScrollItems = value; }
        }
        public int Padding
        {
            get { return m_Padding; }
            set { m_Padding = value; }
        }
        public LumiScrollItem GetMetaItem(int index = 0)
        {
            return ScrollItems?[index];
        }
        public void AddMetaItem(LumiScrollItem item)
        {
            ScrollItems.Add(item);
        }
        public void SetPadding(int _padding)
        {
            Padding = _padding;
        }

        int m_headIndex = 0, m_tailIndex = 0; // 头尾滑动列表id
        Action<LumiScrollItem, int> m_setInfoCallback; // 设置UI内容的回调
        private List<LumiScrollItem> m_itemList = new List<LumiScrollItem>(); // 当前Item列表
        private int _currentLength = 0;
        private int CurrentLength => _currentLength;
        private Vector3 NewCurrentPos => bHorizontal ? new Vector3(CurrentLength, 0, 0) : new Vector3(0, -CurrentLength, 0);
        private Vector2 GetNewAnchorPos(int pos)
        {
            return bHorizontal ? new Vector2(pos, 0) : new Vector2(0, -pos);
        }
        bool isOverLength => contentTarget + CurrentLength > viewportLength;
        private int GetItemLength(LumiScrollItem item)
        {
            return (int)(bHorizontal ? item.Width : item.Height);
        }
        public void ResetList()
        {
            m_headIndex = 0;
            m_tailIndex = 0;
            content.anchoredPosition = Vector2.zero;
            _currentLength = 0;
            itemTypeList.Clear();
        }
        public void SetScrollListUpdateFunc(Action<LumiScrollItem, int> callback)
        {
            m_setInfoCallback = callback;
        }
        public void DoForceUpdate(bool toTop = false)
        {
            if (toTop) {
                m_headIndex = 0;
            }
            UpdateContent();
        }
        int tempId = 0;
        LumiScrollItem GetScrollItem(int index)
        {
            LumiScrollItem target = null;
            if (scrollItemPool.ContainsKey(index))
            {
                var itemList = scrollItemPool[index];
                if (itemList.Count > 0)
                    target = itemList.Pop();
            }
            if (target == null)
            {
                GameObject go = Instantiate(m_ScrollItems[index].gameObject);
                go.transform.SetParent(content.transform, false);
                target = go.GetComponent<LumiScrollItem>();
                go.name = $"{go.name}{tempId++}";
            }
            return target;
        }
        private Vector2 GetNewSizeDelta(Vector2 data)
        {
            if (bHorizontal)
                data.x = _currentLength;
            else
                data.y = _currentLength;
            return data;
        }
        private Vector2 GetNewSizeDelta(Vector2 data, int length)
        {
            if (bHorizontal)
                data.x = length;
            else
                data.y = length;
            return data;
        }
        private Vector2 GetNewSizeDeltaOffset(Vector2 data, int offset)
        {
            if (bHorizontal)
                data.x = data.x + offset;
            else
                data.y = data.y + offset;
            return data;
        }
        public void AddItems(int index = 0, int count = 1)
        {
            for (int i = 0; i < count; i++)
            {
                itemTypeList.Add(index);

                if (!isOverLength)
                {
                    _currentLength += m_itemList.Count > 0 ? Padding : 0;
                    var item = GetScrollItem(index);
                    item.CurrentRect.anchoredPosition = NewCurrentPos;
                    _currentLength += GetItemLength(item);
                    m_itemList.Add(item);

                    content.sizeDelta = GetNewSizeDelta(content.sizeDelta);
                }
            }
        }
        /// <summary>
        /// 重新定位item
        /// </summary>
        private void RelocateItems()
        {
            _currentLength = 0;
            for (int i = 0; i < m_itemList.Count; i++)
            {
                _currentLength += i > 0 ? Padding : 0;
                var item = m_itemList[i];
                item.CurrentRect.anchoredPosition = NewCurrentPos;
                _currentLength += GetItemLength(item);
            }
            content.sizeDelta = GetNewSizeDelta(content.sizeDelta);
            UpdateContent();
        }
        private void RelocateItemTail()
        {
            _currentLength += m_itemList.Count > 1 ? Padding : 0;
            var item = m_itemList[m_itemList.Count - 1];
            item.CurrentRect.anchoredPosition = NewCurrentPos;
            _currentLength += GetItemLength(item);
        }
        // 根据首尾编号更新列表
        private void UpdateContent()
        {
            for (int i = 0; i < m_itemList.Count; i++)
            {
                var curItem = m_itemList[i];
                if (!curItem.bInitial)
                {
                    m_setInfoCallback(curItem, m_headIndex + i);
                    m_itemList[i].gameObject.SetActive(true);
                }
            }
        }

        public void ScrollToBegin()
        {
            JumpToItem(0);
        }

        public void ScrollToEnd()
        {
            JumpToItem(itemTypeList.Count - 1);
        }

        public void JumpToItem(int index)
        {
            if (itemTypeList.Count <= index) { return; }

            RecycleAllItems();
            m_headIndex = index;

            for (int i = index; i < itemTypeList.Count; i++)
            {
                if (!isOverLength)
                {
                    int targetType = itemTypeList[i];
                    var go = GetScrollItem(targetType);
                    _currentLength += m_itemList.Count > 0 ? Padding : 0;
                    LumiScrollItem item = go.GetComponent<LumiScrollItem>();
                    _currentLength += GetItemLength(item);
                    m_itemList.Add(item);
                }
            }
            content.anchoredPosition = Vector3.zero;
            RelocateItems();
        }
        private Vector2 diffAnchorPos = Vector2.zero;
        public override void OnDrag(PointerEventData eventData)
        {
            m_ContentStartPosition += diffAnchorPos;
            base.OnDrag(eventData);
        }

        public void Update()
        {

            if (Input.GetKey(KeyCode.K))
            {
                content.anchoredPosition -= new Vector2(0, 1);
            }

        }
        private void AddItemToPool(LumiScrollItem item, int type)
        {
            item.bInitial = false;
            item.gameObject.SetActive(false);

            if (!scrollItemPool.ContainsKey(type))
            {
                scrollItemPool.Add(type, new Stack<LumiScrollItem>());
            }
            scrollItemPool[type].Push(item);
        }
        private void RecycleAllItems()
        {
            for (int i = 0; i < m_itemList.Count; i++)
            {
                AddItemToPool(m_itemList[i], itemTypeList[m_headIndex + i]);
            }
            m_itemList.Clear();
        }
        private bool CheckHeadCell()
        {
            if ((!bHorizontal && contentTarget < factor)
                || (bHorizontal && contentTarget + factor < 0))
            {
                // do insert
                if (m_headIndex == 0)
                    return false;
                else
                {
                    int targetId = m_headIndex - 1;
                    int targetType = itemTypeList[targetId];
                    var go = GetScrollItem(targetType);
                    LumiScrollItem item = go.GetComponent<LumiScrollItem>();
                    m_itemList.Insert(0, item);
                    m_headIndex = targetId;
                    go.gameObject.SetActive(false);

                    content.anchoredPosition -= GetNewAnchorPos(GetItemLength(item) + Padding);
                    return true;
                }
            }
            if (m_itemList.Count == 0) return false;
            // need remove
            int firstheight = (int)m_itemList[0].Height;
            if ((!bHorizontal && contentTarget > firstheight + Padding + factorRemove)
                || (bHorizontal && contentTarget + firstheight + Padding + factorRemove < 0))
            {
                var item = m_itemList[0];
                content.anchoredPosition += GetNewAnchorPos(GetItemLength(item) + Padding);

                AddItemToPool(m_itemList[0], itemTypeList[m_headIndex]);
                m_headIndex++;
                m_itemList.RemoveAt(0);

                return true;
            }
            return false;
        }
        private bool CheckTailCell()
        {
            if ((!bHorizontal && CurrentLength - contentTarget < viewportLength + factor)
                || (bHorizontal && CurrentLength + contentTarget < viewportLength + factor))
            {
                // 没有可以添加的
                if (m_itemList.Count + m_headIndex >= itemTypeList.Count) return false;

                int targetId = m_headIndex + m_itemList.Count;
                int targetType = itemTypeList[targetId];
                var go = GetScrollItem(targetType);
                LumiScrollItem item = go.GetComponent<LumiScrollItem>();
                m_itemList.Add(item);
                m_setInfoCallback(item, targetId);
                go.gameObject.SetActive(true);
                RelocateItemTail();
                content.sizeDelta = GetNewSizeDeltaOffset(content.sizeDelta, GetItemLength(item) + Padding);
                return true;
            }
            if (m_itemList.Count == 0) return false;
            // need remove
            int lastId = m_itemList.Count - 1;
            int lastHeight = (int)m_itemList[lastId].Height;
            if ((!bHorizontal && CurrentLength - contentTarget > viewportLength + lastHeight + Padding + factorRemove)
                || (bHorizontal && CurrentLength + contentTarget > viewportLength + factorRemove))
            {
                var item = m_itemList[lastId];
                content.sizeDelta = GetNewSizeDeltaOffset(content.sizeDelta, -GetItemLength(item) - Padding);
                _currentLength -= GetItemLength(item) + Padding;

                AddItemToPool(m_itemList[lastId], itemTypeList[lastId + m_headIndex]);
                m_itemList.RemoveAt(lastId);
                return true;
            }
            return false;
        }
        int factor = 10;
        int factorRemove = 10;
        protected override void LateUpdate()
        {
            base.LateUpdate();

            if (!content)
                return;
            float deltaTime = Time.unscaledDeltaTime;
            diffAnchorPos = Vector2.zero;
            if (deltaTime > 0.0f)
            {
                bool flag = true;
                Vector2 oldAnchoredPos = content.anchoredPosition;
                while (flag)
                {
                    flag = false;
                    // tail变化
                    if (CheckTailCell())
                    {
                        flag = true;
                    }
                    if (!flag && CheckHeadCell())
                    {
                        RelocateItems();
                        flag = true;
                    }
                }
                diffAnchorPos = content.anchoredPosition - oldAnchoredPos;
            }
        }
    }
}


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
    public class LumiScrollGrid : ScrollRect
    {
        [SerializeField] private List<LumiScrollItem> m_ScrollItems = new List<LumiScrollItem>();
        [SerializeField] private Vector2 m_Padding = Vector2.zero;
        [SerializeField] private int m_CellCount = 1; // 单位行列的格子数量
        [SerializeField] private Vector2 m_CellSize = Vector2.one * 100;
        public bool bHorizontal => horizontal;
        private int viewportLength => bHorizontal ? (int)viewport.rect.width : (int)viewport.rect.height;
        private float contentTarget => bHorizontal ? content.anchoredPosition.x : content.anchoredPosition.y;
        private List<int> itemTypeList = new List<int>();
        private Dictionary<int, Stack<LumiScrollItem>> scrollItemPool = new Dictionary<int, Stack<LumiScrollItem>>();
        public List<LumiScrollItem> ScrollItems
        {
            get
            {
                if (m_ScrollItems == null) m_ScrollItems = new List<LumiScrollItem>();
                return m_ScrollItems;
            }
            set { m_ScrollItems = value; }
        }
        public Vector2 Padding
        {
            get { return m_Padding; }
            set { m_Padding = value; }
        }
        private int MainPadding => bHorizontal ? PaddingHorizon : PaddingVertical;
        private int SubPadding => bHorizontal ? PaddingVertical : PaddingHorizon;
        public int CellCount => m_CellCount;
        public Vector2 CellSize => m_CellSize;
        private int MainCellLength => (int)(bHorizontal ? CellSize.x : CellSize.y);
        private int SubCellLength => (int)(bHorizontal ? CellSize.y : CellSize.x);
        private int PaddingHorizon => (int)Padding.x;
        private int PaddingVertical => (int)Padding.y;
        public LumiScrollItem GetMetaItem(int index = 0)
        {
            return ScrollItems?[index];
        }
        public void AddMetaItem(LumiScrollItem item)
        {
            ScrollItems.Add(item);
        }
        public void SetPadding(Vector2 _padding)
        {
            Padding = _padding;
        }
        int m_headLine = 0; // 头尾滑动列表id Line
        private int ListFirstId => m_headLine * CellCount; // 列表第一个物体id
        private Vector2 GetGridPos(int index) // 获取list中某节点的定位
        {
            int mainId = index / CellCount;
            int subId = index % CellCount;
            if (bHorizontal)
                return new Vector2((Padding.x + CellSize.x) * mainId, -(Padding.y + CellSize.y) * subId);
            else
                return new Vector2((Padding.x + CellSize.x) * subId, -(Padding.y + CellSize.y) * mainId);
        }
        Action<LumiScrollItem, int, int> m_setInfoCallback; // 设置UI内容的回调
        private List<LumiScrollItem> m_itemList = new List<LumiScrollItem>(); // 当前Item列表
        // 获取当前长度
        private int CurrentLength => ((m_itemList.Count - 1) / CellCount) * (MainCellLength + MainPadding) + MainCellLength;
        // 获取当前宽度
        private int CurrentSubLength => (CellCount - 1) * (SubCellLength + SubPadding) + SubCellLength;
        private Vector2 GetAnchorOffset(bool bInsert)
        {
            if (bHorizontal) 
                return new Vector2(bInsert ? -CellSize.x - Padding.x : CellSize.x + Padding.x, 0);
            else
                return new Vector2(0, bInsert ? CellSize.y + Padding.y : -CellSize.y - Padding.y);
        }
        private int MaxCellCount => (int)(viewportLength / (contentTarget + CurrentLength) + 2);
        //bool isOverLength => contentTarget + CurrentLength > viewportLength;
        public void ResetList()
        {
            m_headLine = 0;
            content.anchoredPosition = Vector2.zero;
            itemTypeList.Clear();
        }
        public void SetScrollGridUpdateFunc(Action<LumiScrollItem, int, int> callback)
        {
            m_setInfoCallback = callback;
        }
        public void DoForceUpdate(bool toTop = false)
        {
            if (toTop)
            {
                m_headLine = 0;
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
                target.RescaleScrollGridItem(CellSize);
            }
            return target;
        }
        private void SetCurDeltaSize()
        {
            if (bHorizontal)
                content.sizeDelta = new Vector2(CurrentLength, CurrentSubLength);
            else
                content.sizeDelta = new Vector2(CurrentSubLength, CurrentLength);
        }
        public void AddItems(int index = 0, int count = 1)
        {
            for (int i = 0; i < count; i++)
            {
                itemTypeList.Add(index);
                int mainLength = m_itemList.Count / CellCount;
                if ((mainLength < MaxCellCount) && isUpdating)
                {
                    var item = GetScrollItem(index);
                    item.CurrentRect.anchoredPosition = GetGridPos(m_itemList.Count);
                    m_itemList.Add(item);

                }
            }
            SetCurDeltaSize();
        }
        public void RefreshItems()
        {
            RecycleAllItems();
            for (int i = m_headLine * CellCount; i < itemTypeList.Count; i++)
            {
                int mainId = m_itemList.Count % CellCount;
                if ((mainId < MaxCellCount) && isUpdating)
                {
                    var item = GetScrollItem(itemTypeList[i]);
                    item.CurrentRect.anchoredPosition = GetGridPos(m_itemList.Count);
                    m_itemList.Add(item);
                }
            }
            SetCurDeltaSize();
        }
        /// <summary>
        /// 重新定位item
        /// </summary>
        private void RelocateItems()
        {
            for (int i = 0; i < m_itemList.Count; i++)
            {
                var item = m_itemList[i];
                item.CurrentRect.anchoredPosition = GetGridPos(i);
            }
            SetCurDeltaSize();
            UpdateContent();
        }
        // 根据首尾编号更新列表
        private void UpdateContent()
        {
            for (int i = 0; i < m_itemList.Count; i++)
            {
                var curItem = m_itemList[i];
                if (!curItem.bInitial)
                {
                    m_setInfoCallback?.Invoke(curItem, m_headLine * CellCount + i, itemTypeList[m_headLine * CellCount + i]);
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
            if (!isUpdating) { return; }

            RecycleAllItems();
            m_headLine = index / CellCount;
            for (int i = m_headLine * CellCount; i < itemTypeList.Count; i++)
            {
                int mainId = m_itemList.Count / CellCount;
                if (mainId < MainCellLength)
                {
                    int targetType = itemTypeList[i];
                    var item = GetScrollItem(targetType);
                    m_itemList.Add(item);
                }
            }
            content.anchoredPosition = Vector3.zero;
            RelocateItems();
        }
        private bool m_CurDragging = false;
        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);
            m_CurDragging = true;
        }
        public override void OnEndDrag(PointerEventData eventData)
        {
            base.OnEndDrag(eventData);
            m_CurDragging = false;
        }
        private bool isUpdating = false;
        protected override void OnEnable()
        {
            base.OnEnable();
            isUpdating = true;
            RefreshItems();
            UpdateContent();
        }
        protected override void OnDisable()
        {
            base.OnDisable();
            isUpdating = false;
            RecycleAllItems();
        }
        private Vector2 diffAnchorPos = Vector2.zero;
        public override void OnDrag(PointerEventData eventData)
        {
            m_ContentStartPosition += diffAnchorPos;
            base.OnDrag(eventData);
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
                AddItemToPool(m_itemList[i], itemTypeList[m_headLine + i]);
            }
            m_itemList.Clear();
        }
        private void InsertLine(int lineId, bool bHead)
        {
            List<LumiScrollItem> newList = new List<LumiScrollItem>();
            for (int i = 0; i < CellCount; i++)
            {
                int targetId = lineId * CellCount + i;
                if (itemTypeList.Count > targetId)
                {
                    var item = GetScrollItem(itemTypeList[targetId]);
                    newList.Add(item);
                    item.gameObject.SetActive(false);
                }
                else
                    break;
            }
            //Debug.Log($"lumier Get {lineId * CellCount} - {newList.Count}");
            m_itemList.InsertRange(bHead ? 0 : m_itemList.Count, newList);
        }
        private bool CheckHeadCell()
        {
            if ((!bHorizontal && contentTarget < factor)
                || (bHorizontal && contentTarget + factor > 0))
            {
                // do insert
                if (m_headLine == 0)
                    return false;
                else
                {
                    InsertLine(m_headLine - 1, true);
                    m_headLine--;
                    content.anchoredPosition += GetAnchorOffset(true);
                    return true;
                }
            }
            if (m_itemList.Count == 0) return false;
            // need remove
            if ((!bHorizontal && contentTarget > MainCellLength + MainPadding + factorRemove)
                || (bHorizontal && contentTarget + MainCellLength + MainPadding + factorRemove < 0))
            {
                for (int i = 0; i < CellCount; i++)
                {
                    var item = m_itemList[i];
                    AddItemToPool(m_itemList[i], itemTypeList[m_headLine * CellCount + i]);
                }
                m_itemList.RemoveRange(0, CellCount);
                content.anchoredPosition += GetAnchorOffset(false);
                m_headLine++;
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
                if (m_itemList.Count + m_headLine * CellCount >= itemTypeList.Count) return false;

                InsertLine(m_headLine + (m_itemList.Count - 1) / CellCount + 1, false);
                UpdateContent();
                int targetId = (m_itemList.Count - 1) / CellCount * CellCount;
                for (int i = targetId; i < m_itemList.Count; i++)
                {
                    m_itemList[i].CurrentRect.anchoredPosition = GetGridPos(i);
                }
                SetCurDeltaSize();
                return true;
            }
            if (m_itemList.Count == 0) return false;
            // need remove
            if ((!bHorizontal && CurrentLength - contentTarget > viewportLength + MainCellLength + MainPadding + factorRemove)
                || (bHorizontal && CurrentLength + contentTarget > viewportLength + MainCellLength + MainPadding + factorRemove))
            {
                int targetId = (m_itemList.Count - 1) / CellCount * CellCount;
                int removeCount = 0;
                for (int i = targetId; i < m_itemList.Count; i++)
                {
                    removeCount++;
                    AddItemToPool(m_itemList[i], itemTypeList[i + m_headLine * CellCount]);
                }
                m_itemList.RemoveRange(m_itemList.Count - removeCount, removeCount);
                SetCurDeltaSize();
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


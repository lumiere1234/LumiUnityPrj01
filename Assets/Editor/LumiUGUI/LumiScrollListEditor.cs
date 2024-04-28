using System.Collections;
using System.Collections.Generic;
using UnityEditor.AnimatedValues;
using UnityEngine;
using UnityEngine.UI;

namespace UnityEditor.UI
{
    [CustomEditor(typeof(LumiScrollList), true)]
    [CanEditMultipleObjects]
    public class LumiScrollListEditor : Editor
    {
        LumiScrollList curTarget;

        SerializedProperty m_scrollItems;
        SerializedProperty m_Content;
        SerializedProperty m_HorizontalFlag;
        SerializedProperty m_Padding;
        SerializedProperty m_MovementType;
        SerializedProperty m_Elasticity;
        SerializedProperty m_Inertia;
        SerializedProperty m_DecelerationRate;
        SerializedProperty m_ScrollSensitivity;
        SerializedProperty m_Viewport;
        SerializedProperty m_HorizontalScrollbar;
        SerializedProperty m_VerticalScrollbar;
        SerializedProperty m_HorizontalScrollbarVisibility;
        SerializedProperty m_VerticalScrollbarVisibility;
        SerializedProperty m_HorizontalScrollbarSpacing;
        SerializedProperty m_VerticalScrollbarSpacing;
        SerializedProperty m_OnValueChanged;
        AnimBool m_ShowElasticitySub;
        AnimBool m_ShowDecelerationRateSub;
        bool m_ViewportIsNotChildSub, m_HScrollbarIsNotChildSub, m_VScrollbarIsNotChildSub;
        static string s_HError = "For this visibility mode, the Viewport property and the Horizontal Scrollbar property both needs to be set to a Rect Transform that is a child to the Scroll Rect.";
        static string s_VError = "For this visibility mode, the Viewport property and the Vertical Scrollbar property both needs to be set to a Rect Transform that is a child to the Scroll Rect.";

        protected virtual void OnEnable()
        {
            curTarget = (LumiScrollList)target;
            //base.OnEnable();
            m_scrollItems = serializedObject.FindProperty("m_ScrollItems");            
            m_Content = serializedObject.FindProperty("m_Content");
            m_HorizontalFlag = serializedObject.FindProperty("m_Horizontal");
            m_Padding = serializedObject.FindProperty("m_Padding");
            m_MovementType = serializedObject.FindProperty("m_MovementType");
            m_Elasticity = serializedObject.FindProperty("m_Elasticity");
            m_Inertia = serializedObject.FindProperty("m_Inertia");
            m_DecelerationRate = serializedObject.FindProperty("m_DecelerationRate");
            m_ScrollSensitivity = serializedObject.FindProperty("m_ScrollSensitivity");
            m_Viewport = serializedObject.FindProperty("m_Viewport");
            m_HorizontalScrollbar = serializedObject.FindProperty("m_HorizontalScrollbar");
            m_VerticalScrollbar = serializedObject.FindProperty("m_VerticalScrollbar");
            m_HorizontalScrollbarVisibility = serializedObject.FindProperty("m_HorizontalScrollbarVisibility");
            m_VerticalScrollbarVisibility = serializedObject.FindProperty("m_VerticalScrollbarVisibility");
            m_HorizontalScrollbarSpacing = serializedObject.FindProperty("m_HorizontalScrollbarSpacing");
            m_VerticalScrollbarSpacing = serializedObject.FindProperty("m_VerticalScrollbarSpacing");
            m_OnValueChanged = serializedObject.FindProperty("m_OnValueChanged");

            m_ShowElasticitySub = new AnimBool(Repaint);
            m_ShowDecelerationRateSub = new AnimBool(Repaint);
            SetAnimBools(true);
        }
        protected virtual void OnDisable()
        {
            //base.OnDisable();
            m_ShowElasticitySub.valueChanged.RemoveListener(Repaint);
            m_ShowDecelerationRateSub.valueChanged.RemoveListener(Repaint);
        }
        void SetAnimBools(bool instant)
        {
            SetAnimBool(m_ShowElasticitySub, !m_MovementType.hasMultipleDifferentValues && m_MovementType.enumValueIndex == (int)ScrollRect.MovementType.Elastic, instant);
            SetAnimBool(m_ShowDecelerationRateSub, !m_Inertia.hasMultipleDifferentValues && m_Inertia.boolValue == true, instant);
        }

        void SetAnimBool(AnimBool a, bool value, bool instant)
        {
            if (instant)
                a.value = value;
            else
                a.target = value;
        }
        void CalculateCachedValues()
        {
            m_ViewportIsNotChildSub = false;
            m_HScrollbarIsNotChildSub = false;
            m_VScrollbarIsNotChildSub = false;
            if (targets.Length == 1)
            {
                Transform transform = ((ScrollRect)target).transform;
                if (m_Viewport.objectReferenceValue == null || ((RectTransform)m_Viewport.objectReferenceValue).transform.parent != transform)
                    m_ViewportIsNotChildSub = true;
                if (m_HorizontalScrollbar.objectReferenceValue == null || ((Scrollbar)m_HorizontalScrollbar.objectReferenceValue).transform.parent != transform)
                    m_HScrollbarIsNotChildSub = true;
                if (m_VerticalScrollbar.objectReferenceValue == null || ((Scrollbar)m_VerticalScrollbar.objectReferenceValue).transform.parent != transform)
                    m_VScrollbarIsNotChildSub = true;
            }
        }
        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();
            SetAnimBools(false);

            serializedObject.Update();
            // Once we have a reliable way to know if the object changed, only re-cache in that case.
            CalculateCachedValues();

            curTarget = (LumiScrollList)target;
            curTarget.vertical = !curTarget.horizontal;

            EditorGUILayout.PropertyField(m_Content);
            EditorGUILayout.PropertyField(m_scrollItems);

            EditorGUILayout.PropertyField(m_HorizontalFlag);
            EditorGUILayout.PropertyField(m_Padding);

            EditorGUILayout.PropertyField(m_MovementType);
            if (EditorGUILayout.BeginFadeGroup(m_ShowElasticitySub.faded))
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(m_Elasticity);
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.EndFadeGroup();

            EditorGUILayout.PropertyField(m_Inertia);
            if (EditorGUILayout.BeginFadeGroup(m_ShowDecelerationRateSub.faded))
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(m_DecelerationRate);
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.EndFadeGroup();

            EditorGUILayout.PropertyField(m_ScrollSensitivity);

            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(m_Viewport);

            EditorGUILayout.PropertyField(m_HorizontalScrollbar);
            if (m_HorizontalScrollbar.objectReferenceValue && !m_HorizontalScrollbar.hasMultipleDifferentValues)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(m_HorizontalScrollbarVisibility, EditorGUIUtility.TrTextContent("Visibility"));

                if ((ScrollRect.ScrollbarVisibility)m_HorizontalScrollbarVisibility.enumValueIndex == ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport
                    && !m_HorizontalScrollbarVisibility.hasMultipleDifferentValues)
                {
                    if (m_ViewportIsNotChildSub || m_HScrollbarIsNotChildSub)
                        EditorGUILayout.HelpBox(s_HError, MessageType.Error);
                    EditorGUILayout.PropertyField(m_HorizontalScrollbarSpacing, EditorGUIUtility.TrTextContent("Spacing"));
                }

                EditorGUI.indentLevel--;
            }

            EditorGUILayout.PropertyField(m_VerticalScrollbar);
            if (m_VerticalScrollbar.objectReferenceValue && !m_VerticalScrollbar.hasMultipleDifferentValues)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(m_VerticalScrollbarVisibility, EditorGUIUtility.TrTextContent("Visibility"));

                if ((ScrollRect.ScrollbarVisibility)m_VerticalScrollbarVisibility.enumValueIndex == ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport
                    && !m_VerticalScrollbarVisibility.hasMultipleDifferentValues)
                {
                    if (m_ViewportIsNotChildSub || m_VScrollbarIsNotChildSub)
                        EditorGUILayout.HelpBox(s_VError, MessageType.Error);
                    EditorGUILayout.PropertyField(m_VerticalScrollbarSpacing, EditorGUIUtility.TrTextContent("Spacing"));
                }

                EditorGUI.indentLevel--;
            }

            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(m_OnValueChanged);

            serializedObject.ApplyModifiedProperties();
        }
    }
}


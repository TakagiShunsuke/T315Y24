/*=====
<SerializeNamingWithEnum.cs>
└作成者：takagi

＞内容
Enumをシリアライズフィールド上の要素名として表示するもの
※ネットのもののコピペ。後で修正・コメント付け

＞更新履歴
__Y24
_M06
D
07:プログラム作成:takagi
21:リファクタリング:takagi
=====*/

using System;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class CSerializeNamingWithEnum : PropertyAttribute
{
    private string[] m_sNames;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="enumType"></param>
    public CSerializeNamingWithEnum(Type enumType) => m_sNames = Enum.GetNames(enumType);

#if UNITY_EDITOR    //エディタ使用中

    //インスペクター書き換え
    [CustomPropertyDrawer(typeof(CSerializeNamingWithEnum))] //PropertyDrawer使用
    private class Drawer : PropertyDrawer
    {
        public override void OnGUI(Rect _rect, SerializedProperty _property, GUIContent label)
        {
            var _sNames = ((CSerializeNamingWithEnum)attribute).m_sNames;
            // propertyPath returns something like hogehoge.Array.data[0]
            // so get the index from there.
            var _nIndex = int.Parse(_property.propertyPath.Split('[', ']').Where(c => !string.IsNullOrEmpty(c)).Last());
            if (_nIndex < _sNames.Length) label.text = _sNames[_nIndex];
            EditorGUI.PropertyField(_rect, _property, label, includeChildren: true);
        }

        public override float GetPropertyHeight(SerializedProperty _property, GUIContent _label)
        {
            return EditorGUI.GetPropertyHeight(_property, _label, includeChildren: true);
        }
    }
#endif
}
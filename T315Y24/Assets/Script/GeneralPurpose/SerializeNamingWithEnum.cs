using System;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class SerializeNamingWithEnum : PropertyAttribute
{
    private string[] _names;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="enumType"></param>
    public SerializeNamingWithEnum(Type enumType) => _names = Enum.GetNames(enumType);

#if UNITY_EDITOR    //�G�f�B�^�g�p��

    //�C���X�y�N�^�[��������
    [CustomPropertyDrawer(typeof(SerializeNamingWithEnum))] //PropertyDrawer�g�p
    private class Drawer : PropertyDrawer
    {
        public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
        {
            var names = ((SerializeNamingWithEnum)attribute)._names;
            // propertyPath returns something like hogehoge.Array.data[0]
            // so get the index from there.
            var index = int.Parse(property.propertyPath.Split('[', ']').Where(c => !string.IsNullOrEmpty(c)).Last());
            if (index < names.Length) label.text = names[index];
            EditorGUI.PropertyField(rect, property, label, includeChildren: true);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, includeChildren: true);
        }
    }
#endif
}
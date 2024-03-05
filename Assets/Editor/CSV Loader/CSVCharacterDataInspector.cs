// Merle Roji

using UnityEngine;
using UnityEditor;

namespace T02.Characters
{
    /// <summary>
    /// Custom inspector for the CharacterData object.
    /// </summary>
    [CustomEditor(typeof(CharacterDataManager))]
    public class CSVCharacterDataInspector : Editor
    {
        private CharacterDataManager _characterData;

        public void OnEnable()
        {
            _characterData = (CharacterDataManager)target;
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Update Character Stat Objects"))
            {
                _characterData.UpdateCharacterStatObjects();
            }

            EditorUtility.SetDirty(_characterData);
        }
    }
}

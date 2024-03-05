// Keegan Buckingham, Merle Roji

using UnityEngine;
using UnityEditor;
using T02.Characters;

namespace T02
{
    /// <summary>
    /// Loads a spreadsheet.
    /// </summary>
    public class CSVLoader : EditorWindow
    {
        [MenuItem("Tools/CSV Loader")]
        public static void ShowWindow()
        {
            GetWindow<CSVLoader>("CSV Loader");
        }

        private void OnGUI()
        {
            GUILayout.Label("Reload Character Database", EditorStyles.boldLabel);
            if (GUILayout.Button("Reload Characters"))
            {
                CharacterDataManager characterData = Resources.Load("Character Data/CharacterData") as CharacterDataManager;

                if (characterData != null)
                {
                    characterData.LoadCharacters();
                    characterData.UpdateCharacterStatObjects();
                }
                else
                {
                    Debug.LogError("ERROR: No CharacterData Asset in Resources folder!!");
                }
            }
        }
    }
}

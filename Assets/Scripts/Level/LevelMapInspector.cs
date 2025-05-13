using UnityEditor;
using UnityEngine;

namespace Level
{
    [CustomEditor(typeof(LevelManager))]
    public class LevelInspector : Editor
    {
        private bool _isEditing;
        public override void OnInspectorGUI() {
            
            if (GUILayout.Toggle(false,"Map editing mod")) _isEditing = !_isEditing;
            
            DrawDefaultInspector();
            
            if(_isEditing == false) return;

        
            LevelManager levelManager = (LevelManager)target;

            const float kLeftMargin = 40;
            const int kCellSize = 25;
            const int kPadding = 5;
            const float spacing = 100f;
        
            GUILayout.Space(kCellSize * LevelManager.XMapSize);
            
            var drawRect = new Rect(0, 0, kCellSize, kCellSize);
            for (int x = 0; x < LevelManager.XMapSize; x++)
            {
                for (int y = 0; y < LevelManager.YMapSize; y++) {
                    
                    // Calculate rect position
                    
                    drawRect.x = kLeftMargin + kCellSize * x + kPadding * x;
                    drawRect.y = spacing + kCellSize * y + kPadding * y;
                    
                    // Calculate color
                    var savedColor = GUI.color;
                    
                    //If cell is true -> Green, if sell is false and is in the map center -> yellow else default
                    GUI.color = levelManager.cellMap[x, y] ? Color.green : x == LevelManager.XMapSize/2 && y == LevelManager.YMapSize/2 ? Color.yellow : savedColor;

                    //GUI.color = levelManager.cellMap[x, y] ? Color.green : savedColor;

                    if (GUI.Button(drawRect, "")) {
                        levelManager.cellMap[x, y] ^= true;
                    }

                    GUI.color = savedColor;
                }
            }
            
            if (GUILayout.Button("Reset Map"))
            {
                levelManager.ResetMap();
            }
            
            if (GUILayout.Button("Fill Map"))
            {
                levelManager.ResetMap(true);
            }

            EditorUtility.SetDirty(levelManager);

        }
        
    }
}


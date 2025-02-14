using UnityEngine;

namespace Mouse_Escape.Scripts.Game.GameRoot.Levels
{
    [CreateAssetMenu(fileName = "Level Info", menuName = "GamePlay/ New Level Info")]
    public class LevelInfo : ScriptableObject
    {
        [SerializeField] private string levelID;
        [SerializeField] private int counter;
        [SerializeField] private Vector2Int startMousePosition;
        
        public string LevelID => levelID;
        public int Counter => counter;
        
        public Vector2Int StartMousePosition => startMousePosition;
    }
}
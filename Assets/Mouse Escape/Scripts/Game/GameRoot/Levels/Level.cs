using Mouse_Escape.Scripts.Game.Gameplay.Players;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Mouse_Escape.Scripts.Game.GameRoot.Levels
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private LevelInfo levelInfo;
        [SerializeField] private Tilemap mainTilemap;
        [SerializeField] private Tilemap obstaclesTilemap;
        [SerializeField] private Tilemap triggerTilemap;
        [SerializeField] private Player player;
        
        public int LevelCounter => levelInfo.Counter;
        
        private void Awake()
        {
            player.GetStartMousePosition(levelInfo.StartMousePosition);
        }
        
        public Player GetPlayer() => player;
        
        public Tilemap GetMainTilemap() => mainTilemap;
    }
}
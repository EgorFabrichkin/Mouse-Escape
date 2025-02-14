using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

namespace Mouse_Escape.Scripts.Game.Gameplay.Players
{
    public class Player : MonoBehaviour
    {
        public UnityEvent<Vector2Int> onMove = new();
        public UnityEvent onCounter = new();
        public UnityEvent onFinish = new();
        public UnityEvent onFail = new();
        public UnityEvent onWater = new();
        
        public Tilemap tilemap;
        public Tilemap obstacleTilemap;
        public Tilemap triggerTilemap;
        public Tilemap waterTilemap;
        
        private Vector2Int _position;
        private int _value;

        private void Start()
        {
            transform.position = GetWorldPosition(_position);
            
            onMove.AddListener(Move);
        }

        private void Move(Vector2Int direction)
        {
            var newPosition = _position + direction;

            var tile = obstacleTilemap.GetTile((Vector3Int)newPosition);
            var finishTile = triggerTilemap.GetTile((Vector3Int)newPosition);
            var waterTile = waterTilemap.GetTile((Vector3Int)newPosition);
            
            if (tile == null)
            {
                _position = newPosition;
                transform.position = GetWorldPosition(_position);
                onCounter.Invoke();
            }

            if (finishTile != null)
            {
                onFinish.Invoke();
                triggerTilemap.gameObject.SetActive(false);
            }

            if (waterTile != null)
            {
                onWater.Invoke();
            }
        }

        private Vector3 GetWorldPosition(Vector2Int gridPos)
        {
            return tilemap.GetCellCenterWorld((Vector3Int)gridPos);
        }
        
        public void GetStartMousePosition(Vector2Int startPosition) => _position = startPosition;

        public Vector2Int GetCurrentPosition() => _position;
    }
}

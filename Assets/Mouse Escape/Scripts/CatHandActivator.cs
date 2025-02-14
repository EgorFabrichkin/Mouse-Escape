using Mouse_Escape.Scripts.Game.Gameplay;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

namespace Mouse_Escape.Scripts
{
    public class CatHandActivator : MonoBehaviour
    {
        public UnityEvent<Tilemap,Vector2Int> onActivate = new ();
        
        [SerializeField] private CatHand catHand;
        
        private void Awake()
        {
            onActivate.AddListener( (tilemap, newPosition) =>
            {
                transform.position = GetWorldPosition(tilemap, newPosition);
                catHand.onActivate.Invoke();
            });
        }
        
        private Vector3 GetWorldPosition(Tilemap tilemap, Vector2Int gridPos)
        {
            return tilemap.GetCellCenterWorld((Vector3Int)gridPos);
        }
    }
}
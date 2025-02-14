using System;
using Mouse_Escape.Scripts.Game.Gameplay.Mouses.Input;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

namespace Mouse_Escape.Scripts.Game.Gameplay.Players.Input
{
    public class TouchInput : MonoBehaviour, IInput, IPointerClickHandler
    {
        [SerializeField] private Camera camera;
        
        private Tilemap _tilemap;
        private Vector2 _newMousePosition = Vector2.zero;

        private void Awake()
        {
            camera = Camera.main;
        }

        public Vector2 Position()
        {
            return _newMousePosition;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            var worldPosition = camera.ScreenToWorldPoint(eventData.position);
            var selectPosition = _tilemap.WorldToCell(worldPosition);
            
            Debug.Log(selectPosition);
            
            var convertPosition = _tilemap.GetCellCenterWorld(selectPosition);
            _newMousePosition = convertPosition;
        }
        
        public void GetTilemap(Tilemap tilemap) => _tilemap = tilemap;
    }
}
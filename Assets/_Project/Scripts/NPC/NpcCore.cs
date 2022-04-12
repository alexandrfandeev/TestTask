using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Folder;
using _Project.Scripts.Handlers;
using UnityEngine;

namespace _Project.Scripts.NPC
{
    public class NpcCore : MonoBehaviour
    {
        private Transform _ownTransform;
        private GridSystem _gridSystem;
        private Movement _movement;
    
        private Position _currentPositionInSpace;
        private Position _futurePositionInSpace;
    
        private GridCell _currentCell;
        private GridCell _futureCell;
    
        private Vector2 _targetPosition = Vector2.zero;
    
        private bool _isInSearching;
        private bool _isMovingVertically = false;
        

        private void Awake()
        {
            _ownTransform = transform;
            _movement = FindObjectOfType<Movement>();
            _gridSystem = FindObjectOfType<GridSystem>();
        }

        private void Start()
        {
            _currentCell = _gridSystem.GetClosestCell(_ownTransform.position);
            _ownTransform.position = _currentCell.Position;
        }

        private GridCell GetNext()
        {
            if (_currentCell.Neighbours.ContainsKey(_currentPosition) &&
                !_currentCell.Neighbours[_currentPosition].Occupied)
            {
                return _currentCell.Neighbours[_currentPosition];
            }

            Position currentPosition = _currentPosition;
            for (int i = 0; i < 3; i++)
            {
                currentPosition = GetNextDirection(currentPosition);
                if (_currentCell.Neighbours.ContainsKey(currentPosition) &&
                    !_currentCell.Neighbours[currentPosition].Occupied)
                {
                    return _currentCell.Neighbours[currentPosition];
                }
            }

            return null;
        }

        private Position GetNextDirection(Position currentPosition)
        {
            if (currentPosition == Position.Left) return Position.Backward;
            if (currentPosition == Position.Backward) return Position.Right;
            if (currentPosition == Position.Right) return Position.Forward;

            return Position.Left;
        }

        private Position _currentPosition;

        private IEnumerator MoveToPath()
        {
            GridCell targetCell = _gridSystem.GetClosestCell(_targetPosition);
            float xDifference = _targetPosition.x - transform.position.x;
            _currentPosition  = xDifference > 0 ? Position.Right : Position.Left;

            while (_currentCell != targetCell)
            {
               var nextCell = GetNext();
                yield return _movement.MoveTo(nextCell.Position);
                _currentCell = nextCell;
            }
        }

        private Coroutine _movePath;


        private void Update()
        {
            if (Input.GetMouseButtonDown(2))
            {
                if (_movePath != null) StopCoroutine(_movePath);
                _movePath =   StartCoroutine(MoveToPath());
            }
        }

        public void StartCalculateDirection(Vector2 targetPosition)
        {
            _targetPosition = targetPosition;
            _isMovingVertically = !_isMovingVertically;
            Vector2 playerPosition = _ownTransform.position;
            float xDifference = targetPosition.x - playerPosition.x;
            _currentCell = _gridSystem.GetClosestCell(_ownTransform.position);
        
            _futurePositionInSpace  = xDifference > 0 ? Position.Right : Position.Left;

       
        }

        private void OnSearchNextCell()
        {
            _currentCell = _futureCell;
            _futureCell = _currentCell.OnReturnCellByPlayerDirection(_currentPositionInSpace);
        }
    }
}

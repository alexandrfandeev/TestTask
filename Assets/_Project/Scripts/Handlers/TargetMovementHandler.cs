using System;
using System.Collections;
using UnityEngine;
using Random = System.Random;

public class TargetMovementHandler : MonoBehaviour
{
    [SerializeField] private Target targetPrefab;
    
    private readonly Random _rnd = new Random();
    private Target _target;
    private GridSystem _gridSystem;
    private NpcCore _npcCore;

    private void Start()
    {
        _npcCore = FindObjectOfType<NpcCore>();
        _gridSystem = GetComponent<GridSystem>();
        _target = Instantiate(targetPrefab);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1)) GetPosition();
    }

    public void GetPosition()
    {
        bool randomPosition = false;
        int index = _rnd.Next(0, _gridSystem.GridCells.Count);

        while (!randomPosition)
        {
            if (_gridSystem.IsFreeCell(index))
            {
                randomPosition = true;
                Vector2 position = _gridSystem.GridCells[index].Position;
                _target.transform.position = position;
                _npcCore.StartCalculateDirection(position);
            }

            
            index = _rnd.Next(0, _gridSystem.GridCells.Count);
        }
    }
}

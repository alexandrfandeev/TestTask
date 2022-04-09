using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(order = 0, fileName = "CellVectors", menuName = "Configs/CellVectors")]
public class CellVectors : ScriptableObject
{
    public List<CellLocalPositionInSpace> cellPositions = new List<CellLocalPositionInSpace>();
}

[Serializable] public class CellLocalPositionInSpace
{
    public Position enumLocalPosition;
    public Vector2 localPosition;
}

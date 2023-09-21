using P_HexaGame_Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

/// <summary>
/// 블럭 클래스
/// </summary>
public class Block : MonoBehaviour
{
    /// <summary>
    /// 블럭을 이루는 타일의 위치 값
    /// </summary>
    public Vector2Int[] cellPos { get; private set; }

    private void Start()
    {
        cellPos = new Vector2Int[Data.cellData.Length];

        for (int i = 0; i < cellPos.Length; i++)
        {
            // Block을 이루는 타일의 위치값을 저장
            cellPos[i] = Data.cellData[i];
        }
    }

    private void Update()
    {

    }

    /// <summary>
    /// 블럭 이동 메서드
    /// </summary>
    private void Move()
    {
        //Vector3Int newPosition = 
    }
}
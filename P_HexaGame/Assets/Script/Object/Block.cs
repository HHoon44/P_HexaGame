using P_HexaGame_Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

/// <summary>
/// �� Ŭ����
/// </summary>
public class Block : MonoBehaviour
{
    /// <summary>
    /// ���� �̷�� Ÿ���� ��ġ ��
    /// </summary>
    public Vector2Int[] cellPos { get; private set; }

    private void Start()
    {
        cellPos = new Vector2Int[Data.cellData.Length];

        for (int i = 0; i < cellPos.Length; i++)
        {
            // Block�� �̷�� Ÿ���� ��ġ���� ����
            cellPos[i] = Data.cellData[i];
        }
    }

    private void Update()
    {

    }

    /// <summary>
    /// �� �̵� �޼���
    /// </summary>
    private void Move()
    {
        //Vector3Int newPosition = 
    }
}
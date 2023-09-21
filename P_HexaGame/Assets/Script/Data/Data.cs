using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UIElements;

namespace P_HexaGame_Data
{
    /// <summary>
    /// �����͸� ���ϰ� �ִ� Ŭ����
    /// </summary>
    public static class Data
    {
        public enum TileColor
        {
            None, Blue, Cyan, Green, Orange, Purple, Red, Yellow
        }

        public static readonly Vector2Int[] cellData = 
            { new Vector2Int(0, -2), new Vector2Int(0, -1), new Vector2Int(0, -0) };
    }
}
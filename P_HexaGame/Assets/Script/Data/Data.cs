using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UIElements;

namespace P_HexaGame_Data
{
    /// <summary>
    /// 데이터를 지니고 있는 클래스
    /// </summary>
    public static class Data
    {
        public enum TileColor
        {
            None, Blue, Cyan, Green, Orange, Purple, Red, Yellow
        }

        public enum DirValue
        { 
            None, Left, Right, Down
        }

        public enum TouchValue
        { 
            None, Left, Right, Down
        }

        public static readonly Vector2Int[] cellData = 
            { new Vector2Int(0, -2), new Vector2Int(0, -1), new Vector2Int(0, -0) };
    }
}
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UIElements;

namespace P_HexaGame_Define
{
    /// <summary>
    /// 데이터를 지니고 있는 클래스
    /// </summary>
    public enum BlockColor
    {
        None, Blue, Cyan, Green, 
    }

    public enum DirData
    {
        None, Left, Right, Down
    }

    public enum BorderData
    {
        None, L_Border, R_Border
    }
}
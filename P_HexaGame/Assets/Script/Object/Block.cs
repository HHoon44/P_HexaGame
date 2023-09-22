using P_HexaGame_Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

/// <summary>
/// 블럭 클래스
/// </summary>
public class Block : MonoBehaviour
{
    private float stepTime;
    private float stepDelay = 1f;

    [SerializeField]
    private float test_y;

    [SerializeField]
    private bool isMin;


    private List<GameObject> cells;
    private GameObject l_Cell;
    private Transform l_RayPos;

    private void Start()
    {
        // 현재 존재하는 셀을 저장
        cells.Add(GetComponentInChildren<GameObject>());

        // 맨 아래 셀을 구하는 작업
        GetLastChil();

        // 셀에 존재하는 Ray Pos 활성화
        l_RayPos = l_Cell.transform.GetChild(0);
        l_RayPos.gameObject.SetActive(true);

    }

    private void Update()
    {
        if (transform.position.y == test_y && !isMin)
        {
            isMin = true;
        }

        // test
        DownChecking();


        if (Time.time >= stepTime && !isMin)
        {
            Step();
        }
    }

    /// <summary>
    /// 블럭 이동 메서드
    /// </summary>
    private void Move()
    {
        Vector3 newPos = transform.position;

        newPos.x += Vector2.down.x;
        newPos.y += Vector2.down.y;

        transform.position = newPos;
    }

    private void Step()
    {
        stepTime = Time.time + stepDelay;

        Move();
    }

    private void DownChecking()
    {
        Debug.DrawRay(transform.position, Vector3.down, Color.red);
    }

    /// <summary>
    /// 맨 아래 자식을 구하는 메서드
    /// </summary>
    private void GetLastChil()
    {
        if (gameObject.transform.childCount <= 0)
        {
            return;
        }

        // 마지막 셀을 저장
        l_Cell = cells[cells.Count - 1];
    }
}
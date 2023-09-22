using P_HexaGame_Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

/// <summary>
/// �� Ŭ����
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
        // ���� �����ϴ� ���� ����
        cells.Add(GetComponentInChildren<GameObject>());

        // �� �Ʒ� ���� ���ϴ� �۾�
        GetLastChil();

        // ���� �����ϴ� Ray Pos Ȱ��ȭ
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
    /// �� �̵� �޼���
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
    /// �� �Ʒ� �ڽ��� ���ϴ� �޼���
    /// </summary>
    private void GetLastChil()
    {
        if (gameObject.transform.childCount <= 0)
        {
            return;
        }

        // ������ ���� ����
        l_Cell = cells[cells.Count - 1];
    }
}
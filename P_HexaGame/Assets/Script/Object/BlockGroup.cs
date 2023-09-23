using P_HexaGame_Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

/// <summary>
/// �� Ŭ����
/// </summary>
public class BlockGroup : MonoBehaviour
{
    private bool isTouch;                   // �� or ���� ��Ҵ��� üũ

    [SerializeField]
    private Sprite[] blockSprites;

    [SerializeField]
    private List<Transform> blocks;         // �ڽ� ��ü�� �����ϴ� ��

    private Transform lastBlock;            // �� �׷� �� �������� �����ϴ� ��
    private Transform lastRayPos;           // ������ ���� �����ϴ� Ray ������

    private float stepTime;
    private float stepDelay = 1f;

    /// <summary>
    /// �� �׷� �ʱ�ȭ �޼���
    /// </summary>
    public void Initialize()
    {
        // ��� �ڽ� ������ ����
        // �ڽ� ������ �̹����� ����
        for (int i = 0; i < transform.childCount; i++)
        {
            blocks.Add(transform.GetChild(i));
            blocks[i].gameObject.GetComponent<SpriteRenderer>().sprite = blockSprites[Random.Range(0, blockSprites.Length)];
        }

        // �� �Ʒ� ���� ���ϴ� �۾�
        GetLastChil();

        // ���� �����ϴ� Ray Pos Ȱ��ȭ
        lastRayPos = lastBlock.transform.GetChild(0);
        lastRayPos.gameObject.SetActive(true);
    }

    private void Update()
    {
        // �Ʒ��� �� or ���� �ִ��� üũ
        DownCheck();


        if (Time.time >= stepTime && !isTouch)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log("����");
            }

            Move();
        }
    }

    /// <summary>
    /// �� �̵� �޼���
    /// </summary>
    private void Move()
    {
        Vector3 newPos = transform.position;

        // �ӽ� Ű �Է�
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("���� �̵�");

            // ���� �̵�
            newPos.x += Vector2.left.x;
            newPos.y += Vector2.left.y;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("������ �̵�");

            // ������ �̵�
            newPos.x += Vector2.right.x;
            newPos.y += Vector2.right.y;
        }
        else
        {
            newPos.x += Vector2.down.x;
            newPos.y += Vector2.down.y;
        }

        transform.position = newPos;
        stepTime = Time.time + stepDelay;
    }

    private void DownCheck()
    {
        Debug.DrawRay(lastRayPos.position + (Vector3.down / 2), Vector2.down, Color.red);

        // Layer ��ȣ
        int mask = 1 << 6 | 1 << 7;

        // ���� ���� �Ʒ��� Ray�� �߻��Ͽ� �˻�
        RaycastHit2D hit = Physics2D.Raycast(lastRayPos.position + (Vector3.down / 2), Vector2.down, -1.5f, mask);

        if (hit.collider != null)
        {
            isTouch = true;
        }
    }

    /// <summary>
    /// �� �Ʒ� �ڽ��� ���ϴ� �޼���
    /// </summary>
    private void GetLastChil()
    {
        if (transform.childCount <= 0)
        {
            return;
        }

        // ������ ���� ����
        lastBlock = blocks[blocks.Count() - 1];
    }
}
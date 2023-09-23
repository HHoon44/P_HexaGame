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
/// 블럭 클래스
/// </summary>
public class BlockGroup : MonoBehaviour
{
    private bool isTouch;                   // 벽 or 블럭에 닿았는지 체크

    [SerializeField]
    private Sprite[] blockSprites;

    [SerializeField]
    private List<Transform> blocks;         // 자식 객체로 존재하는 블럭

    private Transform lastBlock;            // 블럭 그룹 중 마지막에 존재하는 블럭
    private Transform lastRayPos;           // 마지막 블럭에 존재하는 Ray 포지션

    private float stepTime;
    private float stepDelay = 1f;

    /// <summary>
    /// 블럭 그룹 초기화 메서드
    /// </summary>
    public void Initialize()
    {
        // 모든 자식 블럭들을 저장
        // 자식 블럭들의 이미지를 설정
        for (int i = 0; i < transform.childCount; i++)
        {
            blocks.Add(transform.GetChild(i));
            blocks[i].gameObject.GetComponent<SpriteRenderer>().sprite = blockSprites[Random.Range(0, blockSprites.Length)];
        }

        // 맨 아래 블럭을 구하는 작업
        GetLastChil();

        // 블럭에 존재하는 Ray Pos 활성화
        lastRayPos = lastBlock.transform.GetChild(0);
        lastRayPos.gameObject.SetActive(true);
    }

    private void Update()
    {
        // 아래에 블럭 or 벽이 있는지 체크
        DownCheck();


        if (Time.time >= stepTime && !isTouch)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log("왼쪽");
            }

            Move();
        }
    }

    /// <summary>
    /// 블럭 이동 메서드
    /// </summary>
    private void Move()
    {
        Vector3 newPos = transform.position;

        // 임시 키 입력
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("왼쪽 이동");

            // 왼쪽 이동
            newPos.x += Vector2.left.x;
            newPos.y += Vector2.left.y;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("오른쪽 이동");

            // 오른쪽 이동
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

        // Layer 번호
        int mask = 1 << 6 | 1 << 7;

        // 현재 블럭의 아래로 Ray를 발사하여 검사
        RaycastHit2D hit = Physics2D.Raycast(lastRayPos.position + (Vector3.down / 2), Vector2.down, -1.5f, mask);

        if (hit.collider != null)
        {
            isTouch = true;
        }
    }

    /// <summary>
    /// 맨 아래 자식을 구하는 메서드
    /// </summary>
    private void GetLastChil()
    {
        if (transform.childCount <= 0)
        {
            return;
        }

        // 마지막 블럭을 저장
        lastBlock = blocks[blocks.Count() - 1];
    }
}
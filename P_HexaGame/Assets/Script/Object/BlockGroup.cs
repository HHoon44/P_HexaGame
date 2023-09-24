using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static P_HexaGame_Data.Data;

namespace P_HexaGame_Object
{
    /// <summary>
    /// 블럭 클래스
    /// </summary>
    public class BlockGroup : MonoBehaviour
    {
        //[SerializeField]
        //private bool isTouch;                   // 벽 or 블럭에 닿았는지 체크

        [SerializeField]
        private Sprite[] blockSprites;          // 블럭에 설정할 이미지

        [SerializeField]
        private List<Transform> blocks;         // 자식 객체로 존재하는 블럭

        [SerializeField]
        private TouchValue isTouch = TouchValue.None;

        private Transform lastBlock;            // 블럭 그룹 중 마지막에 존재하는 블럭
        private Transform lastRayPos;           // 마지막 블럭에 존재하는 Ray 포지션

        private float stepTime;                 // 블럭 이동 시간
        private float stepDelay = 1f;           // 블럭 이동에 줄 딜레이 값

        private DirValue curDir;                  // 현재 블럭의 방향

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

        private void FixedUpdate()
        {
            // 아래에 블럭 or 벽이 있는지 체크
            RayCheck();
        }

        private void Update()
        {
            if (isTouch == TouchValue.Down)
            {
                // 이미 땅 or 블럭
                return;
            }

            if (Input.GetKeyDown(KeyCode.A) && isTouch != TouchValue.Left)
            {
                isTouch = TouchValue.None;
                curDir = DirValue.Left;
                Move(Vector2.left);
            }
            else if (Input.GetKeyDown(KeyCode.D) && isTouch != TouchValue.Right)
            {
                isTouch = TouchValue.None;
                curDir = DirValue.Right;
                Move(Vector2.right);
            }

            if (Time.time >= stepTime)
            {
                curDir = DirValue.Down;
                Move(Vector2.down);
            }
        }

        /// <summary>
        /// 블럭 이동 메서드
        /// </summary>
        private void Move(Vector2 dir)
        {
            Vector3 newPos = transform.position;
            newPos.x += dir.x;
            newPos.y += dir.y;

            transform.position = newPos;

            // 딜레이를 계속 주면서 시간 업로드
            stepTime = Time.time + stepDelay;
        }

        /// <summary>
        /// 레이를 발사하여 좌, 우, 아래를 확인하는 메서드
        /// </summary>
        private void RayCheck()
        {
            // Layer 번호
            int mask = 1 << 6 | 1 << 7;
            RaycastHit2D hit;
            Vector3 rayDir = Vector3.zero;

            // 분기문으로 방향 설정
            switch (curDir)
            {
                case DirValue.Left:
                    rayDir = Vector3.left;
                    break;

                case DirValue.Right:
                    rayDir = Vector3.right;
                    break;

                case DirValue.Down:
                    rayDir = Vector3.down;
                    break;
            }

            Debug.DrawRay(lastRayPos.position + (rayDir / 1.5f), (Vector2)rayDir, Color.red);

            // 현재 블럭의 아래로 Ray를 발사하여 검사
            hit = Physics2D.Raycast
                (lastRayPos.position + (rayDir / 1.5f), (Vector2)rayDir, -1.5f, mask);

            if (hit.collider != null)
            {
                switch (curDir)
                {
                    case DirValue.Left:
                        isTouch = TouchValue.Left;
                        break;

                    case DirValue.Right:
                        isTouch = TouchValue.Right;
                        break;

                    case DirValue.Down:
                        isTouch = TouchValue.Down;
                        break;
                }
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
}
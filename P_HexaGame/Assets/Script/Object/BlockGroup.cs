using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static P_HexaGame_Data.Data;

namespace P_HexaGame_Object
{
    /// <summary>
    /// 블럭 클래스
    /// </summary>
    public class BlockGroup : MonoBehaviour
    {
        /// <summary>
        /// 블럭에 닿은 오브젝트
        /// </summary>
        public BorderData isBorder;

        public DirData curDir { get; private set; }

        [SerializeField]
        private Sprite[] blockSprites;          // 블럭에 설정할 이미지

        [SerializeField]
        private List<Transform> blocks;         // 자식 객체로 존재하는 블럭

        [SerializeField]
        private Transform rayPos;               // 레이 시작 지점

        private float stepTime;                 // 블럭 이동 시간
        private float stepDelay = 1f;           // 블럭 이동에 줄 딜레이 값

        /// <summary>
        /// 블럭 그룹 초기화 메서드
        /// </summary>
        public void Initialize()
        {
            // 모든 자식 블럭들을 저장
            // 자식 블럭들의 이미지를 설정
            for (int i = 0; i < transform.childCount; i++)
            {
                if (i < transform.childCount - 1)
                {
                    blocks.Add(transform.GetChild(i));
                    blocks[i].gameObject.GetComponent<SpriteRenderer>().sprite = blockSprites[Random.Range(0, blockSprites.Length)];
                    blocks[i].GetComponent<Block>().Initialize();
                }
                else
                {
                    rayPos = transform.GetChild(i);
                }
            }
        }

        private void FixedUpdate()
        {
            // 아래에 블럭 or 벽이 있는지 체크
            DownCheck();
        }

        private void Update()
        {
            if (isBorder == BorderData.B_Border)
            {
                // 이미 땅 or 블럭
                return;
            }

            if (Input.GetKeyDown(KeyCode.A) && isBorder != BorderData.L_Border)
            {
                isBorder = BorderData.None;
                curDir = DirData.Left;
                Move(Vector2.left);
            }
            else if (Input.GetKeyDown(KeyCode.D) && isBorder != BorderData.R_Border)
            {
                isBorder = BorderData.None;
                curDir = DirData.Right;
                Move(Vector2.right);
            }

            if (Time.time >= stepTime)
            {
                curDir = DirData.Down;
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
        /// 아래 레이 체크 메서드
        /// </summary>
        private void DownCheck()
        {
            // 바닥 or 블럭을 체크
            int mask = 1 << 6 | 1 << 7;

            Debug.DrawRay(rayPos.position + (Vector3.down / 2), Vector2.down, Color.red);

            RaycastHit2D hit =
                Physics2D.Raycast(rayPos.position + (Vector3.down / 2), Vector2.down, .3f, mask);

            if (hit.collider != null)
            {
                isBorder = BorderData.B_Border;
            }
        }

        private void LineCheck()
        {

        }
    }
}
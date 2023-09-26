using P_HexaGame_Define;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace P_HexaGame_Object
{
    /// <summary>
    /// 블럭을 관리하는 클래스
    /// </summary>
    public class BlockGroup : MonoBehaviour
    {
        public DirData curDir { get; private set; }

        [SerializeField]
        private Sprite[] blockSprites;          // 블럭에 설정할 이미지

        [SerializeField]
        private List<Block> blocks;             // 자식 객체로 존재하는 블럭

        [SerializeField]
        private Transform rayPos;               // 레이 시작 지점

        private float stepTime;                 // 블럭 이동 시간
        private float stepDelay = 1f;           // 블럭 이동에 줄 딜레이 값

        [SerializeField]
        protected bool isBottom;                // 바닥에 닿았는가에 대한 진리 값

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
                    blocks.Add(transform.GetChild(i).GetComponent<Block>());
                    blocks[i].Initialize(blockSprites[Random.Range(0, blockSprites.Length)]);
                }
                else
                {
                    rayPos = transform.GetChild(i);
                }
            }
        }

        private void FixedUpdate()
        {
            // 맨 아래 블럭이 땅에 닿지 않았다면
            if (!isBottom)
            {
                DownCheck();
            }
        }

        private void Update()
        {
            if (isBottom)
            {
                // 이미 땅 or 블럭
                return;
            }

            if (Input.GetKeyDown(KeyCode.A) && blocks[blocks.Count - 1].isBorder != BorderData.L_Border)
            {
                blocks[blocks.Count - 1].isBorder = BorderData.None;
                curDir = DirData.Left;
                Move(Vector2.left);
            }
            else if (Input.GetKeyDown(KeyCode.D) && blocks[blocks.Count - 1].isBorder != BorderData.R_Border)
            {
                blocks[blocks.Count - 1].isBorder = BorderData.None;
                curDir = DirData.Right;
                Move(Vector2.right);
            }

            // Test
            if (Input.GetKeyDown(KeyCode.S))
            {
                Move(Vector2.down);
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
            Debug.DrawRay(rayPos.position + (Vector3.down / 2), Vector2.down, Color.red);

            RaycastHit2D hit = Physics2D.Raycast(rayPos.position + (Vector3.down / 2), Vector2.down, .5f, LayerMask.GetMask("Border"));

            if (hit.collider != null)
            {
                isBottom = true;
            }
        }

        /*
        private void LineCheck()
        {
            // 현재 블럭이 블럭 or 땅에 위치 한다면
            // 같은 라인에 자신과 같은 블럭이 있는지 체크
            if (isBorder == BorderData.B_Border)
            {
                // 왼쪽으로 레이를 발사하여 자신과 같은 블럭이 있는지 체크
                Debug.DrawRay(rayPos.position + new Vector3(-.6f, .6f, 0), Vector2.left, Color.red);
                RaycastHit2D leftHit = Physics2D.Raycast
                    (rayPos.position + new Vector3(-.6f, .6f, 0), Vector2.left, .3f, LayerMask.GetMask("Block"));

                if (leftHit.collider != null)
                {
                    if (leftHit.collider.GetComponent<Block>().blockColor == blocks[2].blockColor)
                    {
                        Debug.Log("왼쪽에 같은 색의 블럭이 존재합니다.");
                    }
                }

                // 오른쪽으로 레이를 발사하여 자신과 같은 블럭이 있는지 체크
                Debug.DrawRay(rayPos.position + new Vector3(.6f, .6f, 0), Vector2.right, Color.red);
                RaycastHit2D rightHit = Physics2D.Raycast
                    (rayPos.position + new Vector3(.6f, .6f, 0), Vector2.right, .3f, LayerMask.GetMask("Block"));

                if (rightHit.collider != null)
                {
                    if (rightHit.collider.GetComponent<Block>().blockColor == blocks[2].blockColor)
                    {

                    }
                }
            }
        }
        */

    }
}
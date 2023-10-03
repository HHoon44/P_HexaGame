using P_HexaGame_Define;
using P_HexaGame_Util;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace P_HexaGame_Object
{
    /// <summary>
    /// 블럭을 관리하는 클래스
    /// </summary>
    public class BlockGroup : MonoBehaviour
    {
        public DirData curDir { get; private set; }

        [SerializeField]
        private Sprite[] blockSprites;                              // 블럭에 설정할 이미지

        [SerializeField]
        public List<Block> blocks { get; private set; }             // 자식 객체로 존재하는 

        [SerializeField]
        public Block lastBlock { get; private set; }                // 현재 블럭 그룹의 맨 아래 블럭

        [SerializeField]
        private Transform rayPos;                                   // 레이 시작 지점

        private float stepTime;                                     // 블럭 이동 시간
        private float stepDelay = 1f;                               // 블럭 이동에 줄 딜레이 값

        [SerializeField]
        public bool isBottom;                                       // 바닥에 닿았는가에 대한 진리 값

        [SerializeField]
        private GameManager gm;

        /// <summary>
        /// 블럭 그룹 초기화 메서드
        /// </summary>
        public void Initialize()
        {
            gm = GameManager.Instance;
            blocks = new List<Block>();

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

            // 일단 이렇게 마지막 블럭 구하고
            // 나중에 수정하기
            lastBlock = blocks[blocks.Count() - 1];
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
            // 바닥에 오브젝트가 존재하는지 체크
            if (isBottom)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.A) && lastBlock.isBorder != BorderData.L_Border)
            {
                lastBlock.isBorder = BorderData.None;
                curDir = DirData.Left;
                Move(Vector2.left);
            }
            else if (Input.GetKeyDown(KeyCode.D) && lastBlock.isBorder != BorderData.R_Border)
            {
                lastBlock.isBorder = BorderData.None;
                curDir = DirData.Right;
                Move(Vector2.right);
            }

            // 테스트
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

            RaycastHit2D hit = Physics2D.Raycast(rayPos.position + (Vector3.down / 2), Vector2.down, .05f, LayerMask.GetMask("Border", "Block"));

            if (hit.collider != null)
            {
                // 이동이 완료된 블럭 그룹은 게임 매니저에 저장
                gm.AddBlockGroup(this);
                isBottom = true;
            }
        }

        /// <summary>
        /// 블럭 삭제 후 정보 변경 메서드
        /// </summary>
        /// <param name="block"></param>
        public void DeleteBlock(Block block)
        {
            if (blocks.Count <= 0)
            {
                return;
            }

            for (int i = 0; i < blocks.Count(); i++)
            {
                // 현재 존재하는 블럭을 한칸 씩 내린다
                if (blocks[i] != lastBlock)
                {
                    // 마지막 블럭이 아닌 녀석들만 한칸 씩 내려준다
                    blocks[i].gameObject.transform.position += new Vector3(0, -1, 0);
                }
            }

            // 리스트에서 제거된 블럭을 삭제
            blocks.Remove(block);

            // 만약에 현재 들어있는 블럭이 없다면
            if (blocks.Count == 0)
            {
                /// 테스트
                Destroy(gameObject);
            }

            lastBlock = blocks[blocks.Count() - 1];
        }
    }
}
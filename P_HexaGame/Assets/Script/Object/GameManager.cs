using P_HexaGame_Object;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

namespace P_HexaGame_Util
{
    /// <summary>
    /// 게임 매니저
    /// </summary>
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField]
        private Transform spawn_Pos;

        [SerializeField]
        private List<BlockGroup> inBlockGroup = new List<BlockGroup>();

        // Test
        public GameObject blockGroup;

        private void Start()
        {

        }

        private void Update()
        {
            // test
            if (Input.GetKeyDown(KeyCode.Space))
            {
                BlockSpawn();
            }
        }

        /// <summary>
        /// 이동을 완료한 블럭 그룹을 저장 해놓는 메서드
        /// </summary>
        /// <param name="blockGroup">이동을 완료한 블럭 그룹</param>
        public void AddBlockGroup(BlockGroup blockGroup)
        {
            inBlockGroup.Add(blockGroup);
        }

        /// <summary>
        /// 인 게임에 존재하는 모든 블럭 정렬 메서드
        /// </summary>
        public void BlockSort()
        {
            for (int i = 0; i < inBlockGroup.Count - 1; i++)
            {
                // 블럭 그룹의 자식 개수 만큼 반복
                for (int j = 0; j < inBlockGroup[i].blocks.Count; j++)
                {
                    // 접근하려는 블럭이 블럭 그룹의 마지막 블럭인지 체크
                    /// 자기 아래에 블럭이 없는 블럭만 움직이도록 설정해야하는데..
                    if ((inBlockGroup[i].lastBlock == inBlockGroup[i].blocks[j]) && ())
                    {
                        // 아니라면 한칸 씩 내려준다
                        inBlockGroup[i].blocks[j].transform.position += new Vector3(0, -1f, 0);
                    }
                }
            }
        }

        /// <summary>
        /// 블럭 생성 메서드
        /// </summary>
        private void BlockSpawn()
        {
            // test
            GameObject protoGroup = Instantiate(blockGroup, spawn_Pos.position, Quaternion.identity);
            protoGroup.GetComponent<BlockGroup>().Initialize();
            protoGroup.SetActive(true);
        }

    }
}
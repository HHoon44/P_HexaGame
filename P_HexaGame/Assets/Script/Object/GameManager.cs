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
    /// ���� �Ŵ���
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
        /// �̵��� �Ϸ��� �� �׷��� ���� �س��� �޼���
        /// </summary>
        /// <param name="blockGroup">�̵��� �Ϸ��� �� �׷�</param>
        public void AddBlockGroup(BlockGroup blockGroup)
        {
            inBlockGroup.Add(blockGroup);
        }

        /// <summary>
        /// �� ���ӿ� �����ϴ� ��� �� ���� �޼���
        /// </summary>
        public void BlockSort()
        {
            for (int i = 0; i < inBlockGroup.Count - 1; i++)
            {
                // �� �׷��� �ڽ� ���� ��ŭ �ݺ�
                for (int j = 0; j < inBlockGroup[i].blocks.Count; j++)
                {
                    // �����Ϸ��� ���� �� �׷��� ������ ������ üũ
                    /// �ڱ� �Ʒ��� ���� ���� ���� �����̵��� �����ؾ��ϴµ�..
                    if ((inBlockGroup[i].lastBlock == inBlockGroup[i].blocks[j]) && ())
                    {
                        // �ƴ϶�� ��ĭ �� �����ش�
                        inBlockGroup[i].blocks[j].transform.position += new Vector3(0, -1f, 0);
                    }
                }
            }
        }

        /// <summary>
        /// �� ���� �޼���
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
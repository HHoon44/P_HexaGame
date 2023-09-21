using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

namespace P_HexaGame_Data
{

    /// <summary>
    /// �̳༮ �ణ ���� �Ŵ��� ���� �����̳� ����
    /// </summary>
    public class Board : MonoBehaviour
    {
        [SerializeField]
        private Vector3Int spawnPos = new Vector3Int(-1, 8, 0);     // ���� ��ġ ��ǥ

        [SerializeField]
        private Tile[] tile;        // ����� Ÿ��

        [SerializeField]
        private Tilemap tilemap;    // Ÿ���� ������ Ÿ�ϸ�

        /// <summary>
        /// ���� ������ ��
        /// </summary>
        public Block C_Block { get; private set; }

        private void Start()
        {
            C_Block = GetComponentInChildren<Block>();
        }

        private void Update()
        {
            // Test
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Spawn();
            }
        }

        /// <summary>
        /// �� ���� �޼���
        /// </summary>
        private void Spawn()
        {
            for (int i = 0; i < C_Block.cellPos.Length; i++)
            {
                Vector3Int tilePos = (Vector3Int)C_Block.cellPos[i] + spawnPos;

                tilemap.SetTile(tilePos, tile[Random.Range(0, tile.Length)]);
            }
        }
    }
}
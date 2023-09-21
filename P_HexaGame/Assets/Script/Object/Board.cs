using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

namespace P_HexaGame_Data
{

    /// <summary>
    /// 이녀석 약간 게임 매니저 같은 역할이네 ㅋㅋ
    /// </summary>
    public class Board : MonoBehaviour
    {
        [SerializeField]
        private Vector3Int spawnPos = new Vector3Int(-1, 8, 0);     // 생성 위치 좌표

        [SerializeField]
        private Tile[] tile;        // 사용할 타일

        [SerializeField]
        private Tilemap tilemap;    // 타일이 생성될 타일맵

        /// <summary>
        /// 현재 생성할 블럭
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
        /// 블럭 생성 메서드
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
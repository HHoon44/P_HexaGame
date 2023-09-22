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
        private Transform spawn_Pos;

        // Test
        public GameObject block;

        private void Start()
        {

        }

        private void Update()
        {
            // test
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
            // test
            Instantiate(block, spawn_Pos.position, Quaternion.identity);
            block.SetActive(true);
        }
    }
}
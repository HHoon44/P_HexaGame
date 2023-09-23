using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

namespace P_HexaGame_Data
{
    /// <summary>
    /// ���� �Ŵ���
    /// </summary>
    public class GameManager : MonoBehaviour
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
        /// �� ���� �޼���
        /// </summary>
        private void Spawn()
        {
            // test
            GameObject blockGroup = Instantiate(block, spawn_Pos.position, Quaternion.identity);
            blockGroup.GetComponent<BlockGroup>().Initialize();
            blockGroup.SetActive(true);
        }
    }
}
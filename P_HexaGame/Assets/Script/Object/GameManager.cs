using P_HexaGame_Object;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

namespace P_HexaGame_Util
{
    /// <summary>
    /// ���� �Ŵ���
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private Transform spawn_Pos;

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
                Spawn();
            }
        }

        /// <summary>
        /// �� ���� �޼���
        /// </summary>
        private void Spawn()
        {
            // test
            GameObject protoGroup = Instantiate(blockGroup, spawn_Pos.position, Quaternion.identity);
            protoGroup.GetComponent<BlockGroup>().Initialize();
            protoGroup.SetActive(true);
        }
    }
}
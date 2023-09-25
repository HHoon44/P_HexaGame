using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static P_HexaGame_Data.Data;

namespace P_HexaGame_Object
{
    public class Block : MonoBehaviour
    {
        [SerializeField]
        private Transform B_rayPos;

        private BlockGroup blockGroup;

        public void Initialize()
        {
            B_rayPos = transform.GetChild(0);
            blockGroup = GetComponentInParent<BlockGroup>();
        }

        public void FixedUpdate()
        {
            LRCheck();
        }

        /// <summary>
        /// 좌, 우 레이 체크 메서드
        /// </summary>
        private void LRCheck()
        {
            RaycastHit2D hit;

            switch (blockGroup.curDir)
            {
                case DirData.Left:
                    // 왼쪽으로 레이 발사
                    Debug.DrawRay(B_rayPos.position + (Vector3.left / 2f), Vector2.left, Color.red);
                    hit = Physics2D.Raycast
                        (B_rayPos.position + (Vector3.left / 2f), Vector2.left, 1f, LayerMask.GetMask("Border", "Block"));

                    if (hit.collider != null)
                    {
                        // 왼쪽 벽에 닿았음
                        blockGroup.isBorder = BorderData.L_Border;
                    }
                    break;

                case DirData.Right:
                    // 오른쪽으로 레이 발사
                    Debug.DrawRay(B_rayPos.position + (Vector3.right / 1.5f), Vector2.right, Color.red);
                    hit = Physics2D.Raycast
                        (B_rayPos.position + (Vector3.right / 1.5f), Vector2.right, .5f, LayerMask.GetMask("Border", "Block"));

                    if (hit.collider != null)
                    {
                        // 오른쪽 벽에 닿았음
                        blockGroup.isBorder = BorderData.R_Border;
                    }
                    break;
            }
        }
    }
}
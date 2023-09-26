using P_HexaGame_Define;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace P_HexaGame_Object
{
    public class Block : MonoBehaviour
    {
        public BlockColor blockColor { get; private set; }

        public BorderData isBorder;

        [SerializeField]
        private Transform B_rayPos;

        private BlockGroup blockGroup;
        private SpriteRenderer spriteRender;

        public void Initialize(Sprite sprite)
        {
            spriteRender = GetComponent<SpriteRenderer>();

            spriteRender.sprite = sprite;

            // 자식으로 존재하는 Ray Pos를 저장
            B_rayPos = transform.GetChild(0);
            blockGroup = GetComponentInParent<BlockGroup>();

            // 현재 블럭의 색상
            switch (sprite.name)
            {
                case "Blue":
                    blockColor = BlockColor.Blue;
                    break;

                case "Cyan":
                    blockColor = BlockColor.Cyan;
                    break;

                case "Green":
                    blockColor = BlockColor.Green;
                    break;
            }
        }

        private void FixedUpdate()
        {
            LRCheck();

            if (gameObject.GetComponentInParent<BlockGroup>().isBottom)
            {
                // 바닥에 닿았다면 라인 체크
                LineCheck();
            }
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
                    Debug.DrawRay(B_rayPos.position + (Vector3.left / 1.5f), Vector2.left, Color.red);
                    hit = Physics2D.Raycast
                        (B_rayPos.position + (Vector3.left / 1.5f), Vector2.left, .5f, LayerMask.GetMask("Border", "Block"));

                    if (hit.collider != null)
                    {
                        // 왼쪽 벽에 닿았음
                        isBorder = BorderData.L_Border;
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
                        isBorder = BorderData.R_Border;
                    }
                    break;
            }
        }

        /// <summary>
        /// 현재 블럭과 같은 라인에 있는 블럭을 체크 하는 메서드
        /// </summary>
        private void LineCheck()
        {
            // 현재 블럭이 블럭 or 땅에 위치 한다면
            // 같은 라인에 자신과 같은 블럭이 있는지 체크
            if (gameObject.GetComponentInParent<BlockGroup>().isBottom)
            {
                // 왼쪽으로 레이를 발사하여 자신과 같은 블럭이 있는지 체크
                Debug.DrawRay(B_rayPos.position + new Vector3(-.6f, 0f, 0), Vector2.left, Color.red);
                RaycastHit2D leftHit = Physics2D.Raycast
                    (B_rayPos.position + new Vector3(-.6f, 0, 0), Vector2.left, .3f, LayerMask.GetMask("Block"));

                // 왼쪽에 닿은 블럭이 있고 그 블럭의
                // 블럭 그룹이 바닥에 닿은 상태일 때 실행
                if (leftHit.collider != null && leftHit.collider.GetComponentInParent<BlockGroup>().isBottom)
                {
                    if (leftHit.collider.GetComponent<Block>().blockColor == blockColor)
                    {
                        Debug.Log("왼쪽의 색상은" + leftHit.collider.GetComponent<Block>().blockColor.ToString());
                    }
                }

                // 오른쪽으로 레이를 발사하여 자신과 같은 블럭이 있는지 체크
                Debug.DrawRay(B_rayPos.position + new Vector3(.6f, 0f, 0), Vector2.right, Color.red);
                RaycastHit2D rightHit = Physics2D.Raycast
                    (B_rayPos.position + new Vector3(.6f, 0, 0), Vector2.right, .3f, LayerMask.GetMask("Block"));

                if (rightHit.collider != null && rightHit.collider.GetComponentInParent<BlockGroup>().isBottom)
                {
                    if (rightHit.collider.GetComponent<Block>().blockColor == blockColor)
                    {
                        Debug.Log("오른쪽의 색상은" + rightHit.collider.GetComponent<Block>().blockColor.ToString());
                    }
                }

                /**
                 *  왼쪽, 오른쪽 레이에 현재 블럭과 같은 블럭이 걸렸을 시
                 *  그 라인을 제거하는 작업을 진행
                 */
            }
        }
    }
}
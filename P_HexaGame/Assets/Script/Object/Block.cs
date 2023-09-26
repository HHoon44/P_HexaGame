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
    public class Block : BlockGroup
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

            // �ڽ����� �����ϴ� Ray Pos�� ����
            B_rayPos = transform.GetChild(0);
            blockGroup = GetComponentInParent<BlockGroup>();

            // ���� ���� ����
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

            if (isBottom)
            {
                // �ٴڿ� ��Ҵٸ� ���� üũ
                LineCheck();
            }
        }

        /// <summary>
        /// ��, �� ���� üũ �޼���
        /// </summary>
        private void LRCheck()
        {
            RaycastHit2D hit;

            switch (blockGroup.curDir)
            {
                case DirData.Left:
                    // �������� ���� �߻�
                    Debug.DrawRay(B_rayPos.position + (Vector3.left / 1.5f), Vector2.left, Color.red);
                    hit = Physics2D.Raycast
                        (B_rayPos.position + (Vector3.left / 1.5f), Vector2.left, .5f, LayerMask.GetMask("Border", "Block"));

                    if (hit.collider != null)
                    {
                        // ���� ���� �����
                        isBorder = BorderData.L_Border;
                    }
                    break;

                case DirData.Right:
                    // ���������� ���� �߻�
                    Debug.DrawRay(B_rayPos.position + (Vector3.right / 1.5f), Vector2.right, Color.red);
                    hit = Physics2D.Raycast
                        (B_rayPos.position + (Vector3.right / 1.5f), Vector2.right, .5f, LayerMask.GetMask("Border", "Block"));

                    if (hit.collider != null)
                    {
                        // ������ ���� �����
                        isBorder = BorderData.R_Border;
                    }
                    break;
            }
        }

        /// <summary>
        /// ���� ���� ���� ���ο� �ִ� ���� üũ �ϴ� �޼���
        /// </summary>
        private void LineCheck()
        {
            // ���� ���� �� or ���� ��ġ �Ѵٸ�
            // ���� ���ο� �ڽŰ� ���� ���� �ִ��� üũ
            if (isBottom)
            {
                // �������� ���̸� �߻��Ͽ� �ڽŰ� ���� ���� �ִ��� üũ
                Debug.DrawRay(B_rayPos.position + new Vector3(-.6f, .6f, 0), Vector2.left, Color.red);
                RaycastHit2D leftHit = Physics2D.Raycast
                    (B_rayPos.position + new Vector3(-.6f, .6f, 0), Vector2.left, .3f, LayerMask.GetMask("Block"));

                if (leftHit.collider != null)
                {
                    if (leftHit.collider.GetComponent<Block>().blockColor == blockColor)
                    {
                        Debug.Log("���ʿ� ���� ���� ���� �����մϴ�.");
                    }
                }

                // ���������� ���̸� �߻��Ͽ� �ڽŰ� ���� ���� �ִ��� üũ
                Debug.DrawRay(B_rayPos.position + new Vector3(.6f, .6f, 0), Vector2.right, Color.red);
                RaycastHit2D rightHit = Physics2D.Raycast
                    (B_rayPos.position + new Vector3(.6f, .6f, 0), Vector2.right, .3f, LayerMask.GetMask("Block"));

                if (rightHit.collider != null)
                {
                    if (rightHit.collider.GetComponent<Block>().blockColor == blockColor)
                    {
                        Debug.Log("���ʿ� ���� ���� ���� �����մϴ�.");
                    }
                }
            }
        }
    }
}
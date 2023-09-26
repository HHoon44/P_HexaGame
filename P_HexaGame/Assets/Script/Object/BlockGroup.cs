using P_HexaGame_Define;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace P_HexaGame_Object
{
    /// <summary>
    /// ���� �����ϴ� Ŭ����
    /// </summary>
    public class BlockGroup : MonoBehaviour
    {
        public DirData curDir { get; private set; }

        [SerializeField]
        private Sprite[] blockSprites;          // ���� ������ �̹���

        [SerializeField]
        private List<Block> blocks;             // �ڽ� ��ü�� �����ϴ� ��

        [SerializeField]
        private Transform rayPos;               // ���� ���� ����

        private float stepTime;                 // �� �̵� �ð�
        private float stepDelay = 1f;           // �� �̵��� �� ������ ��

        [SerializeField]
        protected bool isBottom;                // �ٴڿ� ��Ҵ°��� ���� ���� ��

        /// <summary>
        /// �� �׷� �ʱ�ȭ �޼���
        /// </summary>
        public void Initialize()
        {
            // ��� �ڽ� ������ ����
            // �ڽ� ������ �̹����� ����
            for (int i = 0; i < transform.childCount; i++)
            {
                if (i < transform.childCount - 1)
                {
                    blocks.Add(transform.GetChild(i).GetComponent<Block>());
                    blocks[i].Initialize(blockSprites[Random.Range(0, blockSprites.Length)]);
                }
                else
                {
                    rayPos = transform.GetChild(i);
                }
            }
        }

        private void FixedUpdate()
        {
            // �� �Ʒ� ���� ���� ���� �ʾҴٸ�
            if (!isBottom)
            {
                DownCheck();
            }
        }

        private void Update()
        {
            if (isBottom)
            {
                // �̹� �� or ��
                return;
            }

            if (Input.GetKeyDown(KeyCode.A) && blocks[blocks.Count - 1].isBorder != BorderData.L_Border)
            {
                blocks[blocks.Count - 1].isBorder = BorderData.None;
                curDir = DirData.Left;
                Move(Vector2.left);
            }
            else if (Input.GetKeyDown(KeyCode.D) && blocks[blocks.Count - 1].isBorder != BorderData.R_Border)
            {
                blocks[blocks.Count - 1].isBorder = BorderData.None;
                curDir = DirData.Right;
                Move(Vector2.right);
            }

            // Test
            if (Input.GetKeyDown(KeyCode.S))
            {
                Move(Vector2.down);
            }

            if (Time.time >= stepTime)
            {
                curDir = DirData.Down;
                Move(Vector2.down);
            }
        }

        /// <summary>
        /// �� �̵� �޼���
        /// </summary>
        private void Move(Vector2 dir)
        {
            Vector3 newPos = transform.position;
            newPos.x += dir.x;
            newPos.y += dir.y;

            transform.position = newPos;

            // �����̸� ��� �ָ鼭 �ð� ���ε�
            stepTime = Time.time + stepDelay;
        }

        /// <summary>
        /// �Ʒ� ���� üũ �޼���
        /// </summary>
        private void DownCheck()
        {
            Debug.DrawRay(rayPos.position + (Vector3.down / 2), Vector2.down, Color.red);

            RaycastHit2D hit = Physics2D.Raycast(rayPos.position + (Vector3.down / 2), Vector2.down, .5f, LayerMask.GetMask("Border"));

            if (hit.collider != null)
            {
                isBottom = true;
            }
        }

        /*
        private void LineCheck()
        {
            // ���� ���� �� or ���� ��ġ �Ѵٸ�
            // ���� ���ο� �ڽŰ� ���� ���� �ִ��� üũ
            if (isBorder == BorderData.B_Border)
            {
                // �������� ���̸� �߻��Ͽ� �ڽŰ� ���� ���� �ִ��� üũ
                Debug.DrawRay(rayPos.position + new Vector3(-.6f, .6f, 0), Vector2.left, Color.red);
                RaycastHit2D leftHit = Physics2D.Raycast
                    (rayPos.position + new Vector3(-.6f, .6f, 0), Vector2.left, .3f, LayerMask.GetMask("Block"));

                if (leftHit.collider != null)
                {
                    if (leftHit.collider.GetComponent<Block>().blockColor == blocks[2].blockColor)
                    {
                        Debug.Log("���ʿ� ���� ���� ���� �����մϴ�.");
                    }
                }

                // ���������� ���̸� �߻��Ͽ� �ڽŰ� ���� ���� �ִ��� üũ
                Debug.DrawRay(rayPos.position + new Vector3(.6f, .6f, 0), Vector2.right, Color.red);
                RaycastHit2D rightHit = Physics2D.Raycast
                    (rayPos.position + new Vector3(.6f, .6f, 0), Vector2.right, .3f, LayerMask.GetMask("Block"));

                if (rightHit.collider != null)
                {
                    if (rightHit.collider.GetComponent<Block>().blockColor == blocks[2].blockColor)
                    {

                    }
                }
            }
        }
        */

    }
}
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static P_HexaGame_Data.Data;

namespace P_HexaGame_Object
{
    /// <summary>
    /// �� Ŭ����
    /// </summary>
    public class BlockGroup : MonoBehaviour
    {
        /// <summary>
        /// ���� ���� ������Ʈ
        /// </summary>
        public BorderData isBorder;

        public DirData curDir { get; private set; }

        [SerializeField]
        private Sprite[] blockSprites;          // ���� ������ �̹���

        [SerializeField]
        private List<Transform> blocks;         // �ڽ� ��ü�� �����ϴ� ��

        [SerializeField]
        private Transform rayPos;               // ���� ���� ����

        private float stepTime;                 // �� �̵� �ð�
        private float stepDelay = 1f;           // �� �̵��� �� ������ ��

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
                    blocks.Add(transform.GetChild(i));
                    blocks[i].gameObject.GetComponent<SpriteRenderer>().sprite = blockSprites[Random.Range(0, blockSprites.Length)];
                    blocks[i].GetComponent<Block>().Initialize();
                }
                else
                {
                    rayPos = transform.GetChild(i);
                }
            }
        }

        private void FixedUpdate()
        {
            // �Ʒ��� �� or ���� �ִ��� üũ
            DownCheck();
        }

        private void Update()
        {
            if (isBorder == BorderData.B_Border)
            {
                // �̹� �� or ��
                return;
            }

            if (Input.GetKeyDown(KeyCode.A) && isBorder != BorderData.L_Border)
            {
                isBorder = BorderData.None;
                curDir = DirData.Left;
                Move(Vector2.left);
            }
            else if (Input.GetKeyDown(KeyCode.D) && isBorder != BorderData.R_Border)
            {
                isBorder = BorderData.None;
                curDir = DirData.Right;
                Move(Vector2.right);
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
            // �ٴ� or ���� üũ
            int mask = 1 << 6 | 1 << 7;

            Debug.DrawRay(rayPos.position + (Vector3.down / 2), Vector2.down, Color.red);

            RaycastHit2D hit =
                Physics2D.Raycast(rayPos.position + (Vector3.down / 2), Vector2.down, .3f, mask);

            if (hit.collider != null)
            {
                isBorder = BorderData.B_Border;
            }
        }

        private void LineCheck()
        {

        }
    }
}
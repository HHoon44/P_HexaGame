using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static P_HexaGame_Data.Data;

namespace P_HexaGame_Object
{
    /// <summary>
    /// �� Ŭ����
    /// </summary>
    public class BlockGroup : MonoBehaviour
    {
        //[SerializeField]
        //private bool isTouch;                   // �� or ���� ��Ҵ��� üũ

        [SerializeField]
        private Sprite[] blockSprites;          // ���� ������ �̹���

        [SerializeField]
        private List<Transform> blocks;         // �ڽ� ��ü�� �����ϴ� ��

        [SerializeField]
        private TouchValue isTouch = TouchValue.None;

        private Transform lastBlock;            // �� �׷� �� �������� �����ϴ� ��
        private Transform lastRayPos;           // ������ ���� �����ϴ� Ray ������

        private float stepTime;                 // �� �̵� �ð�
        private float stepDelay = 1f;           // �� �̵��� �� ������ ��

        private DirValue curDir;                  // ���� ���� ����

        /// <summary>
        /// �� �׷� �ʱ�ȭ �޼���
        /// </summary>
        public void Initialize()
        {
            // ��� �ڽ� ������ ����
            // �ڽ� ������ �̹����� ����
            for (int i = 0; i < transform.childCount; i++)
            {
                blocks.Add(transform.GetChild(i));
                blocks[i].gameObject.GetComponent<SpriteRenderer>().sprite = blockSprites[Random.Range(0, blockSprites.Length)];
            }

            // �� �Ʒ� ���� ���ϴ� �۾�
            GetLastChil();

            // ���� �����ϴ� Ray Pos Ȱ��ȭ
            lastRayPos = lastBlock.transform.GetChild(0);
            lastRayPos.gameObject.SetActive(true);
        }

        private void FixedUpdate()
        {
            // �Ʒ��� �� or ���� �ִ��� üũ
            RayCheck();
        }

        private void Update()
        {
            if (isTouch == TouchValue.Down)
            {
                // �̹� �� or ��
                return;
            }

            if (Input.GetKeyDown(KeyCode.A) && isTouch != TouchValue.Left)
            {
                isTouch = TouchValue.None;
                curDir = DirValue.Left;
                Move(Vector2.left);
            }
            else if (Input.GetKeyDown(KeyCode.D) && isTouch != TouchValue.Right)
            {
                isTouch = TouchValue.None;
                curDir = DirValue.Right;
                Move(Vector2.right);
            }

            if (Time.time >= stepTime)
            {
                curDir = DirValue.Down;
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
        /// ���̸� �߻��Ͽ� ��, ��, �Ʒ��� Ȯ���ϴ� �޼���
        /// </summary>
        private void RayCheck()
        {
            // Layer ��ȣ
            int mask = 1 << 6 | 1 << 7;
            RaycastHit2D hit;
            Vector3 rayDir = Vector3.zero;

            // �б⹮���� ���� ����
            switch (curDir)
            {
                case DirValue.Left:
                    rayDir = Vector3.left;
                    break;

                case DirValue.Right:
                    rayDir = Vector3.right;
                    break;

                case DirValue.Down:
                    rayDir = Vector3.down;
                    break;
            }

            Debug.DrawRay(lastRayPos.position + (rayDir / 1.5f), (Vector2)rayDir, Color.red);

            // ���� ���� �Ʒ��� Ray�� �߻��Ͽ� �˻�
            hit = Physics2D.Raycast
                (lastRayPos.position + (rayDir / 1.5f), (Vector2)rayDir, -1.5f, mask);

            if (hit.collider != null)
            {
                switch (curDir)
                {
                    case DirValue.Left:
                        isTouch = TouchValue.Left;
                        break;

                    case DirValue.Right:
                        isTouch = TouchValue.Right;
                        break;

                    case DirValue.Down:
                        isTouch = TouchValue.Down;
                        break;
                }
            }
        }

        /// <summary>
        /// �� �Ʒ� �ڽ��� ���ϴ� �޼���
        /// </summary>
        private void GetLastChil()
        {
            if (transform.childCount <= 0)
            {
                return;
            }

            // ������ ���� ����
            lastBlock = blocks[blocks.Count() - 1];
        }
    }
}
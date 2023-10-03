using P_HexaGame_Define;
using P_HexaGame_Util;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace P_HexaGame_Object
{
    /// <summary>
    /// ���� �����ϴ� Ŭ����
    /// </summary>
    public class BlockGroup : MonoBehaviour
    {
        public DirData curDir { get; private set; }

        [SerializeField]
        private Sprite[] blockSprites;                              // ���� ������ �̹���

        [SerializeField]
        public List<Block> blocks { get; private set; }             // �ڽ� ��ü�� �����ϴ� 

        [SerializeField]
        public Block lastBlock { get; private set; }                // ���� �� �׷��� �� �Ʒ� ��

        [SerializeField]
        private Transform rayPos;                                   // ���� ���� ����

        private float stepTime;                                     // �� �̵� �ð�
        private float stepDelay = 1f;                               // �� �̵��� �� ������ ��

        [SerializeField]
        public bool isBottom;                                       // �ٴڿ� ��Ҵ°��� ���� ���� ��

        [SerializeField]
        private GameManager gm;

        /// <summary>
        /// �� �׷� �ʱ�ȭ �޼���
        /// </summary>
        public void Initialize()
        {
            gm = GameManager.Instance;
            blocks = new List<Block>();

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

            // �ϴ� �̷��� ������ �� ���ϰ�
            // ���߿� �����ϱ�
            lastBlock = blocks[blocks.Count() - 1];
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
            // �ٴڿ� ������Ʈ�� �����ϴ��� üũ
            if (isBottom)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.A) && lastBlock.isBorder != BorderData.L_Border)
            {
                lastBlock.isBorder = BorderData.None;
                curDir = DirData.Left;
                Move(Vector2.left);
            }
            else if (Input.GetKeyDown(KeyCode.D) && lastBlock.isBorder != BorderData.R_Border)
            {
                lastBlock.isBorder = BorderData.None;
                curDir = DirData.Right;
                Move(Vector2.right);
            }

            // �׽�Ʈ
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

            RaycastHit2D hit = Physics2D.Raycast(rayPos.position + (Vector3.down / 2), Vector2.down, .05f, LayerMask.GetMask("Border", "Block"));

            if (hit.collider != null)
            {
                // �̵��� �Ϸ�� �� �׷��� ���� �Ŵ����� ����
                gm.AddBlockGroup(this);
                isBottom = true;
            }
        }

        /// <summary>
        /// �� ���� �� ���� ���� �޼���
        /// </summary>
        /// <param name="block"></param>
        public void DeleteBlock(Block block)
        {
            if (blocks.Count <= 0)
            {
                return;
            }

            for (int i = 0; i < blocks.Count(); i++)
            {
                // ���� �����ϴ� ���� ��ĭ �� ������
                if (blocks[i] != lastBlock)
                {
                    // ������ ���� �ƴ� �༮�鸸 ��ĭ �� �����ش�
                    blocks[i].gameObject.transform.position += new Vector3(0, -1, 0);
                }
            }

            // ����Ʈ���� ���ŵ� ���� ����
            blocks.Remove(block);

            // ���࿡ ���� ����ִ� ���� ���ٸ�
            if (blocks.Count == 0)
            {
                /// �׽�Ʈ
                Destroy(gameObject);
            }

            lastBlock = blocks[blocks.Count() - 1];
        }
    }
}
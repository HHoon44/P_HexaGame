using UnityEngine;

namespace P_HexaGame_Util
{
    /// <summary>
    /// �̱��� ���̽� Ŭ����
    /// </summary>
    /// <typeparam name="T"> �̱����� ������� �ϴ� �Ļ� Ŭ����</typeparam>
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        /// <summary>
        /// �̱��� �ν��Ͻ��� ã�ų� 
        /// ���� �� �ٸ� �����忡�� ��������� �Ǵ��� ��ü
        /// </summary>
        private static object syncObject = new object();

        private static T instance;

        /// <summary>
        /// �ܺο��� �ν��Ͻ� ��ü�� �����ϱ� ���� ������Ƽ
        /// </summary>
        public static T Instance
        {
            get
            {
                // �ν��Ͻ��� ���ٸ�
                if (instance == null)
                {
                    // �ٸ� �����忡�� ��������� �Ǵ�
                    lock (syncObject)
                    {
                        // �ٸ� �����忡�� ������̶�� �˻�
                        instance = FindObjectOfType<T>();

                        // �׷��� ���ٸ�
                        if (instance == null)
                        {
                            // ���ο� �ν��Ͻ��� ����
                            GameObject obj = new GameObject();

                            // �̸��� ��ũ��Ʈ�� �߰�
                            obj.name = typeof(T).Name;
                            instance = obj.AddComponent<T>();
                        }
                    }
                }

                return instance;
            }
        }

        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance = this as T;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void OnDestroy()
        {
            if (instance != this)
            {
                return;
            }

            instance = null;
        }
    }
}
using UnityEngine;

namespace P_HexaGame_Util
{
    /// <summary>
    /// 싱글턴 베이스 클래스
    /// </summary>
    /// <typeparam name="T"> 싱글톤을 만들고자 하는 파생 클래스</typeparam>
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        /// <summary>
        /// 싱글턴 인스턴스를 찾거나 
        /// 과정 중 다른 스레드에서 사용중인지 판단할 객체
        /// </summary>
        private static object syncObject = new object();

        private static T instance;

        /// <summary>
        /// 외부에서 인스턴스 객체에 접근하기 위한 프로퍼티
        /// </summary>
        public static T Instance
        {
            get
            {
                // 인스턴스가 없다면
                if (instance == null)
                {
                    // 다른 스레드에서 사용중인지 판단
                    lock (syncObject)
                    {
                        // 다른 스레드에서 사용중이라면 검색
                        instance = FindObjectOfType<T>();

                        // 그래도 없다면
                        if (instance == null)
                        {
                            // 새로운 인스턴스를 생성
                            GameObject obj = new GameObject();

                            // 이름과 스크립트를 추가
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
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace _4._20_과제
{
    internal class List<T> : IEnumerable<T>
    {
        private const int DefaultCapacity = 4;

        private T[] user;
        private int size;

        public List()
        {
            user = new T[DefaultCapacity];
            size = 0;
        }

        public int Capacity { get { return user.Length; } }
        public int Count { get { return size; } }

        public T this[int index]                                    // if를 사용하여 index가 0보다 크고 size보다 작을때 user[index]를 하고싶었는데 get과 set를 안적으면 if예악어가 안됐다.
        {
            get
            {
                if (index < 0 || index >= size)
                    throw new IndexOutOfRangeException();

                return user[index];
            }
            set
            {
                if (index > 0 || index <= size)
                    user[index] = value;
            }
        }
        public void Add(T item)                                     //size가 처음 설정한 배열의 길이보다 작으면 배열에 T item을 넣는다 size초기값이 0이므로 배열 맨앞부터 넣는다.
        {
            if (size <= user.Length)
                user[size++] = item;
            else                                                    //위에 조건이 아니면(size가 배열의 길이보다 클때) 배열을 성장시키고 배열에 T item을 넣는다.
            {
                Grow();
                user[size++] = item;
            }
        }
        public void Grow()
        {
            int newCapacity = user.Length + 5;              // 초기 배열의 길이가 4인데 새로운 배열길이를 9로만들고
            T[] newuser = new T[newCapacity];                // 배열길이가 9인 새로운 배열을 만들고
            Array.Copy(user, newuser, size);          // 기존배열을 새로운 배열에 처음부터 카피한다.
            user = newuser;                                 // 그리고 새로운배열의 참조값을 user에 복사한다.
        }
        public bool Remove(T item)
        {
            int index = IndexOf(item);                      // 내가 입력한 T item값을 indexof한값을 intdex로 설정한다.
            if (index >= 0)                                 // index가 배열안에 있으면
            {
                RemoveAt(index);                            //삭제하고
                return true;                                //true로 반환한다.
            }
            return false;                                   // 배열안에 없으면false로 반환한다.
        }
        public int IndexOf(T item)
        {
            return Array.IndexOf(user, item, 0, size);              //배열안에 T item값이 어디에 위치했는지 반환한다.

        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index > size)                      // index값이 0보다 작거나 size보다 크면 예외처리
                throw new IndexOutOfRangeException();
            if (index >= 0 || index <= size)                         // index가 배열범위안에있으면
            {
                size--;                                                         // 배열사이즈를 줄이고
                Array.Copy(user, index, user, index - 1, size - index);         // user배열을 index -1 위치까지 다시 카피
            }

        }
        public struct Enumerator : IEnumerator<T>
        {
            private List<T> list;
            private int index;
            private T currnet;


            public T Current { get { return currnet; } }

            object IEnumerator.Current { get { return Current; } }
            public Enumerator(List<T> list)
            {
                this.list = list;
                this.index = 0;
                this.currnet = default(T);
            }

            public void Dispose()
            {
                Console.WriteLine("List끝");
            }

            public bool MoveNext()
            {
                if (index < list.Count)
                {
                    currnet = list[index++];
                    return true;
                }
                else
                {
                    currnet = default(T);
                    return false;
                }

            }

            public void Reset()
            {
                index = 0;
                currnet = default(T);
            }

        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(this);
        }
    }
}

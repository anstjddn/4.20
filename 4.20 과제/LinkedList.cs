using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4._20_과제
{
    public class LinkedListNode<T> 
    {
        internal LinkedList<T> list;
        internal LinkedListNode<T> next;
        internal LinkedListNode<T> prev;
        private T item;

        public LinkedListNode(T value)                                          //생성자 설정
        {
            this.list = null;
            this.next = null;
            this.prev = null;
            this.item = value;
        }
        public LinkedListNode(LinkedList<T> list, T value)
        {
            this.list = list;
            this.next = null;
            this.prev = null;
            this.item = value;
        }
        public LinkedListNode(LinkedList<T> list, LinkedListNode<T> next, LinkedListNode<T> prev, T value)
        {
            this.list = list;
            this.next = next;
            this.prev = prev;
            this.item = value;
        }
        public LinkedListNode<T> Next { get { return next; } }              // .Next같은거 사용할떄 불러오는값 설정
        public LinkedListNode<T> Prev { get { return prev; } }
        public LinkedList<T> List { get { return list; } }
        public T Item { get { return item; } set { item = value; } }               // 값불러내기??
    }

    public class LinkedList<T>       : IEnumerable<T>       // 초기 생성자설정
    {
        private LinkedListNode<T> head;
        private LinkedListNode<T> tail;
        private int count;

        public LinkedList()
        {
            this.head = null;
            this.tail = null;
            count = 0;
        }


        public LinkedListNode<T> First { get { return head; } }
        public LinkedListNode<T> Last { get { return tail; } }
        public int Count { get { return count; } }

        public LinkedListNode<T> AddFirst(T value)                          // 맨앞에 추가
        {
            LinkedListNode<T> newNode = new LinkedListNode<T>(this, value);

            // 2. 연결구조 바꾸기
            if (head != null)       // 2-1 head 노드가 있었을때
            {

                newNode.next = head;            // 참조형식으로 지정
                head.prev = newNode;
                head = newNode;
            }
            else                                // 2-2 head 노드가 없었을 때== 연결리스트에 아무것도 없었을 때
            {
                head = newNode;
                tail = newNode;
            }
            // 3. 갯수 늘리기
            count++;
            return newNode;

        }

        public LinkedListNode<T> AddLast(T value)              // 맨뒤에 추가
        {
            LinkedListNode<T> newnode = new LinkedListNode<T>(this, value);
            LinkedListNode<T> prveNode = tail;                  // 기존 맨뒤node를 tail로 설정
            if (tail != null)
            {
                newnode = tail;                 // 앞에 붙힌 새로운 노드가 head가 되고 
                prveNode.next = newnode;         // 기존 head 였던 prveNode는 새로운 head Node의 다음이 되게 설정 
            }
            else
            {
                newnode = head;                 // 뒤에가 null이면 newnode가 머리이자 꼬리가 되게끔
                newnode = tail;
            }
            count++;
            return newnode;

        }
        public LinkedListNode<T> AddBefore(LinkedListNode<T> Node, T value)            //내가 중간에 삽입하고자 하는 위치랑 값을 넣고
        {
            LinkedListNode<T> newnode = new LinkedListNode<T>(this, value);


            if (Node.prev != null)                                                  // 넣고자하는 노드 앞에 다른 노드가 있다면
            {
                Node.prev.next = newnode.prev;                                          // 이전의 노드의 다음을 새로운 노드의 이전을 나타내고
                Node.prev = newnode.next;                                              // 삽입하고자하는 이전을 앞에넣은 노드의 다음값으로 받음

            }
            else
            {
                newnode = head;                                         // 넣는 노드 가 head라면  새로운 노드를 head 로 두고 
                Node.prev = newnode.next;                               // 삽입하고자하는 노드 이전값을 새로운 노드값으로 받는다
            }

            count++;
            return newnode;

        }
        public LinkedListNode<T> AddAffer(LinkedListNode<T> Node, T value)            //내가 중간에 삽입하고자 하는 위치랑 값을 넣고
        {
            LinkedListNode<T> newnode = new LinkedListNode<T>(this, value);


            if (Node.next != null)                                                  // 넣고자하는 노드 뒤에 다른 노드가 있다면
            {
                Node.next.prev = newnode.next;                                          // 뒤에 노드의 이전값을 중간에 끼는 노드의 다음값으로 설정하고
                Node.next = newnode.prev;                                              // 기존 노드의 다음값을 중간에 끼는 노드의 이전값으로 설정한다.

            }
            else
            {
                newnode = tail;                                         // 끼워넣고자하는 노드가 마지막부분이면 끼워넣는노드를 tail로 받고 
                Node.next = newnode.prev;                               // 기존노드의 다음값을 마지막 노드의 이전값으로 설정한다.
            }

            count++;
            return newnode;
        }

        public struct Enumerator : IEnumerator<T>
        {
            private LinkedList<T> LinkedList;
            private T current;
            private LinkedListNode<T> node;


            public Enumerator(LinkedList<T> LinkedList)
            {
                this.LinkedList = LinkedList;
                this.current = default(T);
                this.node = LinkedList.head;

            }
            public T Current { get { return current; } }

            object IEnumerator.Current { get { return current; } }

            public void Dispose()
            {
                Console.WriteLine("LinkedList 끝");
            }

            public bool MoveNext()
            {
                if (node != null)
                {
                    current = node.Item;
                    node = node.next;
                    return true;
                }
                else
                {
                    current = default(T);
                    return false;
                }
            }

            public void Reset()
            {
                this.current = default(T);
                this.node = LinkedList.head;
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

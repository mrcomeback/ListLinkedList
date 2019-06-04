using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace List
{

    class DynArr<T>
    {
        private T[] _inner;

        public event Action<T> OnAddedEvent;
        public event Action<T, int> OnINsertedEvent;
        public event Action<T, int> OnRemoveEvent;

        public T this [int index]
        {
            get { return _inner[index]; }
            set { _inner[index] = value; }
        }
        public void Add(T item)
        {
            T[] newInner = new T[_inner == null ? 1 : _inner.Length + 1];

            for (int i = 0; i < newInner.Length; i++)
            {
                if (_inner == null)
                    newInner[i] = item;
                else if (i == newInner.Length - 1)
                    newInner[i] = item;
                else
                    newInner[i] = _inner[i];
            }
            _inner = newInner;

            OnAddedEvent?.Invoke(item);
        }
        public void Remove(T item)
        {
           

            if(_inner != null)
            {
                int removedIndex = 0;
                T[] newInner = new T[_inner.Length - 1];
                int newIndex = 0;              

                for (int i = 0; i < _inner.Length; i++)
                {
                    if (_inner[i].Equals(item))
                    {
                        removedIndex = i;
                        continue;
                    }
                    else
                    {
                        newInner[newIndex] = _inner[i];
                        newIndex++;
                    }

                }
                _inner = newInner;
                OnRemoveEvent?.Invoke(item, removedIndex);


            }else
                Console.WriteLine("Array is empty");
        }
        public void Insert(int index ,T item)
        {
            if (_inner != null)
            {
                T[] newInner = new T[_inner == null ? 1 : _inner.Length + 1];

                int newIndex = 0;

                for (int i = 0; i < _inner.Length; i++)
                {
                    if (i == index && newInner[index] == null)
                    {
                        newInner[newIndex] = item;
                        newIndex++;
                        i--;
                    }
                    else
                    {
                        newInner[newIndex] = _inner[i];
                        newIndex++;
                    }
                        
                }
                _inner = newInner;

                OnINsertedEvent?.Invoke(item,index);
            }
            else
                Console.WriteLine("Array is empty");

        }
        public void RemoveAt(int index)
        {

           
            if (_inner != null && index <= _inner.Length && index >= 0)
            {
                T removedItem = default(T);
                T[] newInner = new T[_inner.Length - 1];
                int newIndex = 0;

                for (int i = 0; i < _inner.Length; i++)
                {
                    if (i == index)
                    {
                        removedItem = _inner[i];
                        continue;
                    }
                    else
                    {
                        newInner[newIndex] = _inner[i];
                        newIndex++;
                    }
                }
                _inner = newInner;
                OnRemoveEvent?.Invoke(removedItem, index);
            }
            else
                Console.WriteLine("Array is empty");
        }
        public void Clear()
        {
            _inner = null;
        }
        public bool Contains(T item)
        {

            for (int i = 0; i < _inner.Length; i++)
            {
                if (_inner[i].Equals(item))
                {
                    return true;
                }
            }
            return false;
        }
        public int IndexOf(T item)
        {
            for (int i = 0; i < _inner.Length; i++)
            {
                if (_inner[i].Equals(item))
                {
                    return i;
                }
            }

            return -1;
        }
    }
    class Person
    {
        private string _name;
        private int _age;

        public Person(string name, int age)
        {
            _name = name;
            _age = age;
        }

        public string getName()
        {
            return _name;
        }
    }
    class Node<T>
    {
        public T Data;
        public Node<T> Next;
        public Node<T> Previous;

        public Node(T data)
        {
            Data = data;
        }
    }
    class LinkedList<T>
    {   

        private Node<T> _first;
        private Node<T> _last;
        private int Count;

        public void Add(T item)
        {
            Node<T> node = new Node<T>(item);

            if (_first == null)
                _first = node;
            else
            {
                _last.Next = node;
                node.Previous = _last;
            }
                

            _last = node;
            Count++;
        }

    }
    class Program
    {
        public static void WriteAdd(Person item)
        {
            Console.WriteLine($"Person {item.getName()} was Added");
        }
        public static void WriteInsert(Person item, int index)
        {
            Console.WriteLine($"Person {item.getName()} was inserted into index {index}");
        }
        public static void WriteRemove(Person item , int index)
        {
            Console.WriteLine($"Person {item.getName()} was removed from index {index}");
        }
        static void Main(string[] args)
        {
            LinkedList<int> linkedList = new LinkedList<int>();

            linkedList.Add(2);
            linkedList.Add(23);
            linkedList.Add(1);
            linkedList.Add(6);
            linkedList.Add(90);
            linkedList.Add(123);


        }
    }
}

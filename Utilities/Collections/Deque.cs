using System.Diagnostics;

namespace Utilities.Collections;

/// <summary>
///     A double-ended queue data structure.
/// </summary>
/// <typeparam name="T">The type of data stored in the <see cref="Deque{T}"/></typeparam>
[DebuggerDisplay("Count = {_size}")]
public class Deque<T>
{
    private T[] _array = [];
    private int _head; // First valid element in the dequeue
    private int _tail; // First open slot in the dequeue, unless the dequeue is full
    private int _size; // Number of elements.

    public int Count => _size;
    public bool IsEmpty => _size == 0;

    public Deque()
    {
    }

    public Deque(IEnumerable<T> items)
    {
        ArgumentNullException.ThrowIfNull(items);
        foreach (var item in items)
        {
            EnqueueTail(item);
        }
    }
    
    /// <summary>
    ///     Enqueue an item at the head of the deque.
    /// </summary>
    public void EnqueueHead(T item)
    {
        if (_size == _array.Length)
        {
            Grow();
        }
    
        _head = (_head == 0 ? _array.Length : _head) - 1;
        _array[_head] = item;
        _size++;
    }
    
    /// <summary>
    ///     Enqueue an item at the tail of the deque.
    /// </summary>
    public void EnqueueTail(T item)
    {
        if (_size == _array.Length)
        {
            Grow();
        }

        _array[_tail] = item;
        if (++_tail == _array.Length)
        {
            _tail = 0;
        }
        _size++;
    }

    /// <summary>
    ///     Dequeue an item from the head of the deque.
    /// </summary>
    public T DequeueHead()
    {
        ThrowIfEmpty();
        var item = _array[_head];
        _array[_head] = default!;

        if (++_head == _array.Length)
        {
            _head = 0;
        }
        _size--;

        return item;
    }

    /// <summary>
    ///     Dequeue an item from the tail of the deque.
    /// </summary>
    public T DequeueTail()
    {
        ThrowIfEmpty();
        if (--_tail == -1)
        {
            _tail = _array.Length - 1;
        }

        var item = _array[_tail];
        _array[_tail] = default!;

        _size--;
        return item;
    }

    /// <summary>
    ///     Peek at the item at the head of the deque without removing it.
    /// </summary>
    public T PeekHead()
    {
        ThrowIfEmpty();
        return _array[_head];
    }

    /// <summary>
    ///     Peek at the item at the tail of the deque without removing it.
    /// </summary>
    public T PeekTail()
    {
        ThrowIfEmpty();
        var index = _tail - 1;
        var item = index == -1
            ? _array[^1]
            : _array[index];
        return item;
    }

    private void Grow()
    {
        Debug.Assert(_size == _array.Length);
        Debug.Assert(_head == _tail);

        const int minimumGrow = 4;
        var capacity = (int)(_array.Length * 2L);
        if (capacity < _array.Length + minimumGrow)
        {
            capacity = _array.Length + minimumGrow;
        }

        var newArray = new T[capacity];
        if (_head == 0)
        {
            Array.Copy(_array, newArray, _size);
        }
        else
        {
            Array.Copy(_array, _head, newArray, 0, _array.Length - _head);
            Array.Copy(_array, 0, newArray, _array.Length - _head, _tail);
        }

        _array = newArray;
        _head = 0;
        _tail = _size;
    }
    
    private void ThrowIfEmpty()
    {
        if (IsEmpty)
        {
            throw new InvalidOperationException("The deque is empty.");   
        }
    }
}
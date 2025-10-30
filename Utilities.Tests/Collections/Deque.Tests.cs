using Utilities.Collections;

namespace Utilities.Tests.Collections;

/// <summary>
///     Tests associated with <see cref="Deque{T}"/>.
/// </summary>
public sealed class DequeTests
{
    [Fact]
    public void Constructor_Empty_IsEmptyAndCountZero()
    {
        // Arrange & Act
        var deque = new Deque<int>();

        // Assert
        Assert.True(deque.IsEmpty);
        Assert.Equal(0, deque.Count);
    }

    [Fact]
    public void Constructor_WithItems_ContainsAllItems()
    {
        // Arrange
        var items = new[] { 1, 2, 3, 4, 5 };

        // Act
        var deque = new Deque<int>(items);

        // Assert
        Assert.False(deque.IsEmpty);
        Assert.Equal(5, deque.Count);
        Assert.Equal(1, deque.PeekHead());
        Assert.Equal(5, deque.PeekTail());
    }

    [Fact]
    public void Constructor_WithNullItems_ThrowsArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new Deque<int>(null!));
    }

    [Fact]
    public void EnqueueHead_SingleItem_BecomesHeadAndTail()
    {
        // Arrange
        var deque = new Deque<int>();

        // Act
        deque.EnqueueHead(42);

        // Assert
        Assert.Equal(1, deque.Count);
        Assert.False(deque.IsEmpty);
        Assert.Equal(42, deque.PeekHead());
        Assert.Equal(42, deque.PeekTail());
    }

    [Fact]
    public void EnqueueTail_SingleItem_BecomesHeadAndTail()
    {
        // Arrange
        var deque = new Deque<int>();

        // Act
        deque.EnqueueTail(42);

        // Assert
        Assert.Equal(1, deque.Count);
        Assert.False(deque.IsEmpty);
        Assert.Equal(42, deque.PeekHead());
        Assert.Equal(42, deque.PeekTail());
    }

    [Fact]
    public void EnqueueHead_MultipleItems_MaintainsCorrectOrder()
    {
        // Arrange
        var deque = new Deque<int>();

        // Act
        deque.EnqueueHead(1);
        deque.EnqueueHead(2);
        deque.EnqueueHead(3);

        // Assert
        Assert.Equal(3, deque.Count);
        Assert.Equal(3, deque.PeekHead());
        Assert.Equal(1, deque.PeekTail());
    }

    [Fact]
    public void EnqueueTail_MultipleItems_MaintainsCorrectOrder()
    {
        // Arrange
        var deque = new Deque<int>();

        // Act
        deque.EnqueueTail(1);
        deque.EnqueueTail(2);
        deque.EnqueueTail(3);

        // Assert
        Assert.Equal(3, deque.Count);
        Assert.Equal(1, deque.PeekHead());
        Assert.Equal(3, deque.PeekTail());
    }

    [Fact]
    public void DequeueHead_SingleItem_ReturnsItemAndBecomesEmpty()
    {
        // Arrange
        var deque = new Deque<int>();
        deque.EnqueueHead(42);

        // Act
        var item = deque.DequeueHead();

        // Assert
        Assert.Equal(42, item);
        Assert.True(deque.IsEmpty);
        Assert.Equal(0, deque.Count);
    }

    [Fact]
    public void DequeueTail_SingleItem_ReturnsItemAndBecomesEmpty()
    {
        // Arrange
        var deque = new Deque<int>();
        deque.EnqueueTail(42);

        // Act
        var item = deque.DequeueTail();

        // Assert
        Assert.Equal(42, item);
        Assert.True(deque.IsEmpty);
        Assert.Equal(0, deque.Count);
    }

    [Fact]
    public void DequeueHead_EmptyDeque_ThrowsInvalidOperationException()
    {
        // Arrange
        var deque = new Deque<int>();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => deque.DequeueHead());
    }

    [Fact]
    public void DequeueTail_EmptyDeque_ThrowsInvalidOperationException()
    {
        // Arrange
        var deque = new Deque<int>();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => deque.DequeueTail());
    }

    [Fact]
    public void PeekHead_EmptyDeque_ThrowsInvalidOperationException()
    {
        // Arrange
        var deque = new Deque<int>();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => deque.PeekHead());
    }

    [Fact]
    public void PeekTail_EmptyDeque_ThrowsInvalidOperationException()
    {
        // Arrange
        var deque = new Deque<int>();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => deque.PeekTail());
    }

    [Fact]
    public void Mixed_EnqueueDequeue_MaintainsCorrectState()
    {
        // Arrange
        var deque = new Deque<int>();

        // Act & Assert
        deque.EnqueueHead(1);
        deque.EnqueueTail(2);
        deque.EnqueueHead(3);
        
        Assert.Equal(3, deque.Count);
        Assert.Equal(3, deque.PeekHead());
        Assert.Equal(2, deque.PeekTail());

        var head = deque.DequeueHead();
        Assert.Equal(3, head);
        Assert.Equal(2, deque.Count);

        var tail = deque.DequeueTail();
        Assert.Equal(2, tail);
        Assert.Equal(1, deque.Count);

        Assert.Equal(1, deque.PeekHead());
        Assert.Equal(1, deque.PeekTail());
    }

    [Fact]
    public void GrowthBehavior_ExceedsInitialCapacity_ContinuesWorking()
    {
        // Arrange
        var deque = new Deque<int>();

        // Act - Add more items than initial capacity to trigger growth
        for (var i = 0; i < 10; i++)
        {
            deque.EnqueueTail(i);
        }

        // Assert
        Assert.Equal(10, deque.Count);
        Assert.Equal(0, deque.PeekHead());
        Assert.Equal(9, deque.PeekTail());

        // Verify all items can be dequeued in correct order
        for (var i = 0; i < 10; i++)
        {
            Assert.Equal(i, deque.DequeueHead());
        }

        Assert.True(deque.IsEmpty);
    }

    [Fact]
    public void CircularBehavior_HeadAndTailOperations_WorkCorrectly()
    {
        // Arrange
        var deque = new Deque<string>();

        // Act - Mix head and tail operations to test circular buffer behavior
        deque.EnqueueTail("A");
        deque.EnqueueTail("B");
        deque.EnqueueHead("C");
        deque.EnqueueHead("D");

        // Assert
        Assert.Equal(4, deque.Count);
        Assert.Equal("D", deque.PeekHead());
        Assert.Equal("B", deque.PeekTail());

        // Dequeue from both ends
        Assert.Equal("D", deque.DequeueHead());
        Assert.Equal("B", deque.DequeueTail());
        
        Assert.Equal(2, deque.Count);
        Assert.Equal("C", deque.PeekHead());
        Assert.Equal("A", deque.PeekTail());
    }
}
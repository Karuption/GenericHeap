using Microsoft.VisualStudio.TestTools.UnitTesting;
using GenericHeap;
using System;

namespace HeapTests;
[TestClass]
public class BasicTests {
    [TestMethod]
    public void IncreaseSize_ShouldIncreaseSize() {
        Heap<int> heap = new(HeapType.Minimum, 2);
        heap.Add(1);
        heap.Add(2);
        heap.Add(3);
        Assert.IsTrue(heap.Size() > 2);
    }
    [TestMethod]
    public void TestSize_ShouldReturnCorrectSize() {
        Heap<int> heap = new(HeapType.Minimum, 2);
        heap.Add(1);
        heap.Add(2);
        heap.Add(3);
        Assert.IsTrue(heap.Size().Equals(3));
    }
    [TestMethod]
    public void TestEmpty_ShouldReturnTrue() =>
        Assert.IsTrue(new Heap<int>(HeapType.Maximum).isEmpty());
    [TestMethod]
    public void TestEmpty_ShouldReturnFalse() {
        Heap<int> heap = new (HeapType.Maximum);
        heap.Add(1);
        Assert.IsFalse(heap.isEmpty());
    }
    [TestMethod]
    public void Initialization_NegitiveInitalization_ShouldGuard() =>
        Assert.IsTrue(new Heap<int>(HeapType.Maximum, -1).Size() >= 0);

    [TestMethod]
    [DataRow( new[] { 5, 516, 86, 789, 4, 2 }, 2)]
    [DataRow( new[] { 5, 516, 86, 789, 4, 2, 86, -8, 598, 5 }, -8)]
    public void TestHeapPeekMinimum_ShouldPass(int[] input, int smallest) {
        Heap<int> heap = new(HeapType.Minimum);

        foreach (var item in input)
            heap.Add(item);

        Assert.IsTrue(heap.Peek().Equals(smallest));
    }
    
    [TestMethod]
    [DataRow( new[] { 5, 516, 86, 789, 4, 2 }, 789)]
    [DataRow( new[] { 5, 516, 86, 789, 4, 2, 86, -8, 1000, 5 }, 1000)]
    public void TestHeapPeekMaximum_ShouldPass(int[] input, int largest) {
        Heap<int> heap = new(HeapType.Maximum);

        foreach (var item in input)
            heap.Add(item);

        Assert.IsTrue(heap.Peek().Equals(largest));
    }

    [TestMethod]
    public void TestPopWhileEmpty_ShouldThrow() {
        Assert.ThrowsException<InvalidOperationException>(() => new Heap<int>(HeapType.Minimum).Pop());
    }

    [TestMethod]
    [DataRow(new[] { 5, 4, 4, 6 }, new[] { 4, 4, 5, 6})]
    [DataRow(new[] { 5, 516, 86, 789, 4, 2 }, new [] { 2, 4, 5, 86, 516, 789})]
    [DataRow(new[] { 5, 516, 86, 789, 4, 2, 86, -8, 1000, 5 }, new[] { -8, 2, 4, 5, 5, 86, 86, 516, 789, 1000 })]
    public void TestHeapPopMinimum_ShouldPass(int[] input, int[] popOrder) {
        Heap<int> heap = new(HeapType.Minimum);

        foreach (var item in input)
            heap.Add(item);
        foreach(var item in popOrder)
            Assert.IsTrue(heap.Pop().Equals(item));
    }
    
    [TestMethod] 
    [DataRow(new[] { 5, 516, 86, 789, 4, 2 }, new [] { 789, 516, 86, 5, 4, 2})]
    [DataRow(new[] { 5, 516, 86, 789, 4, 2, 86, -8, 1000, 5 }, new[] { 1000, 789, 516, 86, 86, 5, 5, 4, 2, -8})]
    [DataRow(new[] { 5, 4, 4, 6}, new [] {6, 5, 4, 4})]
    public void TestHeapPopMaximum_ShouldPass(int[] input, int[] popOrder) {
        Heap<int> heap = new(HeapType.Maximum);

        foreach (var item in input)
            heap.Add(item);
        foreach(var item in popOrder)
            Assert.IsTrue(heap.Pop().Equals(item));
    }

    [TestMethod]
    public void DeleteRoot() {
        Heap<int> heap = new(HeapType.Maximum);
        heap.Add(1);
        heap.RemoveRoot();
        Assert.IsTrue(heap.isEmpty());
    }
    [TestMethod]
    public void DeleteRoot_ThrowsWhenEmpty() {
        Heap<int> heap = new(HeapType.Maximum);
        Assert.ThrowsException<InvalidOperationException>(()=>new Heap<int>(HeapType.Maximum).RemoveRoot());
    }
    [TestMethod]
    public void ReplaceRemovesAndPushesNewMax() {
        Heap<int> heap = new(HeapType.Maximum);
        heap.Add(25);
        Assert.IsTrue(heap.Replace(26).Equals(25));
        Assert.IsTrue(heap.Pop().Equals(26));
    }
}
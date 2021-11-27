namespace GenericHeap;
public class Heap<T> where T : System.IComparable<T>, new() {
    private readonly HeapType _heapType;
    private T[] heapArray = new T[20];
    private int count = 0;


    public Heap(HeapType heapType) =>
        _heapType = heapType;
    public Heap(HeapType heapType, int initializationSize) : this(heapType) =>
        heapArray = new T[initializationSize > 0 ? initializationSize : 0];

    public void Add(T item) {
        if (heapArray.Length == count)
            increaseSize();
        heapArray[count] = item;
        count++;
        siftUp();
    }

    private void increaseSize() {
        T[] newArray = new T[count * 2];
        System.Array.Copy(heapArray, newArray, heapArray.Length);
        heapArray = newArray;
    }
    private void siftUp() {
        var currentNode = count - 1;
        while ((_heapType == HeapType.Minimum) ? heapArray[currentNode].CompareTo(heapArray[getParent(currentNode)]) < 0 :
            heapArray[currentNode].CompareTo(heapArray[getParent(currentNode)]) > 0) {

            swap(currentNode, getParent(currentNode));
            currentNode /= 2;
        }
    }
    private void swap(int i, int j) =>
        (heapArray[i], heapArray[j]) = (heapArray[j], heapArray[i]);
    public int Size() =>
        count;

    public bool isEmpty() =>
        count == 0;

    public T Peek() =>
        heapArray[0];
    public T Pop() {
        if (count == 0)
            throw new InvalidOperationException("Cannot pop from the heap when count is 0");
        var extract = heapArray[0];
        swapWithEnd(0);
        count--;
        siftDown(0);
        return extract;
    }
    private void siftDown(int index) {
        var currentNode = index;
        bool needsCorrection = true;
        
        while (needsCorrection) { 
            if (leftChildGreater(currentNode) || rightChildGreater(currentNode)) {
                if (rightSiblingGreater(currentNode)) {
                    swap(currentNode, getRight(currentNode));
                    currentNode = getRight(currentNode);
                } else {
                    swap(currentNode, getLeft(currentNode));
                    currentNode = getLeft(currentNode);
                }
            }
            else {
                needsCorrection = false;
            }
        } 
        
    }
    public T Replace(T value) {
        var extract = heapArray[0];
        heapArray[0] = value;
        siftDown(0);
        return extract;
    }
    public void RemoveRoot() =>
        Delete(0);
    public void Delete(int index) {
        if (index < 0 || index >= count)
            throw new InvalidOperationException("index out of range");
        swapWithEnd(0);
        count--;
        siftDown(index);
    }
    private void swapWithEnd(int index) =>
        (heapArray[index], heapArray[count-1]) = (heapArray[count-1], default(T));
    private static int getLeft(int index) =>
    index * 2 + 1;
    private static int getRight(int index) =>
        index * 2 + 2;
    private static int getParent(int index) =>
        index / 2;
    private bool leftChildGreater(int index) {
        if(this._heapType == HeapType.Minimum)
            return (getLeft(index) < count) ? heapArray[index].CompareTo(heapArray[getLeft(index)]) > 0: false;
        else
            return (getLeft(index) < count) ? heapArray[index].CompareTo(heapArray[getLeft(index)]) < 0: false;
    }
    private bool rightChildGreater(int currentNode) {
        if(this._heapType == HeapType.Minimum) 
            return (getRight(currentNode) < count) ? heapArray[currentNode].CompareTo(heapArray[getRight(currentNode)]) > 0: false;
        else
            return (getRight(currentNode) < count) ? heapArray[currentNode].CompareTo(heapArray[getRight(currentNode)]) < 0: false;
        }
    private bool rightSiblingGreater(int index) {
        if (this._heapType == HeapType.Minimum)
            return (getRight(index) < count) ? heapArray[getLeft(index)].CompareTo(heapArray[getRight(index)]) > 0:false ;
        else
            return (getRight(index) < count) ? heapArray[getLeft(index)].CompareTo(heapArray[getRight(index)]) < 0:false;

    }

    //Creation
    //heapify: create a heap out of given array of elements
    //merge(union): joining two heaps to form a valid new heap containing all the elements of both, preserving the original heaps.
    //meld: joining two heaps to form a valid new heap containing all the elements of both, destroying the original heaps.

    //Internal
    //increase - key or decrease - key: updating a key within a max - or min - heap, respectively
}

public enum HeapType {
    Minimum,
    Maximum
}


using static System.Console;
public class genlist<T>{
	T[] data;
	public int size = 0;
	public int capacity = 999;
	// Constructor
	public genlist(){ data = new T[capacity];}
	
	// Indexer
    public T this[int i]{
        get {return data[i];}
        set {data[i] = value;}
        }

	// Push method
	public void push(T item){
		if(size == capacity){
			capacity *= 2;
			T[] newdata = new T[capacity];
			for(int i=0; i<capacity; i++){
				newdata[i] = data[i];
			}
			data = newdata;
		}
		data[size] = item;
		size++;
	}

	// Remove method
	public void rem(int j){
		T[] newdata = new T[capacity];
		for(int i=0; i<j; i++){
			newdata[i] = data[i];
		}
		for(int i=j+1; i<size; i++){
			newdata[i-1] = data[i];
		}
		data = newdata;
		size--;
	}
}
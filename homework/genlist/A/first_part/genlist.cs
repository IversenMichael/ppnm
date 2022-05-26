using static System.Console;
public class genlist<T>{
	// Data
    T[] data;

    // Constructor
	public genlist(){
		data = new T[0];
		}
	
    // Size of list
	public int size{
		get {return data.Length;}
		set {WriteLine("You cannot set the size property");}
	}
    // Push method
	public void push(T item){
		T[] newdata = new T[size + 1];
		for(int i=0; i<size; i++){
			newdata[i] = data[i];
		}
		newdata[size] = item;
		data = newdata;
	}

    // Indexer
    public T this[int i]{
        get {return data[i];}
        set {data[i] = value;}
        }

}
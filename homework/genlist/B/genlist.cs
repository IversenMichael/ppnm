public class genlist<T>{
	public T[] data;
	public int size = 0;
	public int capacity = 8;
	public genlist(){ data = new T[capacity]; }
	
	public void push(T item){ /* add item to list */
		if(size==capacity){
			capacity *= 2;
			T[] newdata = new T[capacity];
			for(int i=0; i<size; i++){
				newdata[i] = data[i];
			}
			data = newdata;
		}
		data[size] = item;
		size++;
	}

	public void rem(int j){ /* removes item number j from list */
		T[] newdata = new T[capacity];
		for(int i=0; i<j; i++){
			newdata[i] = data[i];
		}
		for(int i=j+1; i<size; i++){
			newdata[i-1] = data[i];
		}
		data = newdata;
		size -= 1;
	}
}

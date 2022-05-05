using System;
using static System.Console;
public static class main{

public static void Main(){
	var list = new genlist<double>();
	WriteLine($"Initial size of list = {list.size}");
	double x0 = 42.0;
	WriteLine($"We push the number {x0} to the list");
	list.push(x0);	
	WriteLine($"Now the list has size = {list.size}");
	WriteLine($"And the first entry is = {list.data[0]}");
	double x1 = -13;
	WriteLine($"We push the number {x1} to the list");
	list.push(x1);	
	WriteLine($"Now the list has size = {list.size}");
	WriteLine($"And the second entry is = {list.data[1]}");
	
}

public class genlist<T>{
	public T[] data;

	public genlist(){
		data = new T[0];
		}
	
	public int size{
		get {return data.Length;}
		set {WriteLine("You cannot set the size property");}
	}


	public void push(T item){
		T[] newdata = new T[size+1];
		for(int i=0; i<size; i++){
			newdata[i]=data[i];
		}
		newdata[size] = item;
		data = newdata;
	}
	

}
}


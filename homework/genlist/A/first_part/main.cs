using System;
using static System.Console;
public static class main{

public static void Main(){
	WriteLine("PART A");
	var list = new genlist<double>();
	WriteLine($"Initial size of list = {list.size}");
	double x0 = 42.0;
	WriteLine($"We push the number {x0} to the list");
	list.push(x0);	
	WriteLine($"Now the list has size = {list.size}");
	WriteLine($"And the first entry is = {list[0]}");
	double x1 = -13;
	WriteLine($"We push the number {x1} to the list");
	list.push(x1);	
	WriteLine($"Now the list has size = {list.size}");
	WriteLine($"And the second entry is = {list[1]}");	
	WriteLine("We set the second value to 1.23");
	list[1] = 1.23;
	WriteLine($"Now the second value is = {list[1]}");
	Write("When we attempt to set the size of the list, we are told: ");
	list.size = 123;

	}
}


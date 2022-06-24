// Part A
using System;
using static System.Console;
using static System.Math;
public static class main{
	public static void Main(){
		WriteLine(new String('-', 50));
		WriteLine("PART A");
		WriteLine("Reading a sequence of numbers and creating a table");
		for(string line = ReadLine(); line != null; line = ReadLine()){
			var numbers = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
			for(int i = 0; i < numbers.Length; i++){
				double x = double.Parse(numbers[i]);
				WriteLine($"{x}\t{Sin(x)}\t{Cos(x)}");
			}
		}
	}
}

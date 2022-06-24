// Part B
using System;
using static System.Console;
using static System.Math;
public static class main{
	public static void Main(string[] args){
		WriteLine(new String('-', 50));
		WriteLine("PART B");
		WriteLine("Reading numbers from the command line and creating a table");
		foreach(var arg in args){
			double x = double.Parse(arg);
			WriteLine($"{x}\t{Sin(x)}\t{Cos(x)}");
		}
	}
}


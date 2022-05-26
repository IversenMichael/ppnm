using System;
using static System.Console;
using static System.Math;
public static class main{
	public static void Main(){
		WriteLine("CONSTRUCTING A TABLE OF sin(k * x) VALUES FOR k = 1, 2, 3");
		int k = 0;
		Func<double, double> f = delegate(double x){
			return Sin(k * x);
			};
		int[] k_list = new int[3] {1, 2, 3};
		for(int i=0; i<3; i++){
			k = k_list[i];
			WriteLine($"k = {k}");
			table.make_table(f, 0, 1, 0.1);
			WriteLine();
		}
	}
}

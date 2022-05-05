using System;
using static System.Console;
using static System.Math;
static class main{
	static void Main(){
		int n = 9;
		double[] a = new double[n];
		a[0] = 7;
		WriteLine($"a[0] = {a[0]}");
		for (int i=1;i<n;i++)a[i] = i;
		for (int i=0;i<n;i++)WriteLine($"a[{i}] = {a[i]}");

		foreach(double ai in a)WriteLine($"ai = {ai}");
	}
}

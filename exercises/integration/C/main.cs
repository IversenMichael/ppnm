using System;
using static System.Math;
using static System.Console;
static class main{
	public static void Main(){
		WriteLine("We test the performance of the two different implementations by running the code for different values of x");
		for (double x=0; x<=5; x++){
		WriteLine("-------------------------------------");
		WriteLine($"x = {x}");
		WriteLine($"erf(x) = {erf(x)}");
		WriteLine($"erf_new(x) = {erf_new(x)}");
		}
	}

	public static double erf(double x){
		int counter = 0;
		System.Func<double, double> g = delegate(double t){
			counter++;
			return Exp(-t*t);
		};
		double result = 2/Sqrt(PI)*integrate.quad(g, 0, x);
		WriteLine($"Number of calls = {counter}");
		return result;
	}

	public static double erf_new(double x){
		int counter = 0;
		System.Func<double, double> g = delegate(double t){
			counter++;
			return Exp(-t*t);
			};
		double result = 1 - 2/Sqrt(PI)*integrate.quad(g, x, double.PositiveInfinity);
		WriteLine($"Number of calls = {counter}");
		return result;
	}
}

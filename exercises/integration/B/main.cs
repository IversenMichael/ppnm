using System;
using static System.Math;
using static System.Console;
static class main{
	public static void Main(){
		for (double x=0; x<=25; x+=1.0/8){
			WriteLine($"{x} {bessel(0, x)} {bessel(1, x)} {bessel(2, x)} {bessel(3, x)}");
		}
	}

	public static double bessel(int n, double x){
		System.Func<double, double> f = delegate(double t){
			return Cos(n*t - x*Sin(t));
		};
		return integrate.quad(f, 0, PI)/PI;
	}
}

using System;
using static System.Console;
using static System.Math;
using System.Collections.Generic;
public static class main{
	public static void Main(){
		double b = 0.25;
		double c = 5;

		System.Func<double, vector, vector> F = delegate(double t, vector y){
			double theta = y[0];
			double omega = y[1];
			vector Fp = new vector(omega, - b * omega - c * Sin(theta));
			return Fp;
		};	

		double t0 = 0;
		double t1 = 10;
		vector y0 = new vector(PI-0.1, 0.1);
		var xList = new List<double>();
		var yList = new List<vector>();
		vector ystop = ode.ivp(F, t0, y0, t1, xList, yList);
		for(int i=0;i<xList.Count;i++){
			WriteLine($"{xList[i]} {yList[i][0]} {yList[i][1]}");
		}
	}
}

using System;
using static System.Math;
using static System.Console;
public static class main{
public static void Main(){
	for (double x=-5.0; x<=5.0; x+=1.0/32){
	Console.WriteLine($"{x} {gamma(x)}");
	Console.Error.WriteLine($"{x} {lngamma(x)}");
	}
}

static double gamma(double x){
	 // single precision gamma function (Gergo Nemes, from Wikipedia)
	 if(x<0)return PI/Sin(PI*x)/gamma(1-x);
	 if(x<9)return gamma(x+1)/x;
	 double lngamma=x*Log(x+1/(12*x-1/x/10))-x+Log(2*PI/x)/2;
	 return Exp(lngamma);
	 }
static double lngamma(double x){
	 // single precision gamma function (Gergo Nemes, from Wikipedia)
	 if(x<0)return Log(PI/Sin(PI*x)) - Log(gamma(1-x));
	 if(x<9)return Log(gamma(x+1)) - Log(x);
	 double lngamma=x*Log(x+1/(12*x-1/x/10))-x+Log(2*PI/x)/2;
	 return lngamma;
	 }
}


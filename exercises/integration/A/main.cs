using System;
using static System.Math;
using static System.Console;
static class main{
public static void Main(){
System.Func<double, double> f = delegate(double x){return Log(x)/Sqrt(x);};
double result = integrate.quad(f, 0, 1);
WriteLine($"quad(f, 0, 1) = {result} (should be equal to -4)");
for (double x=-3; x<=3; x+=1.0/8){
Error.WriteLine($"{x} {erf(x)}");
}
}

public static double erf(double x){
System.Func<double, double> g = delegate(double t){return Exp(-t*t);};
return 2/Sqrt(PI)*integrate.quad(g, 0, x);
}
}

using System;
using static System.Console;
using static cmath;
using static complex;
using static System.Math;
public static class main{
	public static void Main(){
		complex z = new complex(-1, 0);
		WriteLine($"sqrt(-1) = {sqrt(z)}");
		WriteLine($"Is sqrt(-1) = i? Answer = {sqrt(z).approx(new complex(0, 1))}");
		z = new complex(0, 1);
		WriteLine($"sqrt(i) = {sqrt(z)}");
		WriteLine($"Is sqrt(i) = 0.707 + 0.707i? Answer = {sqrt(z).approx(new complex(1/sqrt(2), 1/sqrt(2)))}");
		WriteLine($"exp(i) = {exp(z)}");	
		WriteLine($"Is exp(i) = 0.540 + 0.841i? Answer = {exp(z).approx(new complex(Cos(1), Sin(1)))}");
		WriteLine($"exp(i*pi) = {exp(z)}");
		WriteLine($"Is exp(i*pi) = -1? Answer = {exp(z*PI).approx(-1)}");	
		WriteLine($"i^i = {z.pow(z)}");
		WriteLine($"Is i^i = exp(-pi/2)? Answer = {z.pow(z).approx(Exp(-PI/2))}");	
		WriteLine($"ln(i) = {log(z)}");
		WriteLine($"Is ln(i) = i*pi/2? Answer = {log(z).approx(new complex(0, PI/2))}");
		WriteLine($"sin(i*pi) = {sin(z*PI)}");
		WriteLine($"Is sin(i*pi) = i*sinh(pi)? Answer = {sin(z*PI).approx(new complex(0, Sinh(PI)))}");
	}
}	

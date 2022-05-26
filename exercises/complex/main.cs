using System;
using static System.Console;
using static cmath;
using static complex;
using static System.Math;
public static class main{
	public static void Main(){
		WriteLine("CALCULATING VALUES AND CHECKING IF THEY ARE CORRECT");
		// Sqrt(-1)
		complex z = new complex(-1, 0);
		WriteLine($"sqrt(-1) = {sqrt(z)}");
		WriteLine($"Is sqrt(-1) = i? Answer = {sqrt(z).approx(new complex(0, 1))}");
		WriteLine("");

		// Sqrt(i)
		z = cmath.I;
		WriteLine($"sqrt(i) = {sqrt(z)}");
		WriteLine($"Is sqrt(i) = 0.707 + 0.707i? Answer = {sqrt(z).approx(new complex(1/sqrt(2), 1/sqrt(2)))}");
		WriteLine("");

		// Exp(i)
		WriteLine($"exp(i) = {exp(z)}");	
		WriteLine($"Is exp(i) = 0.540 + 0.841i? Answer = {exp(z).approx(new complex(Cos(1), Sin(1)))}");
		WriteLine("");

		// Exp(i * pi)
		WriteLine($"exp(i * pi) = {exp(z)}");
		WriteLine($"Is exp(i * pi) = -1? Answer = {exp(z*PI).approx(-1)}");	
		WriteLine("");

		// i ^ i
		WriteLine($"i ^ i = {z.pow(z)}");
		WriteLine($"Is i ^ i = exp(-pi / 2)? Answer = {z.pow(z).approx(Exp(-PI/2))}");	
		WriteLine("");

		// Ln(i)
		WriteLine($"ln(i) = {log(z)}");
		WriteLine($"Is ln(i) = i * pi / 2? Answer = {log(z).approx(new complex(0, PI/2))}");
		WriteLine("");

		// Sin(i * pi)
		WriteLine($"sin(i * pi) = {sin(z*PI)}");
		WriteLine($"Is sin(i * pi) =  * sinh(pi)? Answer = {sin(z*PI).approx(new complex(0, Sinh(PI)))}");
		WriteLine("");

		// Sinh(i)
		WriteLine($"sinh(i) = {sinh(z)}");
		WriteLine($"Is sinh(i) = i * sin(1)? Answer = {sinh(z).approx(new complex(0, sin(1)))}");
		WriteLine("");

		// Sinh(i)
		WriteLine($"cosh(i) = {cosh(z)}");
		WriteLine($"Is cosh(i) = cos(1)? Answer = {cosh(z).approx(new complex(cos(1), 0))}");
	}
}	

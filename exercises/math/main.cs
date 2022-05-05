using System;
using static System.Console;
using static System.Math;
static class main{
	static void Main(){
		double pi = 3.1415;
		double sqrt2 = Sqrt(2.0);
		WriteLine($"Sqrt(2) = {sqrt2} and Sqrt(2)^2 = {sqrt2*sqrt2}");
		double exp_pi = Exp(pi);
		WriteLine($"exp(pi) = {exp_pi} and ln(exp(pi)) = {Log(exp_pi)}");
		double pi_e = Pow(pi, Exp(1));
		WriteLine($"pi^e = {pi_e} and log_pi(pi^e) = {Log(pi_e)/Log(pi)}");
	}
}

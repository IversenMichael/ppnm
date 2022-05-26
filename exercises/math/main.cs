using static System.Console;
using static System.Math;
static class main{
	static void Main(){
		// Square root of 2
		double sqrt2 = Sqrt(2.0);
		WriteLine($"Sqrt(2)\t = {sqrt2},\t Sqrt(2)^2 = {sqrt2 * sqrt2},\t\t\t\t (Result should be 2).");

		// Exponential of pi
		double exp_pi = Exp(PI);
		WriteLine($"exp(pi)\t = {exp_pi},\t ln(exp(pi)) = {Log(exp_pi)},\t (Result should be pi).");

		// Pi to the power of Eulers number
		double pi_e = Pow(PI, Exp(1));
		WriteLine($"pi^e\t = {pi_e},\t log_pi(pi^e) = {Log(pi_e)/Log(PI)},\t (Result should be e).");
	}
}

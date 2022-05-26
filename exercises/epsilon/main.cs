using static System.Console;
using static System.Math;
public static class main{
	public static void Main(){
		// Find the maximum representable integer
		WriteLine("MAXIMUM REPRESENTABLE INTEGER");
		int i_max = 0;
		while(i_max + 1 > i_max){
			i_max++;
		}
		WriteLine($"Maximum representable integer: i_max = {i_max}");
		WriteLine($"This value agrees with int.MaxValue = {int.MaxValue}");
		WriteLine($"When adding one to this integer, we find the lowest representable integer: i_max + 1 = {i_max + 1}");
		WriteLine("");

		// Find the minimum representable integer
		WriteLine("MAXIMUM REPRESENTABLE INTEGER");
		int i_min = 0;
		while(i_min - 1 < i_min){
			i_min--;
		}
		WriteLine($"Minimum representable integer: i_min = {i_min}");
		WriteLine($"This value agrees with int.MinValue = {int.MinValue}");
		WriteLine($"When subtracting one from this integer, we find the largest representable integer: i_min - 1 = {i_min - 1}");
		WriteLine("");

		// Find machine epsilon
		WriteLine("MACHINE EPSILON");
		double epsilon_double = 1.0;
		do {
			epsilon_double /= 2;
		} while(1.0 + epsilon_double != 1.0);
		WriteLine($"Machine epsilon for double = {epsilon_double}");
		double tiny_double = System.Math.Pow(2, -52);
		WriteLine($"Double machine epsilon is close to the expected value for a IEEE 64-bit floating-point number 2^(-52) = {tiny_double}");

		float epsilon_float = 1F;
		do {
			epsilon_float /= 2;
		} while ((float)1F + epsilon_float != 1F);
		WriteLine($"Machine epsilon for float = {epsilon_float}");
		double tiny_float = System.Math.Pow(2, -23);
		WriteLine($"Float machine epsilon is close to the expected value for single-precision 2^(-52) = {tiny_float}");
		WriteLine("");

		// Comparing 1 + tiny + tiny + ... and tiny + tiny + ...
		WriteLine("COMPARING 1 + tiny + tiny + ... and tiny + tiny + ... + 1");
		double tiny = tiny_double/2;
		WriteLine($"Let tiny = tiny_double/2, i.e. tiny = {tiny}");
		int n = (int)1e6;
		double sum_a = 1;
		double sum_b = 0;
		for (int i=0; i<n; i++){
			sum_a += tiny;
			sum_b += tiny;
		}
		sum_a -= 1;
		sum_b += 1;
		sum_b -= 1;
		WriteLine($"sum_a = (1 + tiny + tiny + ...) - 1 = {sum_a}");
		WriteLine($"sum_b = (tiny + tiny + ... + 1) - 1 = {sum_b}");
		WriteLine("The two results are different because 1 + tiny = 1 and consequently the first part of sum_a is one: 1 + tiny + tiny ... = 1.");
		WriteLine("The second sum, on the other hand, is different because tiny + tiny + ... + tiny != 0.");
		WriteLine("");

		// Testing the approximation method
		WriteLine("TESTING THE APPROXIMATION METHOD");
		WriteLine($"approx(1, 2) = {approx(1.0, 2.0)}");
		WriteLine($"approx(1, 1) = {approx(1.0, 1.0)}");
		WriteLine($"approx(1, 1 + machine epsilon) = {approx(1.0, 11.0 + tiny_double)}");
		WriteLine($"approx(machine epsilon, machine epsilon) = {approx(tiny_double, tiny_double)}");
	}

	public static bool approx(double x, double y, double tau=1e-9, double epsilon=1e-9){
		// Approximation method
		if (Abs(x - y) < tau){
			return true;
			}
		if (Abs(x - y)/(Abs(x) + Abs(y)) < epsilon){
			return true;
			}
		return false;
	}	
	
}

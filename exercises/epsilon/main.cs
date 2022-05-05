using static System.Console;
using static System.Math;
public static class main{
	public static void Main(){
		
		int i = 0;
		while(i+1>i){i++;}
		WriteLine($"Max integer: i = {i}");
		WriteLine(i+1);
		WriteLine($"int.MaxValue = {int.MaxValue}");
		
		i = 0;
		while(i-1<i){i--;}
		WriteLine($"Min integer: i = {i}");
		WriteLine($"int.MinValue = {int.MinValue}");
		
		double x = 1;
		while(1+x!=1){
			x /= 2;
		}
		WriteLine($"x = {x}");
		x *= 2;
		WriteLine($"x = {x}");

		float y = 1F;
		while((float)1F+y!=1F){
			y /= 2;
		}
		y *= 2;
		WriteLine($"y = {y}");

		double tiny = System.Math.Pow(2, -52)/2;
		WriteLine($"tiny = {tiny}");
		int n = (int)1e6;
		double sum_a = 0;
		double sum_b = 1;
		for (i=0; i<n; i++){
			sum_a += tiny;
			sum_b += tiny;
		}
		sum_a += 1;
		sum_a -= 1;
		sum_b -= 1;
		WriteLine($"sum_a = {sum_a}");
		WriteLine($"sum_b = {sum_b}");

		WriteLine($"approx(1, 2) = {approx(1, 2)}");
		WriteLine($"approx(1, 1) = {approx(1, 1)}");
	}
	public static bool approx(double a, double b, double tau=1e-9, double epsilon=1e-9){
		if (Abs(a - b) < tau){return true;}
		if (Abs(a - b)/(Abs(a) + Abs(b)) < epsilon){return true;}
		return false;
	}	
	
}

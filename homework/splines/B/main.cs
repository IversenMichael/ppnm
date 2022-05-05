using System;
using static System.Console;
using static System.Math;
public static class main{
	public static void Main(){
	/* TABULATED VALUES FOR SINE */
	int N = 20;
	double[] x = new double[20];
	double[] y = new double[20];
	for(int i=0; i<N; i++){
		x[i] = 2 * PI * i / (N - 1);
		y[i] = Sin(2*x[i]) + Cos(x[i]);
	}
	
	var tab = new System.IO.StreamWriter("tab.txt", append:true);
	for(int i=0; i<N; i++){
		tab.WriteLine($"{x[i]} {y[i]}");
	}
	tab.Close();
	
	/* QUADRATIC SPLINE OF THE TABULATED VAULES */
	var spline = new System.IO.StreamWriter("spline.txt", append:true);
	var q = new qspline(x, y);
	int N_spline = 1000;
	double[] x_spline = new double[N_spline];
	for(int i=0; i<N_spline; i++){
		x_spline[i] = 2 * PI * i /  N_spline; 
		spline.WriteLine($"{x_spline[i]} {q.spline(x_spline[i])} {q.derivative(x_spline[i])} {q.integral(x_spline[i])}");
	}
	spline.Close();
	
	/* SPLINE TEST 1 */
	var spline_test_1 = new System.IO.StreamWriter("spline_test_1.txt", append:true);

	double[] x_1 = new double[5] {1, 2, 3, 4, 5};
	double[] y_1 = new double[5] {1, 1, 1, 1, 1};
	var q_test_1 = new qspline(x_1, y_1);
	spline_test_1.WriteLine("p values");
	for(int i=0; i<4; i++){
		spline_test_1.WriteLine($"{q_test_1.p[i]}");
	}
	spline_test_1.WriteLine("c values");
	for(int i=0; i<4; i++){
		spline_test_1.WriteLine($"{q_test_1.c[i]}");
	}
	spline_test_1.Close();
	
	/* SPLINE TEST 2 */
	var spline_test_2 = new System.IO.StreamWriter("spline_test_2.txt", append:true);
	double[] x_2 = new double[5] {1, 2, 3, 4, 5};
	double[] y_2 = new double[5] {1, 2, 3, 4, 5};
	var q_test_2 = new qspline(x_2, y_2);
	spline_test_2.WriteLine("p values");
	for(int i=0; i<4; i++){
		spline_test_2.WriteLine($"{q_test_2.p[i]}");
	}
	spline_test_2.WriteLine("c values");
	for(int i=0; i<4; i++){
		spline_test_2.WriteLine($"{q_test_2.c[i]}");
	}
	spline_test_2.Close();
	
	/* SPLINE TEST 3 */
	var spline_test_3 = new System.IO.StreamWriter("spline_test_3.txt", append:true);
	double[] x_3 = new double[5] {1, 2, 3, 4, 5};
	double[] y_3 = new double[5] {1, 4, 9, 16, 25};
	var q_test_3 = new qspline(x_3, y_3);
	spline_test_3.WriteLine("p values");
	for(int i=0; i<4; i++){
		spline_test_3.WriteLine($"{q_test_3.p[i]}");
	}
	spline_test_3.WriteLine("c values");
	for(int i=0; i<4; i++){
		spline_test_3.WriteLine($"{q_test_3.c[i]}");
	}
	spline_test_3.Close();
	}
}

class qspline{
	public double[] x;
	public double[] y;
	public double[] p;
	public double[] c;
	public qspline(double[] x_init, double[] y_init){
		x = x_init;
		y = y_init;
		p = new double[x.Length-1];
		c = new double[y.Length-1];
		c[0] = 0;
		for(int i=0; i<x.Length-1; i++){
			p[i] = (y[i+1] - y[i]) / (x[i+1] - x[i]);
		}
		for(int i=0; i<x.Length-2; i++){
			c[i+1] = (p[i+1] - p[i] - c[i] * (x[i+1] - x[i])) / (x[i+2] - x[i+1]);
		}
		c[x.Length-2] /= 2;
		for(int i=x.Length-3; i>=0; i--){
			c[i] = (p[i+1] - p[i] - c[i+1] * (x[i+2] - x[i+1])) / (x[i+1] - x[i]);
		}
	}
	public static int binsearch(double[] x, double z){ 
		if(!(x[0]<=z && z<=x[x.Length-1])) throw new Exception("binsearch: bad z");
		int i=0, j=x.Length-1;
		while(j-i>1){
			int mid=(i+j)/2;
			if(z>x[mid]) i=mid; else j=mid;
		}
		return i;
	}

	public double spline(double z){
		int i = binsearch(x, z);
		return y[i] + p[i]*(z - x[i]) + c[i]*(z - x[i])*(z - x[i+1]);
	}
	public double derivative(double z){
		int i = binsearch(x, z);
		return p[i] + c[i]*(2*z - x[i] - x[i+1]);
	}
	public double integral(double z){
		int i_max = binsearch(x, z);
		double result = 0;
		for(int i=0; i<i_max; i++){
			result += (y[i] - p[i]*x[i] + c[i]*x[i]*x[i+1])*(x[i+1] - x[i]) + 0.5 * (p[i] - c[i]*(x[i] + x[i+1])) * (x[i+1]*x[i+1] - x[i]*x[i]) +c[i] * (x[i+1]*x[i+1]*x[i+1] - x[i]*x[i]*x[i])/3;
		}
		result += (y[i_max] - p[i_max]*x[i_max] + c[i_max]*x[i_max]*x[i_max+1])*(z - x[i_max]) + 0.5 * (p[i_max] - c[i_max]*(x[i_max] + x[i_max+1])) * (z*z - x[i_max]*x[i_max]) + c[i_max] * (z*z*z - x[i_max]*x[i_max]*x[i_max])/3;
		return result;
	}


}


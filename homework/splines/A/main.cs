using System;
using static System.Console;
using static System.Math;
static public class main{
	static public void Main(){
		int size_tab = 12;
		int size_inter = 1000;
		double offset = 1e-3;
		double[] x_tab = new double[size_tab];
		double[] y_tab = new double[size_tab];
		double[] x_inter = new double[size_inter];
		double[] y_inter = new double[size_inter];
		double[] y_integ = new double[size_inter];
		var out_linter = new System.IO.StreamWriter("out.linter.txt", append:true);
		var out_tab = new System.IO.StreamWriter("out.tab.txt", append:true);
		for(int i=0; i<size_tab; i++){
			x_tab[i] = 2*PI*i/(size_tab-1);
			y_tab[i] = Sin(x_tab[i]);
			out_tab.WriteLine($"{x_tab[i]} {y_tab[i]}");
		}
		for(int i=0; i<size_inter; i++){
			x_inter[i] = 2*PI*offset + (1 - 2*offset)*2*PI*i/(size_inter-1);
			y_inter[i] = linterp(x_tab, y_tab, x_inter[i]);
			y_integ[i] = linterpInteg(x_tab, y_tab, x_inter[i]);
			out_linter.WriteLine($"{x_inter[i]} {y_inter[i]} {y_integ[i]} {Sin(x_inter[i])} {1 - Cos(x_inter[i])}");
		}


		out_linter.Close();
		out_tab.Close();
	}
	public static int binsearch(double[] x, double z){
			if(!(x[0]<=z && z<=x[x.Length-1])){
				throw new Exception("binsearch: bad z");
			}
			int i = 0;
			int j = x.Length-1;
			while(j-i>1){
				int mid=(i+j)/2;
				if(z>x[mid]){
					i=mid;
				}
			       	else{
					j=mid;
				}
				}
			return i;
		}

	static public double linterp(double[] x, double[] y, double z){
		int i = binsearch(x, z);
		z = y[i] + (y[i+1]-y[i])/(x[i+1]-x[i])*(z - x[i]);
		return z;
		}

	
	static public double linterpInteg(double[] x, double[] y, double z){
		int size = x.Length;
		
		double integral = 0.0;
		for(int i=0; i<size; i++){
			if(x[i+1] < z){
			integral += y[i]*(x[i+1]-x[i]) + 0.5*(y[i+1]-y[i])*(x[i+1]-x[i]);
			}
			else{
			integral += y[i]*(z-x[i]) + 0.5*(linterp(x, y, z)-y[i])*(z-x[i]);
			break;
			}
		}	
		return integral;
		}


	
}

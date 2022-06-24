using System;
using System.IO;
using static System.Console;
using static System.Math;
public static class main{
	public static void Main(){
		/* Output streams */
		var Rutherford_Soddy = new System.IO.StreamWriter("Rutherford_Soddy.txt", append:true);
		var fit = new System.IO.StreamWriter("fit.txt", append:true);
		/* Data */
		double[] t = new double[] {1, 2, 3, 4, 6, 9, 10, 13, 15};	
		double[] y = new double[] {117, 100, 88, 72, 53, 29.5, 25.2, 15.2, 11.1};
		double[] dy = new double[] {5, 5, 5, 5, 5, 1, 1, 1, 1};
		for(int i=0; i<t.Length; i++){
			Rutherford_Soddy.WriteLine($"{t[i]} {y[i]} {dy[i]}");
		}

		/* Convert data to log */
		double[] logy = new double[y.Length];
		double[] dlogy = new double[dy.Length];
		for(int i=0; i<y.Length; i++){
			logy[i] = Log(y[i]);
			dlogy[i] = dy[i]/y[i];
		}
		/* Functions */
		var fs = new Func<double,double>[] { z => 1.0, z => z};

		/* Ordinary least squares */
		var ls = new LS(t, logy, dlogy, fs);
		int N_fit = 100;
		double[] t_fit = new double[N_fit];
		double[] y_fit = new double[N_fit];
		for(int i=0; i<N_fit; i++){
			t_fit[i] = (t[t.Length-1] - t[0])/(N_fit-1) * i + t[0];
			y_fit[i] = Exp(ls.c[0]) * Exp(ls.c[1] * t_fit[i]);

			fit.WriteLine($"{t_fit[i]} {y_fit[i]}");
		}
		Rutherford_Soddy.Close();
		fit.Close();
	
		double half_life = Log(1.0/2)/ls.c[1];
		WriteLine($"The fitting coefficients are a = {Exp(ls.c[0])}, lambda = {-ls.c[1]}");
                WriteLine("The Sigma matrix is = ");
		ls.Sigma.print();
		WriteLine($"From the measurements we find the half life of Ra-224: {half_life} days");
		WriteLine($"The correct half life of Ra-224 is 3.6 days");
		double sigma_half_life = Sqrt(ls.Sigma[1, 1]) * Log(2)/(ls.c[1]*ls.c[1]);
		WriteLine($"With uncertainties, the half life is between {half_life - sigma_half_life} and {half_life + sigma_half_life}");
		WriteLine("The modern value of half life does not lie within the errorbars");

		/* Adding uncertainties to fit coefficients */
		var fit_uncertainties = new System.IO.StreamWriter("fit_uncertainties.txt", append:true);
		for(int i=0; i<N_fit; i++){
			fit_uncertainties.WriteLine($"{t_fit[i]} {Exp(ls.c[0] - Sqrt(ls.Sigma[0, 0])) * Exp(ls.c[1] * t_fit[i])} {Exp(ls.c[0] + Sqrt(ls.Sigma[0, 0])) * Exp(ls.c[1] * t_fit[i])} {Exp(ls.c[0]) * Exp((ls.c[1] - Sqrt(ls.Sigma[1, 1])) * t_fit[i])} {Exp(ls.c[0]) * Exp((ls.c[1] + Sqrt(ls.Sigma[1, 1])) * t_fit[i])}");
		}
		fit_uncertainties.Close();
	}
}


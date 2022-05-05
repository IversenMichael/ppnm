using System;
using static System.Random;
using static System.Console;
using static System.Math;
public static class main{
    public static void Main(){
        // Number of monte carlo samples
        int N = 100_000;

        // Example function
        Func<vector, double> f = delegate(vector x){
            return x[0]*x[1];
            };

        // Integration limits
        vector a = new vector(new double[] {0.0, 0.0});
        vector b = new vector(new double[] {1.0, 1.0});

        (double I, double err) = monte_carlo_integrate(f, a, b, N);
        WriteLine("Integral of f(x, y) = x*y from 0 to 1 along both x and y is equal to 0.25");
        WriteLine($"Using monte carlo integration, we find I = {I}");
        WriteLine($"With integration error = {err}");

        // Example function
        Func<vector, double> g = delegate(vector x){
            return 1/(1 - Cos(x[0])*Cos(x[1])*Cos(x[2]));
            };

        // Integration limits
        a = new vector(new double[] {0.0, 0.0, 0.0});
        b = new vector(new double[] {PI, PI, PI});

        (I, err) = monte_carlo_integrate(g, a, b, N);
        WriteLine("Integral of f(x, y, z) = 1/(1 - cos(x)*cos(y)*cos(z))*1/PI^3 from 0 to PI along all directions is equal to 1.393");
        WriteLine($"Using monte carlo integration, we find I = {I/Pow(PI, 3)}");
        WriteLine($"With integration error = {err/Pow(PI, 3)}");
    }
        public static (double, double) monte_carlo_integrate (Func<vector, double> f, vector a, vector b, int N){
        Random rnd = new Random();
        int size = a.size;

        // Integration volume
        double V = 1.0;
        for(int i=0; i<size; i++){
            V *= (b[i] - a[i]);
        }

        // Sum of function value, f(x), and its square, f(x)^2.
        double sum_f = 0;
        double sum_f2 = 0;

        // Monte carlo samples
        vector x = new vector(size);
        for (int i=0; i<N; i++){
            for (int j=0; j<size; j++){
                x[j] = a[j] + rnd.NextDouble() * (b[j] - a[j]);
            }
            sum_f += f(x);
            sum_f2 += Pow(f(x), 2);
        }
        // Mean value of function <f(x)> and its square <f(x)^2>
        double mean_f = sum_f/N;
        double mean_f2 = sum_f2/N;

        // Variance of funtion value
        double var_f = mean_f2 - Pow(mean_f, 2);

        // Estimate and error of integral
        double I = V*mean_f;
        double err = V*Sqrt(var_f/N);

        return (I, err);
    }
}
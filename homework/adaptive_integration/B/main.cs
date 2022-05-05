using System;
using static System.Console;
using static System.Math;
public class main{
    public static void Main(){
        int ncalls = 0;
        Func<double, double> f = delegate(double x){
            ncalls = ncalls + 1;
            return 1/Sqrt(x);
            };
        WriteLine($"Integral of 1/Sqrt(x) = {Clenshaw_Curtis_integrate(f, 0.0, 1.0, double.NaN, double.NaN, 0, 0.0001, 0.0001)}. The answer should be 2.");
        WriteLine($"Comparison with and without variable transformation.");
        WriteLine($"With variable transformation: The function was called {ncalls} times");

        ncalls = 0;
        integrate(f, 0.0, 1.0, double.NaN, double.NaN, 0, 0.0001, 0.0001);
        WriteLine($"Without variable transformation: The function was called {ncalls} times");

        ncalls = 0;
        Func<double, double> g = delegate(double x){
            ncalls = ncalls + 1;
            return Log(x)/Sqrt(x);
            };
        WriteLine("");
        WriteLine($"Integral of ln(x)/Sqrt(x) = {Clenshaw_Curtis_integrate(g, 0.0, 1.0, double.NaN, double.NaN, 0, 0.0001, 0.0001)}. The answer should be -4.");
        WriteLine($"Comparison with and without variable transformation.");
        WriteLine($"With variable transformation: The function was called {ncalls} times");

        ncalls = 0;
        integrate(g, 0.0, 1.0, double.NaN, double.NaN, 0, 0.0001, 0.0001);
        WriteLine($"Without variable transformation: The function was called {ncalls} times");
        WriteLine("");
    }
    public static double integrate(Func<double, double> f, double a, double b, double y1, double y2, int nrec, double delta, double epsilon)
    {   

        if(nrec > 1_000_000){
            WriteLine("Recursion depth reached");
            return double.NaN;
        }
        double[] x = new double[] {a + 1*(b-a)/6, a + 2*(b-a)/6, a + 4*(b-a)/6, a + 5*(b-a)/6};
        if(double.IsNaN(y1)){ 
            y1 = f(x[1]); 
            y2 = f(x[2]); 
            }
        double[] y = new double[] {f(x[0]), y1, y2, f(x[3])};
        double[] w = new double[] {2*(b-a)/6, (b-a)/6, (b-a)/6, 2*(b-a)/6};
        double[] v = new double[] {1*(b-a)/4, 1*(b-a)/4, 1*(b-a)/4, 1*(b-a)/4};
        double Q = 0;
        double q = 0;
        for(int i=0; i<4; i++){
            Q += w[i]*y[i];
            q += v[i]*y[i];
        }
        double err = Abs(Q - q);
        if(err <= Max(delta, epsilon*Abs(Q))){
            return Q;
        }
        else{
            double IL = integrate(f, a, (a+b)/2, y[0], y[1], nrec+1, delta/Sqrt(2), epsilon);
            double IR = integrate(f, (a+b)/2, b, y[2], y[3], nrec+1, delta/Sqrt(2), epsilon);
            return IL + IR;
        }
    }

    public static double Clenshaw_Curtis_integrate(Func<double, double> f, double a, double b, double y1, double y2, int nrec, double delta, double epsilon)
    {
        Func<double, double> f_transformed = delegate(double x){return f((a+b)/2 + (b-a)/2*Cos(x))*Sin(x);};
        return (b-a)/2 * integrate(f_transformed, 0.0, PI, y1, y2, nrec, delta, epsilon);
    }
}
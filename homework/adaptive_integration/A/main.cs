using System;
using static System.Console;
using static System.Math;
public class main{
    public static void Main(){
        Func<double, double> f = delegate(double x){return Sqrt(x);};
        double I = integrate(f, 0.0, 1.0, double.NaN, double.NaN, 0, 0.001, 0.001);
        WriteLine($"Integral of sqrt(x) = 2/3. Numerical result = {I}");

        f = delegate(double x){return 1.0/Sqrt(x);};
        I = integrate(f, 0.0, 1.0, double.NaN, double.NaN, 0, 0.001, 0.001);
        WriteLine($"Integral of 1/sqrt(x) = 2. Numerical result = {I}");

        f = delegate(double x){return 4.0*Sqrt(1 - x*x);};
        I = integrate(f, 0.0, 1.0, double.NaN, double.NaN, 0, 0.001, 0.001);
        WriteLine($"Integral of 4*sqrt(1-x^2) = pi. Numerical result = {I}");

        f = delegate(double x){return Log(x)/Sqrt(x);};
        I = integrate(f, 0.0, 1.0, double.NaN, double.NaN, 0, 0.001, 0.001);
        WriteLine($"Integral of ln(x)/sqrt(x) = -4. Numerical result = {I}");

        int N = 1000;
        double a = -5.0;
        double b = 5.0;
        double[] x_plot = new double[N];
        double[] y_plot = new double[N];
        for(int i=0; i<N; i++){
            x_plot[i] = a + (b-a)*i/(N-1);
            y_plot[i] = erf(x_plot[i]);
            Error.WriteLine($"{x_plot[i]} {y_plot[i]}");
        }
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

    public static double erf(double z){
        Func<double, double> f = delegate(double x){return Exp(-x*x);};
        return 2/Sqrt(PI) * integrate(f, 0, z, double.NaN, double.NaN, 0, 0.0001, 0.0001);
    }
}
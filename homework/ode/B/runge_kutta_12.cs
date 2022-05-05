using System;
using static System.Console;
using static System.Math;
public class runge_kutta_12{
    public System.Func<double, vector, vector> f;
    public double x;
    public double a;
    public double b;
    public vector y;
    public double h = 0.01;
    public double acc = 0.01;
    public double eps = 0.01;
    public runge_kutta_12(System.Func<double, vector, vector> f0, double a0, double b0, vector y0){
        f = f0;
        x = a0;
        a = a0;
        b = b0;
        y = y0;
    }
    public (vector, vector) stepper(){
        vector k1 = f(x, y);
        vector k2 = f(x + h, y + h*k1);
        vector k = 0.5*k1 + 0.5*k2;
        vector y_high = y + h*k;
        vector y_low = y + h*k1;
        vector err = y_high - y_low;
        return (y_high, err);
    }

    public (genlist<double>, genlist<double>, genlist<double>) driver(){
        genlist<double> times = new genlist<double>();
        genlist<double> thetas = new genlist<double>();
        genlist<double> omegas = new genlist<double>();

        double[] tol = new double[y.size];
        while (x<b){
            (vector y_new, vector err) = stepper();
            for (int i=0; i<tol.Length; i++){
            tol[i] = Max(acc, Abs(y_new[i])*eps) * Sqrt(h / (b-a));
            }
            bool error_check = true;
            for (int i=0; i<tol.Length; i++){
                if (err[i] > tol[i]){
                    error_check = false;
                }
            }
            if(error_check){
                x += h;
                y = y_new;
                times.push(x);
                thetas.push(y[0]);
                omegas.push(y[1]);
            }

            double factor = tol[0] / Abs(err[0]);
            for(int i=1; i<tol.Length; i++){
                factor = Min(factor, tol[i]/Abs(err[i]));
            }
            h *= Min(Pow(factor, 0.25) * 0.95, 2);
        }
        return (times, thetas, omegas);
    }
}

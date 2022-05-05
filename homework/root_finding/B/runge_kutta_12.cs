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
    public double acc = 1e-6;
    public double eps = 1e-6;
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

    public (double, vector) driver(){
        vector y_new;
        vector err;
        double tol;
        while (x<b){
            (y_new, err) = stepper();
            tol = Max(acc, y_new.norm()*eps) * Sqrt(h / (b-a));
            if(err.norm() <= tol){
                x += h;
                y = y_new;
                return (x, y);
            }
            h *= Min(Pow(tol/err.norm(), 0.25) * 0.95, 2);
        }
        WriteLine("Program never finished");
        return (x, y);
    }
}

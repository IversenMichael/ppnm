using System;
using static System.Console;
using static System.Math;
public static class main{
    public static void Main(){
        System.Func<double, vector, vector> f = delegate (double x, vector y){return new vector(y[1], -0.25*y[1] - 5*Sin(y[0]));};
        double a = 0;
        double b = 10;
        vector y0 = new vector(PI-0.1, 0);

	    var rk = new runge_kutta_12(f, a, b, y0);
        (genlist<double> times, genlist<double> thetas, genlist<double> omegas) = rk.driver();
        var outfile = new System.IO.StreamWriter("out.txt", append:true);
        for (int i=0; i<times.size; i++){
            outfile.WriteLine($"{times.data[i]} {thetas.data[i]} {omegas.data[i]}");
        }
    }
}

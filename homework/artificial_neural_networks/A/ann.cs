using System;
using static System.Console;
using static System.Math;
public class ann{
    // Artificial neural network parameters
    public int n;                  // Size of network
    public Func<double, double> f; // Activation function
    public vector p;
    
    //Constructor
    public ann(int n0, Func<double, double> f0){ 
        n = n0;
        f = f0;
        p = new vector(3*n);
        }

    public double response(double x){
        return output(x, p);
    }

    public double output(double x, vector q){
        double res = 0.0;
        for(int i=0; i<n; i++){
            res += q[2*n + i] * f((x - q[i]) / q[n+i]);
        }
        return res;
    }

    public void train(double[] x, double[] y){
        double xmin = x[0];
        double xmax = x[x.Length-1];

        // Initial parameters  values
        // Initial a values
        vector q0 = new vector(3*n);
        for(int i=0; i<n; i++){ 
            q0[i] = xmin + (xmax - xmin) * i / (n - 1);
        }

        // Initial b values
        for(int i=n; i<2*n; i++){ 
            q0[i] = 1;
        }

        // Initial w values
        for(int i=2*n; i<3*n; i++){ 
            q0[i] = 0;
        }

        Func<vector, double> C = delegate(vector q){
            double cost = 0.0;
            for(int i=0; i<x.Length; i++){
                cost += Pow(output(x[i], q) - y[i], 2);
            }
            return cost;
        };
        double acc = 1e-6;
        q0.print();
        (vector q_new, int n_iter) = QN.quasi_newton(C, q0, acc);
        p = q_new;
    }
}
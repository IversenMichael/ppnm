using System;
using static System.Console;
using static System.Math;
public class main{
    public static double SqrtEpsilon = Pow(2, -26);

    public static void Main(){
        // Load data
        double[] E = new double[30];
        double[] cross = new double[30];
        double[] err = new double[30];

        int i = 0;
        string line = In.ReadLine();
        do{
            string[] values = line.Split(" ");
            E[i] = double.Parse(values[0]);
            cross[i] = double.Parse(values[1]);
            err[i] = double.Parse(values[2]);
            line = In.ReadLine();
            i++;
        } while(line != null);

        Func<double, double, double, double, double> Breit_Wigner = delegate(double A, double m, double Gamma, double e){
            return A/(Pow(e - m, 2) + Pow(Gamma, 2)/4);
        };

        Func<vector, double> D = delegate(vector x){
            double deviation = 0.0;
            for(i=0; i<30; i++){
                deviation += Pow((Breit_Wigner(x[0], x[1], x[2], E[i]) - cross[i])/err[i], 2);
            }
            return deviation;
        };
        double acc = 1e-6;
        vector x0 = new vector(new double[] {0.5, 124, 0.5});
        vector x_root;
        int n_iter;
        (x_root, n_iter) = quasi_newton(D, x0, acc);
        WriteLine("Fitting parameters");
        WriteLine($"A = {x_root[0]}");
        WriteLine($"m = {x_root[1]}");
        WriteLine($"Gamma = {x_root[2]}");
        WriteLine($"Number of iterations = {n_iter}");


    }
    public static (vector, int) quasi_newton(Func<vector, double> f, vector x0, double acc){
        // Initialization
        double lambda, denom, alpha = 1e-4, eps = 1e-6;
        int n_iter = 0, max_iter = 1000000, size = x0.size;    
        vector nf1, nf2, delta_x, s, y, u, x = x0.copy(); 
        matrix B = identity_matrix(size), dB;

        do{
            nf1 = nabla_f(f, x);        // Derivative of f at x
            delta_x = - B * nf1;        // Newton's step
            lambda = 1.0;               // Step size ratio
            s = lambda * delta_x;       // Step
            while(f(x+s) - f(x) > alpha * (s % nf1) & lambda >= 1.0/1024){ // (Armijo condition) & (step size must not be too small)
                lambda = lambda/2.0;    // If step size is too large, we reduce it by a factor 2.
                s = lambda * delta_x;   // New step
            }
            if(lambda < 1.0/1024){        // If step size becomes too small, we reset B
                B = identity_matrix(size);
            }
            x += s;                     // Perform step

            nf2 = nabla_f(f, x);        // Derivative of f at new x
            y = nf2 - nf1;
            u = s - B*y;
            denom = u.dot(y);
            if(Abs(denom) > eps){       // If denominator is not too small, we update B.
                dB = outer(u, u) * (1.0/denom);
                B += dB;
            }
            n_iter += 1;                // Increment number of iterations
        } while(nf2.norm() > acc & n_iter < max_iter);  // If gradient is smaller than accuracy, we are done.
        return (x, n_iter);
    }

    public static vector nabla_f(Func<vector, double> f, vector x){
        int size = x.size;
        vector xdx = x.copy();
        vector nf = new vector(size);
        for(int i=0; i<size; i++){
            xdx[i] += SqrtEpsilon;
            nf[i] = (f(xdx) - f(x))/SqrtEpsilon;
            xdx[i] = x[i];
        }
        return nf;
    }

    public static matrix identity_matrix(int size){
        matrix A = new matrix(size, size);
        for(int i=0; i<size; i++){
            for(int j=0; j<size; j++){
                if(i==j){
                    A[i, j] = 1;
                }
                else{
                    A[i, j] = 0;
                }
            }
        }
        return A;
    }
    
    public static matrix outer(vector v, vector u){
        int size1 = v.size;
        int size2 = u.size;
        matrix A = new matrix(size1, size2);
        for(int i=0; i<size1; i++){
            for(int j=0; j<size2; j++){
                A[i, j] = v[i] * u[j];
            }
        }
        return A;
    }
}
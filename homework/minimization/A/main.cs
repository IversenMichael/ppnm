using System;
using static System.Console;
using static System.Math;
public class main{
    public static double SqrtEpsilon = Pow(2, -26);

    public static void Main(){
        Func<vector, double> f = delegate (vector x){
            return Pow(x[0] + 1, 4) + Pow(x[1] - 2, 4);
        };

        Func<vector, double> Rosenbrock = delegate (vector x){
            return Pow((1 - x[0]), 2) + 100 * Pow(x[1] - Pow(x[0], 2), 2);
        };

        Func<vector, double> Himmelblau = delegate (vector x){
            return Pow(Pow(x[0], 2) + x[1] - 11, 2) + Pow(x[0] + Pow(x[1], 2) - 7, 2);
        };

        vector x0 = new vector(new double[2] {0, 0});
        double acc = 1e-6;
        vector x_new;
        int n_iter;
    
        (x_new, n_iter) = quasi_newton(f, x0, acc);
        WriteLine("Finding the root of f(x, y) = (x+1)^4 + (y-2)^4");
        WriteLine($"The initial point of the method is (x0, y0) = ({x0[0]}, {x0[1]})");
        WriteLine($"Quasi-Newton method finds the root (x, y) = ({x_new[0]} {x_new[1]}) in {n_iter} iterations");
        WriteLine("The correct root is (x, y) = (-1, 2)");
        WriteLine("");

        (x_new, n_iter) = quasi_newton(Rosenbrock, x0, acc);
        WriteLine("Finding the root of the Rosenbrock's valley function");
        WriteLine($"The initial point of the method is (x0, y0) = ({x0[0]}, {x0[1]})");
        WriteLine($"Quasi-Newton method finds the root (x, y) = ({x_new[0]} {x_new[1]}) in {n_iter} iterations");
        WriteLine("The correct root is (x, y) = (1, 1)");
        WriteLine("");


        (x_new, n_iter) = quasi_newton(Himmelblau, x0, acc);
        WriteLine("Finding the root of the Himmelblau's function");
        WriteLine($"The initial point of the method is (x0, y0) = ({x0[0]}, {x0[1]})");
        WriteLine($"Quasi-Newton method finds the root (x, y) = ({x_new[0]} {x_new[1]}) in {n_iter} iterations");
        WriteLine("The correct roots are: ");
        WriteLine("(x, y) = (3, 2)");
        WriteLine("(x, y) = (-2.80, 3.13)");
        WriteLine("(x, y) = (-3.78, -3.28)");
        WriteLine("(x, y) = (3.58, -1.85)");
        WriteLine("");
    
    }
    public static (vector, int) quasi_newton(Func<vector, double> f, vector x0, double acc){
        // Initialization
        double lambda, denom, alpha = 1e-4, eps = 1e-6;
        int n_iter = 0, max_iter = 100000, size = x0.size;    
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
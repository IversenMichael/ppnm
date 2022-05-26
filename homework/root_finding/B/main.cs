using System;
using static System.Console;
using static System.Math;
public static class main{
    // Square root of mashine epsilon
    public static double SqrtEpsilon = Pow(2, -26);

    public static void Main(){

        Func<vector, vector> M = delegate (vector E){
            System.Func<double, vector, vector> f = delegate (double x, vector y){
            return new vector(
                y[1], 
                -2*(1/x + E[0])*y[0]
                );
            };
            double r_min = 1;
            double r_max = 8;
            vector y0 = new vector(r_min - r_min*r_min, 1 - 2*r_min);
            var rk = new runge_kutta_12(f, r_min, r_max, y0);
            double x_solution;
            vector y_solution;
            (x_solution, y_solution) = rk.driver();
            return new vector(y_solution[0]);
        };

        // Starting point
        vector E0 = new vector(-0.25);
        vector result = Newton(M, E0);
        result.print();
        M(result).print();
    
    }

    static vector Newton(Func<vector,vector> f, vector x0, double eps=1e-3){
        int iter_limit = 10;        // Limit on the total number of iterations
        int n_iter = 0;             // Number of iterations before finding a solution
        matrix J;                   // Jacobian matrix
        QRGS solver;                // QR solver via Gram schmidt
        vector b;
        vector delta_x;
        double lambda;
        for (int i=0; i<iter_limit; i++){   // Do Newton steps
            WriteLine($"E = {x0[0]}");
            if (f(x0).norm() < eps){        // If |f(x0)| < epsilon we return.
                WriteLine($"Newton's method succeeded in {n_iter} iterations");
                return x0;
                }
            n_iter += 1;                    // Counter the number of iterations
            J = Jacobian(f, x0);
            solver = new QRGS(J);
            b = -f(x0);
            delta_x = solver.solve(b);
            lambda = 1.0;
            // Back-tracking linesearch
            while ((f(x0 + lambda * delta_x).norm() > (1 - lambda/2)*f(x0).norm()) & (lambda >= 1.0/32)){lambda = lambda/2;}
            x0 = x0 + lambda * delta_x;
        }
        // Newton's method failed in iter_limit steps
        WriteLine($"Newton's method failed in {iter_limit} interations");
        return x0;
    }

    static matrix Jacobian(Func<vector, vector> f, vector x0){
        // Matrix size
        int size1 = f(x0).size;
        int size2 = x0.size;

        // Jacobian matrix
        matrix J = new matrix(size1, size2);

        // Step size for finite difference approximation of Jacobian
        double dx = SqrtEpsilon * Sqrt(x0.dot(x0));

        // x0 + dx
        vector x0dx;

        // Construct Jacobian matrix
        for (int i=0; i<size1; i++){
            for (int j=0; j<size2; j++){
                x0dx = x0.copy();
                x0dx[j] += dx; 
                J[i, j] = (f(x0dx)[i] - f(x0)[i])/dx;
            }
        }
        return J;
    }
}
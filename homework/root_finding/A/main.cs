using System;
using static System.Console;
using static System.Math;
public static class main{
    // Square root of mashine epsilon
    public static double SqrtEpsilon = Pow(2, -26);

    public static void Main(){
        //  Trial function
        WriteLine("We consider the following non-linear set of equations:");
        WriteLine("(sin(x - y), cos(z^2 + y), x*tan(z) - y^2) = (0, 0, 0)");
        Func<vector, vector> f = delegate(vector x){
            return new vector(
                Sin(x[0] - x[1]), 
                Cos(x[2]*x[2] + x[1]), 
                x[0]*Tan(x[2])- x[1]*x[1]
                );
        };

        // Starting point
        vector x0 = new vector(1.23, -11.1, 9.87);

        vector result = Newton(f, x0);
        WriteLine("The root to this function is");
        result.print();
        WriteLine("Inserting this point, the left hand side of the non-linear equations yield:");
        f(result).print();

        // Partial derivatives of Rosenbrock's valley function,
        WriteLine("");
        WriteLine("Next, we consider the RosenBrocks valley function:");
        WriteLine("f(x, y) = (1 - x)^2 + 100*(y - x^2)^2");
        Func<vector, vector> g = delegate(vector x){
            return new vector(
                200 * x[0]*x[0]*x[0] - 200*x[0]*x[1] + x[0] - 1,
                x[1] - x[0] * x[0]
            );
        };
        // Starting point
        x0 = new vector(1.23, 9.87);

        result = Newton(g, x0);
        WriteLine("The root to the Rosenbrock's valley function is");
        result.print();
        WriteLine("The function value at this point is");
        g(result).print();
    }

    static vector Newton(Func<vector,vector> f, vector x0, double eps=1e-6){
        int iter_limit = 100_000;    // Limit on the total number of iterations
        int n_iter = 0;             // Number of iterations before finding a solution
        matrix J;                   // Jacobian matrix
        QRGS solver;                // QR solver via Gram schmidt
        vector b;
        vector delta_x;
        double lambda;
        for (int i=0; i<iter_limit; i++){   // Do Newton steps
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

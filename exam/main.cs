using System;
using static System.Console;
using static System.Math;
using static System.Random;
using System.IO;
using System.Diagnostics;
public static class main{
    public static void Main(){
        //get_performance_data();
        test_code();
    }

    public static vector find_eigenvalues(matrix D, vector u, double sigma){       
        // FINDS THE EIGENVALUES AFTER A RANK-1 UPDATE
        vector d = new vector(D.size1);
        for (int i=0; i<d.size; i++){
            d[i] = D[i, i];
        }
        // Secular equation
        Func<double, double> f = delegate(double x){
            double y = 0;
            for (int i=0; i<d.size; i++){
                y += (u[i] * u[i]) / (d[i] - x);
            }
            y *= sigma;
            y += 1;
            return y;
            };

        // Derivative of the secular equation
        Func<double, double> fp = delegate(double x){
            double y = 0;
            for (int i=0; i<d.size; i++){
                y += (u[i] * u[i]) / Pow(d[i] - x, 2);
            }
            y *= sigma;
            return y;
        };
        
        // The i'th eigenvalue is found in the interval [x_min[i], x_max[i]]
        double[] x_min = new double[d.size];
        double[] x_max = new double[d.size];
        if (Sign(sigma) > 0){
            for (int i=0; i<x_min.Length-1; i++){
                x_min[i] = d[i];
                x_max[i] = d[i+1];
            }
            x_min[d.size-1] = d[d.size-1];
            x_max[d.size-1] = d[d.size-1] + sigma*u.dot(u);

        }
        else {
            x_min[0] = d[0] + sigma * u.dot(u);
            x_max[0] = d[0];
            for (int i=1; i<x_min.Length; i++){
                x_min[i] = d[i-1];
                x_max[i] = d[i];
            }
        }

        // Initial quesses for finding the roots of the secular equation x0[i] = (x_min[i] + x_max[i]) / 2
        double[] x0 = new double[d.size];
        for (int i=0; i<x0.Length; i++){
            x0[i] = (x_min[i] + x_max[i]) / 2;
        }

        // New eigenvalues
        vector d_new = new vector(d.size);

        for (int i=0; i<d.size; i++){
            d_new[i] = root_finding(f, fp, x0[i], x_min[i], x_max[i]);
        }

        return d_new;
    }

    public static double root_finding(Func<double, double> f, Func<double, double> fp, double x, double x_min, double x_max, double eps=1e-6){
        // NEWTON-RAPHSON METHOD FOR ROOT FINDING
        int iter_limit = 100_000;              // Limit on the total number of iterations
        int n_iter = 0;                     // Number of iterations before finding a solution
        double delta_x;                     // Step
        double lambda;                      // Used in backtracking line search
        double x_next;                       // Used for storing intermediate steps
        for (int i=0; i<iter_limit; i++){   // Do Newton steps
            if (Abs(f(x)) < eps){           // If |f(x0)| < epsilon we return.
                return x;
                }
            n_iter += 1;                    // Increment the number of iterations
            delta_x = - f(x) / fp(x);       // Next step is calculated by Taylor expanding the function
            lambda = 1.0;                   // Initially we take the full step

            // BACK-TRACKING LINE SEARCH
            x_next = x + lambda * delta_x;   // Next step
            while ((Abs(f(x_next)) > Abs((1 - lambda/2)*f(x))) & (lambda >= 1.0/64) | (x_next < x_min) | (x_max < x_next)){ // Armijo–Goldstein condition and check if new point remains in the interval [x_min, x_max]
                lambda = lambda / 2;            // Decrease lambda
                x_next = x + lambda * delta_x;  // Update new point
                }
            
            x = x_next; // We accept the step 
            }
        
        // NEWTON-RAPHSON METHOD FAILED IN iter_limit STEPS
        throw new IndexOutOfRangeException($"Newton-raphson method did not converge in {iter_limit} steps");
    }

    public static void test_code(){
        var test_writer = new System.IO.StreamWriter("test_results.txt");
        test_writer.WriteLine("-----------------------------------------------");
        test_writer.WriteLine("TESTING THE DIAGONALIZATION OF A RANK 1 UPDATE");
        test_writer.WriteLine("-----------------------------------------------");
        test_writer.WriteLine("We diagonalize random matrices of different sizes.");
        test_writer.WriteLine($"The difference between the computed eigenvalues and the exact eigenvalues is determined (should be close to 0).");
        int seed = 0;
        int[] sizes = new int[6] {2, 4, 8, 10, 50, 100};
        int size;
        matrix D, A;
        vector u, d_rank1, d_jac;
        jacobi jac;
        double sigma;

        for (int i=0; i<sizes.Length; i++){
            size = sizes[i];
            test_writer.Write($"Size = {size}:\t");
            (D, u, sigma) = get_random_rank1_update(size, seed);
            d_rank1 = find_eigenvalues(D, u, sigma);

            A = D + sigma*matrix.outer(u, u);
            jac = new jacobi(A);
            jac.cyclic();
            d_jac = new vector(jac.D.diag());

            test_writer.WriteLine($"Error = {Round((d_rank1 - d_jac).norm(), 5)}");
        }

        test_writer.WriteLine();
        test_writer.WriteLine("We call the method with various special cases");
        D = new matrix(2, 2, mode:"identity");
        D.print();
        u = new vector(new double[2] {1, 1});
        sigma = 1;
        //d_rank1 = find_eigenvalues(D, u, sigma);
        test_writer.Close();
    }

    public static (matrix, vector, double) get_random_rank1_update(int size, int seed){
        var rand = new Random(seed);
        double[] D_diag = new double[size];
        vector u = new vector(size);
        for (int i=0; i<size; i++){
            D_diag[i] = rand.NextDouble();
            u[i] = rand.NextDouble();
        }
        Array.Sort(D_diag);
        matrix D = new matrix(size, size, mode:"zeros");
        for (int i=0; i<size; i++){
            D[i, i] = D_diag[i];
        }
        double sigma = rand.NextDouble();
        return (D, u, sigma);
    }
    public static void get_performance_data(){
        // Test performance
        int[] sizes = new int[6];
        for(int i=0; i<6; i++){
            sizes[i] = 2 + 25*i;
        }
        var rank1_writer = new StreamWriter("rank1_times.txt");
        var jac_writer = new StreamWriter("jac_times.txt");
        (double[] rank1_times, double[] jac_times) = measure_performance(sizes);
        for (int i=0; i<rank1_times.Length; i++){
            rank1_writer.WriteLine($"{sizes[i]} {rank1_times[i]} {rank1_times[i]/((double) sizes[i]*sizes[i])}");
            jac_writer.WriteLine($"{sizes[i]} {jac_times[i]} {jac_times[i]/((double) sizes[i]*sizes[i])}");
        }
        rank1_writer.Close();
        jac_writer.Close();
    }
    
    public static (double[], double[]) measure_performance(int[] sizes, int N_rep=10){
       /* Setup */
        int seed = 0;
        int size;
        Random rand = new Random(seed);
        double[] D_diag;
        vector u;
        matrix D;
        double sigma;
        matrix A;
        vector d_new;
        jacobi jac;
        Stopwatch stopwatch;
        double[] rank1_times = new double[sizes.Length];
        double[] jac_times = new double[sizes.Length];
        for(int i=0; i<sizes.Length; i++){
            size = sizes[i];
            (D, u, sigma) = get_random_rank1_update(size, seed);
            A = D + sigma*matrix.outer(u, u);

            /* Measuring performance */

            // Rank 2 update method
            for (int k=0; k<N_rep; k++){
                stopwatch = Stopwatch.StartNew();
                d_new = find_eigenvalues(D, u, sigma);
                stopwatch.Stop();
                rank1_times[i] += (double)stopwatch.ElapsedTicks/(double)Stopwatch.Frequency;
            }
            rank1_times[i] /= (double)N_rep;

            // Normal Jacobi method
            for (int k=0; k<N_rep; k++){
                jac = new jacobi(A);
                stopwatch = Stopwatch.StartNew();
                jac.cyclic();
                stopwatch.Stop();
                jac_times[i] += (double)stopwatch.ElapsedTicks/(double)Stopwatch.Frequency;
            }
            jac_times[i] /= (double)N_rep;
        }
        return (rank1_times, jac_times);
    }
}
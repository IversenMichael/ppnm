using System;
using static System.Console;
using static System.Math;
using static System.Random;
using System.IO;
using System.Diagnostics;
public static class main{
    public static void Main(){
        measure_performance();
        test_code();

    }

    public static vector find_eigenvalues(matrix D, vector u, double sigma){       
        /* 
        This method computes the new eigenvalues after a rank-1 update.
        Given a diagonal matrix D, vector u and double sigma, the eigenvalues of A = D + sigma*u*u^T is computed.
        */

        // Extracts the diagonal of the matrix D into the vector d
        vector d = new vector(D.size1);
        for (int i=0; i<d.size; i++){
            d[i] = D[i, i];
        }

        // Define secular equation
        Func<double, double> f = delegate(double x){
            double y = 0;
            for (int i=0; i<d.size; i++){
                y += (u[i] * u[i]) / (d[i] - x);
            }
            y *= sigma;
            y += 1;
            return y;
            };

        // Define derivative of the secular equation
        Func<double, double> fp = delegate(double x){
            double y = 0;
            for (int i=0; i<d.size; i++){
                y += (u[i] * u[i]) / Pow(d[i] - x, 2);
            }
            y *= sigma;
            return y;
        };
        
        // The i'th eigenvalue is found in the interval [x_min[i], x_max[i]]
        // We store the left and right boundaries of these intervals in arrays x_min and x_max
        double[] x_min = new double[d.size];
        double[] x_max = new double[d.size];
        if (Sign(sigma) > 0){   // Note that these intervals depends on the sign on sigma
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

        // We use the midpoint on each interval as the starting point of the root finding algorithm
        double[] x0 = new double[d.size];
        for (int i=0; i<x0.Length; i++){
            x0[i] = (x_min[i] + x_max[i]) / 2;
        }

        // We compute the new eigenvalues by finding the root of the secular equation in each interval
        vector d_new = new vector(d.size);  // Container for the new eigenvalues
        for (int i=0; i<d.size; i++){
            if (Abs(u[i]) < 1e-9){  // If the i'th entry in u is zero, then the i'th eigenvalue is preserved
                d_new[i] = d[i];
            }
            else{   // Otherwise, we find the new eigenvalue by searching for the root
                d_new[i] = root_finding(f, fp, x0[i], x_min[i], x_max[i]);
            }
        }

        return d_new;
    }

    public static double root_finding(Func<double, double> f, Func<double, double> fp, double x, double x_min, double x_max, double eps=1e-6){
        /*
        Finds a root of function f with derivative fp in the interval [x_min, x_max] using the initial guess x.
        The method determines a root using the Newton-Raphson method.
        */
        if (Abs(x_min - x_max) < 1e-9){ // If x_min = x_max, then the root is in this point
            return x;
        }
        
        // NEWTON-RAPHSON METHOD FOR ROOT FINDING
        int iter_limit = 100_000;           // Limit on the total number of iterations
        double delta_x;                     // Step
        double lambda;                      // Used in backtracking line search
        double x_next;                      // Used for storing intermediate steps

        for (int i=0; i<iter_limit; i++){   // Do Newton steps
            if (Abs(f(x)) < eps){           // If |f(x)| < epsilon we return.
                return x;
                }
            delta_x = - f(x) / fp(x);       // Next step is calculated by Taylor expanding the function
            lambda = 1.0;                   // Initially we take the full step

            // BACK-TRACKING LINE SEARCH
            x_next = x + lambda * delta_x;      // Next step
            while (Abs(f(x_next)) > Abs((1 - lambda/2)*f(x)) & lambda >= 1.0/128 | x_next < x_min | x_max < x_next){ // Armijo–Goldstein condition and check if new point remains in the interval [x_min, x_max]
                lambda = lambda / 2;            // Decrease lambda
                x_next = x + lambda * delta_x;  // Update new point
                }

            x = x_next; // We accept the step 
            }
        
        // If the code reaches this point, then the Newton-Raphson method failed
        throw new IndexOutOfRangeException($"Newton-Raphson method did not converge in {iter_limit} steps\nThe function was called with values x = {x}, x_min = {x_min}, x_max = {x_max}");
    }

    public static void test_code(){
        /*
        This method tests if "find_eigenvalues" correctly determines the eigenvalues by comparing it to the output of the jacobi algorithm from homework "eigenvalues".
        The tests are seperated into two categories. 
        First, we test if the code works correctly on random matrices of different sizes
        Then, we test if the code works correctly in various special cases (negative sigma, trivial u vector, matrix D with degenerate eigenvalues).
        */

        var test_writer = new System.IO.StreamWriter("test_results.txt");
        test_writer.WriteLine("-----------------------------------------------");
        test_writer.WriteLine("TESTING THE DIAGONALIZATION OF A RANK 1 UPDATE");
        test_writer.WriteLine("-----------------------------------------------");
        test_writer.WriteLine("");

        // Diagonalization of different matrix sizes
        test_writer.WriteLine("DIAGONALIZE MATRICES OF DIFFERENT SIZES");
        test_writer.WriteLine($"The difference between the computed eigenvalues and the exact eigenvalues (should be close to 0).");
        int[] sizes = new int[5] {2, 4, 8, 10, 100};    // The matrix sizes we test
        
        // Defining some quantities used below
        int size;
        matrix D, A;
        vector u, d_rank1, d_jac;
        jacobi jac;
        double sigma;
        
        for (int i=0; i<sizes.Length; i++){ // Loop through all matrix sizes
            size = sizes[i];    // Current matrix size
            test_writer.Write($"Size = {size}:\t");
            (D, u, sigma) = get_random_rank1_update(size, i);    // Returns random D, u and sigma
            d_rank1 = find_eigenvalues(D, u, sigma);    // Applies the code to this situation

            // We compare the eigenvalues to the results of the Jacobi algorithm
            A = D + sigma*matrix.outer(u, u);
            jac = new jacobi(A);
            jac.cyclic();
            d_jac = new vector(jac.D.diag());   // Eigenvalues computed from the Jacobi algorithm

            test_writer.WriteLine($"Error = {(d_rank1 - d_jac).norm()}");   // Print the difference between the two methods
        }
        test_writer.WriteLine();

        // Testing special cases
        // Negative sigma value
        test_writer.WriteLine("TESTING SPECIAL CASES");
        test_writer.WriteLine("Negative sigma value");
        D = new matrix(2, 2, mode:"zeros");
        D[0, 0] = -12;
        D[1, 1] = 99;
        test_writer.WriteLine($"D = ");
        test_writer.WriteLine($"({D[0, 0]}\t{D[0, 1]})\n({D[1, 0]}\t{D[1, 1]})");
        u = new vector(new double[2] {1, -2});
        test_writer.WriteLine($"u = ({u[0]}, {u[1]})");
        sigma = -1.23;
        test_writer.WriteLine($"sigma = {sigma}");
        d_rank1 = find_eigenvalues(D, u, sigma);
        test_writer.WriteLine($"The computed eigenvalues are = ({d_rank1[0]}, {d_rank1[1]})");
        
        jac = new jacobi(D + sigma*matrix.outer(u, u));
        jac.cyclic();
        test_writer.WriteLine($"The exact eigenvalues are = ({jac.D[0, 0]}, {jac.D[1, 1]})");
        test_writer.WriteLine("");

        // Trivial u vector
        test_writer.WriteLine("u vector with a trivial entry");
        test_writer.WriteLine($"D = ");
        test_writer.WriteLine($"({D[0, 0]}\t{D[0, 1]})\n({D[1, 0]}\t{D[1, 1]})");
        u = new vector(new double[2] {0, 1});
        test_writer.WriteLine($"u = ({u[0]}, {u[1]})");
        sigma = 1;
        test_writer.WriteLine($"sigma = {sigma}");
        d_rank1 = find_eigenvalues(D, u, sigma);
        test_writer.WriteLine($"The computed eigenvalues are = ({d_rank1[0]}, {d_rank1[1]})");
        
        jac = new jacobi(D + sigma*matrix.outer(u, u));
        jac.cyclic();
        test_writer.WriteLine($"The exact eigenvalues are = ({jac.D[0, 0]}, {jac.D[1, 1]})");
        test_writer.WriteLine("");

        // D matrix with degenerate eigenvalues
        test_writer.WriteLine("Matrix with degenerate eigenvalues");
        D = new matrix(2, 2, mode:"identity");
        test_writer.WriteLine($"D = ");
        test_writer.WriteLine($"({D[0, 0]}\t{D[0, 1]})\n({D[1, 0]}\t{D[1, 1]})");
        u = new vector(new double[2] {1.23, 9.87});
        test_writer.WriteLine($"u = ({u[0]}, {u[1]})");
        sigma = 1;
        test_writer.WriteLine($"sigma = {sigma}");
        d_rank1 = find_eigenvalues(D, u, sigma);
        test_writer.WriteLine($"The computed eigenvalues are = ({d_rank1[0]}, {d_rank1[1]})");
        
        jac = new jacobi(D + sigma*matrix.outer(u, u));
        jac.cyclic();
        test_writer.WriteLine($"The exact eigenvalues are = ({jac.D[0, 0]}, {jac.D[1, 1]})");
        test_writer.Close();
    }

    public static void measure_performance(int N_rep=10){
        /*
        Runs the code with different systems sizes and measures the ellapsed time.
        The algorithm should take O(n^2) operations, so we also expect to scale quadratically with time, i.e. O(t^2).
        For comparison we also compute the time of the Jacobi algorithm which scales as O(n^3).
        */

        // The sizes for which we test performance
        int[] sizes = new int[8];
        for(int i=0; i<8; i++){
            sizes[i] = 2 + 10*i;
        }

        // Defining quantities used below
        int size;
        double[] D_diag;
        vector u, d_new;
        matrix D, A;
        double sigma;
        jacobi jac;
        Stopwatch stopwatch;

        // Containers for storing the ellapsed times
        double[] rank1_times = new double[sizes.Length];    // Rank-1 update method, i.e. "find_eigenvalues" from above
        double[] jac_times = new double[sizes.Length];      // Jacobi eigenvalue algorithm

        for(int i=0; i<sizes.Length; i++){  // Loop through all matrix sizes
            size = sizes[i];    // Current matrix size
            (D, u, sigma) = get_random_rank1_update(size, i);   // Get a random matrix D, vector u and double sigma
            A = D + sigma*matrix.outer(u, u);   // Compute the matrix we with to diagonalize

            // We measure the performance when diagonalizing the matrix A using the "rank-1 update method" and Jacobi algorithm

            // Rank 1 update method
            for (int k=0; k<N_rep; k++){
                stopwatch = Stopwatch.StartNew();
                d_new = find_eigenvalues(D, u, sigma);
                stopwatch.Stop();
                rank1_times[i] += (double)stopwatch.ElapsedTicks/(double)Stopwatch.Frequency;
            }
            rank1_times[i] /= (double)N_rep;

            // Jacobi eigenvalue algortihm
            for (int k=0; k<N_rep; k++){
                jac = new jacobi(A);
                stopwatch = Stopwatch.StartNew();
                jac.cyclic();
                stopwatch.Stop();
                jac_times[i] += (double)stopwatch.ElapsedTicks/(double)Stopwatch.Frequency;
            }
            jac_times[i] /= (double)N_rep;
        }

        // Save data in two files
        var rank1_writer = new StreamWriter("rank1_times.txt");
        var jac_writer = new StreamWriter("jac_times.txt");
        for (int i=0; i<rank1_times.Length; i++){
            rank1_writer.WriteLine($"{sizes[i]} {rank1_times[i]} {rank1_times[i]/((double) sizes[i]*sizes[i])}");
            jac_writer.WriteLine($"{sizes[i]} {jac_times[i]} {jac_times[i]/((double) sizes[i]*sizes[i])}");
        }
        rank1_writer.Close();
        jac_writer.Close();
    }
    
    public static (matrix, vector, double) get_random_rank1_update(int size, int seed){
        /*
        Helper method used in testing.
        Computes a random diagonal matrix D, random vector u and random double sigma.
        The random number generator uses the given seed.
        */
        var rand = new Random(seed);    // Instance of Random to compute random doubles
        double[] D_diag = new double[size]; // Diagonal elements of matrix D
        vector u = new vector(size);        // vector u
        for (int i=0; i<size; i++){         // Loop through all elements of D and u
            D_diag[i] = rand.NextDouble();  // Append a random double
            u[i] = rand.NextDouble();       // --||--
        }
        Array.Sort(D_diag); // Sort the diagonal of D
        matrix D = new matrix(size, size, mode:"zeros");    // Matrix with sizes everywhere
        for (int i=0; i<size; i++){ // Inserts the diagonal elements in the diagonal of D
            D[i, i] = D_diag[i];
        }
        double sigma = rand.NextDouble();   // Finds a random sigma value
        return (D, u, sigma);
    }
    
}
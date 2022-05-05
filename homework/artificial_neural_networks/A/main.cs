using System;
using static System.Console;
using static System.Math;
public static class main{
    public static void Main(){
        var train_data = new System.IO.StreamWriter("train_data.txt",append:true);
        var fit_data = new System.IO.StreamWriter("fit_data.txt",append:true);
        var correct_data = new System.IO.StreamWriter("correct_data.txt",append:true);
        int n_data = 8;
        double[] x_train = new double[n_data];
        double[] y_train = new double[n_data];
        double x_min = -1.0;
        double x_max = 1.0;
        Func<double, double> g = delegate(double x){return Cos(5 * x - 1) * Exp(-x*x);};
        for(int i=0; i<n_data; i++){
            x_train[i] = x_min + i * (x_max - x_min) / (n_data - 1);
            y_train[i] = g(x_train[i]);
            train_data.WriteLine($"{x_train[i]} {y_train[i]}");
        }
        
        int n = 3;
        Func<double, double> f = delegate(double x){return x*Exp(-x*x);};
        ann network = new ann(n, f);
        network.train(x_train, y_train);

        int n_fit = 500;
        double[] x_fit = new double[n_fit];
        double[] y_fit = new double[n_fit];
        double[] y_correct = new double[n_fit];
        for(int i=0; i<n_fit; i++){
            x_fit[i] = x_min + i * (x_max - x_min) / (n_fit - 1);
            y_correct[i] = g(x_fit[i]);
            y_fit[i] = network.response(x_fit[i]);
            fit_data.WriteLine($"{x_fit[i]} {y_fit[i]}");
            correct_data.WriteLine($"{x_fit[i]} {y_correct[i]}");
        }
        train_data.Close();
        fit_data.Close();
        correct_data.Close();

        WriteLine("We consider the function g(x) = cos(5x-1)*exp(-x^2)");
        WriteLine($"Neural network with 3 hidden neurons has been trained to interpolate the function");
        WriteLine($"We use the following training points");
        for(int i=0; i<n_data; i++){
            WriteLine($"(x, y) = ({x_train[i]}, {y_train[i]})");
        }
        WriteLine("We find the following fitting parameters");
        for(int i=0; i<n; i++){
            WriteLine($"a_{i} = {network.p[i]}");
        }
        for(int i=0; i<n; i++){
            WriteLine($"b_{i} = {network.p[n+i]}");
        }
        for(int i=0; i<n; i++){
            WriteLine($"w_{i} = {network.p[2*n+i]}");
        }
        WriteLine("The interpolation is illustrated in the figure plot.png");
        WriteLine("This figure shows the interpolation, exact function and training points");
    }

}
using System;
using System.Threading;
using System.Diagnostics;
using static System.Console;
using static System.Math;

class data{
    public int a;
	public int b;
	public double sum;
	}
public static class main{
public static void harmonic_sum(object obj){
	data x = (data) obj;
	x.sum = .0; 
	for(int i=x.a; i<=x.b; i++){
		x.sum += 1.0/i;
		}
	}

	public static void Main(){
		WriteLine("FINDING THE HARMONIC SUM USING A SINGLE TREAD");
		data x = new data();
		x.a = 1;
		x.b = (int) Pow(10, 9);
		Thread t = new Thread(harmonic_sum);

		Stopwatch stopwatch = new Stopwatch();
		stopwatch.Start();
		t.Start(x);
		t.Join();
		stopwatch.Stop();
		WriteLine($"Harmonic sum from {x.a} to {x.b} equals {x.sum}");
		WriteLine($"The computation took t = {stopwatch.ElapsedMilliseconds} ms");
		WriteLine();

		WriteLine("FINDING THE HARMONIC SUM USING TWO TREADS");
		data x1 = new data();
		x1.a = 1;
		x1.b = (int) Pow(10, 9)/2;
		Thread t1 = new Thread(harmonic_sum);
		data x2 = new data();
		x2.a = (int) Pow(10, 9)/2;
		x2.b = (int) Pow(10, 9);
		Thread t2 = new Thread(harmonic_sum);

		stopwatch = new Stopwatch();
		stopwatch.Start();
		t1.Start(x1);
		t2.Start(x2);
		t1.Join();
		t2.Join();
		stopwatch.Stop();

		WriteLine($"Harmonic sum from {x1.a} to {x2.b} equals {x1.sum + x2.sum}");
		WriteLine($"The computation took t = {stopwatch.ElapsedMilliseconds} ms (This time should be approximately half of the time using only one thread).");
	}
}


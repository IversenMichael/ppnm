using System;
using System.Threading;
using static System.Console;
using static System.Math;
public static class main{
class data{
       	public int a, b;
	public double sum;
	}

public static void harmonic_sum(object obj){
	data x = (data) obj;
	x.sum = 0; 
	for(int i=x.a; i<=x.b; i++){
		x.sum += 1.0/i;
		}
	}

public static void Main(){
	WriteLine("Finding harmonic sum using a single thread");
	data x = new data();
	x.a = 1;
       	x.b = (int) Pow(10, 9);
	Thread t = new Thread(harmonic_sum);
	t.Start(x);
	t.Join();
	WriteLine($"harmonic sum from {x.a} to {x.b} equals {x.sum}");

	WriteLine("Finding harmonic sun using two threads");
	data x1 = new data();
	x1.a = 1;
	x1.b = (int) Pow(10, 9)/2;
	Thread t1 = new Thread(harmonic_sum);
	data x2 = new data();
	x2.a = (int) Pow(10, 9)/2;
	x2.b = (int) Pow(10, 9);
	Thread t2 = new Thread(harmonic_sum);

	t1.Start(x1);
	t2.Start(x2);
	t1.Join();
	t2.Join();
	
	WriteLine($"harmonic sum from {x1.a} to {x2.b} equals {x1.sum + x2.sum}");
}
}


using System;
using System.Threading;
using static System.Console;
class main{
	public class data{public int a, b; public double sum;}

	public static void harm(object obj){
		data d = (data)obj;
		WriteLine($"Summing from {d.a} to {d.b}");
		d.sum=0;
		for(int i=d.a; i<d.b; i++){
			d.sum += 1.0/i;
		}
		WriteLine($"Sum is equal to sum = {d.sum}");
	}
	public static void Main(){
		int N = (int)1e8;
		WriteLine($"N = {(float)N}");
		data x = new data();
		data y = new data();
		x.a = 1;
		x.b = N/2;
		y.a = N/2;
		y.b = N + 1;
		Thread t1 = new Thread(harm);
		Thread t2 = new Thread(harm);
		t1.Start(x);
		t2.Start(y);
		t1.Join();
		t2.Join();
		WriteLine($"harm sum from {x.a} to {y.b} is equal to {x.sum+y.sum}");
	}
}

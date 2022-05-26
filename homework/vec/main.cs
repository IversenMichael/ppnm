using System;
using static System.Console;
using static System.Math;
using static vec;
static public class main{
	static void Main(){
		// Part A
		WriteLine("PART A");
		vec u = new vec();
		u.print("Using constructor without input: u = ");
		vec v = new vec(1, 2, 3);
		v.print("Using constructor with input 1, 2, 3: v = ");
		vec w = new vec(4, 5, 6);
		w.print("Defining yet another vector for illustrating arithmetic: w = ");
		(v + w).print("Adding v and w: v + w = ");
		(v - w).print("Subtracting v and w: v - w = ");
		double c = 3.0;
		vec left_product = c * v;
		left_product.print("Multiplying v by c from the left: c * v = ");
		vec right_product = v * c;
		right_product.print("Multiplying v by c from the right: v * c = ");
		WriteLine();

		// Part B
		WriteLine("PART B");
		double d = dot(v, w);
		WriteLine($"Dot product between v and w: v dot w = {d}");
		double n = norm(v);
		WriteLine($"Norm of vector v: norm(v) = {n}");
		WriteLine();

		// Part C
		WriteLine("PART C");
		WriteLine("I have overwritten the ToString method. When called from v it now returns: " + v.ToString());
		WriteLine($"Approximate method compares v and u: {v.approx(u)}");
		WriteLine($"Approximate method compares v with itself: {v.approx(v)}");
	}
}

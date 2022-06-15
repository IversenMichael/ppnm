// (C) 2020 Dmitri Fedorov; License: GNU GPL v3+; no warranty.
// Starting from Dmitris class, I have modified vector.cs slightly to better fit the current project.

using System;
using static System.Math;
using static System.Console;
public partial class vector{

private double[] data;

public double this[int i]{
	get{
		return data[i];
		}
	set{
		data[i] = value;
		}
}

public int size{
	get{return data.Length;}
}

public vector(int n){
	data = new double[n];
}

public vector(double[] a){
	data = a;
}

public static vector operator+ (vector v, vector u){
	vector w = new vector(v.size);
	for(int i=0; i<w.size; i++){
		w[i] = v[i] + u[i];
	}
	return w; 
}

public static vector operator- (vector v){
	vector w = new vector(v.size);
	for(int i=0; i<w.size; i++){
		w[i] = -v[i];
	}
	return w; 
}

public static vector operator- (vector v, vector u){
	vector w = new vector(v.size);
	for(int i=0; i<w.size; i++){
		w[i] = v[i] - u[i];
	}
	return w; 
}

public static vector operator* (vector v, double a){
	vector w = new vector(v.size);
	for(int i=0; i<v.size; i++){
		w[i] = a*v[i];
	}
	return w; 
}

public static vector operator* (double a, vector v){
	return v * a; 
}

public static vector operator/ (vector v, double a){
	vector w = new vector(v.size);
	for(int i=0; i<v.size; i++){
		w[i] = v[i] / a;
	}
	return w; 
}

public double dot(vector other){
	double dot_product = 0;
	for(int i=0; i<size; i++){
		dot_product += this[i] * other[i];
	}
	return dot_product;
	}

public double norm(){
	double norm_squared = 0;
	for(int i=0; i<size; i++){
		norm_squared += this[i] * this[i];
	}
	return Sqrt(norm_squared);
}
public vector round(int decimal_places=3){
	vector v = new vector(this.size);
	for (int i=0; i<this.size; i++){
		v[i] = Round(this[i], decimal_places);
	}
	return v;
}

public void print(){
	for (int i=0; i<this.size; i++){
		WriteLine($"{Round(this[i], 3)}");
	}
	WriteLine();
}

}
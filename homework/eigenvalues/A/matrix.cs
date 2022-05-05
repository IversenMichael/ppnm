using static System.Console;
using static System.Math;
using System;
public class matrix{
	public readonly int size1, size2;
	private double[,] data;
	
	/* Constructor */
	public matrix(int n, int m, string mode="empty"){
		size1 = n;
		size2 = m;
		data = new double[size1, size2];

		/* Initialize a random matrix */
		if (mode=="random"){
			var rand = new Random();
			for (int i=0; i<size1; i++){
				for (int j=0; j<size2; j++){
					data[i, j] = rand.NextDouble();
				}
			}
		}
		if (mode=="zeros"){
			for (int i=0; i<size1; i++){
				for (int j=0; j<size2; j++){
					data[i, j] = 0;
				}
			}
		}
		if (mode=="identity"){
			for (int i=0; i<size1; i++){
				for (int j=0; j<size2; j++){
					if(i==j){
						data[i, j] = 1;
					}
					else{
						data[i, j] = 0;
					}
				}
			}
		}
	}

	/* Indexer */
	public double this[int i, int j]{
		get => data[i, j];
		set => data[i, j] = value;
	}

	/* Plus */
	public static matrix operator + (matrix A, matrix B){
		var C = new matrix(A.size1, A.size2);
		for (int i=0; i<A.size1; i++){
			for (int j=0; j<A.size2; j++){
				C[i, j] = A[i, j] + B[i, j];
			}
		}
		return C;
	}

	/* Minus */
	public static matrix operator - (matrix A, matrix B){
		return A + ((-1.0) * B);
	}
	
	/* Scalar multiplication */
	public static matrix operator * (matrix A, double k){
		var C = new matrix(A.size1, A.size2);
		for (int i=0; i<A.size1; i++){
			for (int j=0; j<A.size2; j++){
				C[i, j] = k * A[i, j];
			}
		}
		return C;
	}
	public static matrix operator * (double k, matrix A){
		return A*k;
	}
	
	/* Matrix multiplication */
	public static matrix operator * (matrix A, matrix B){
		var C = new matrix(A.size1, B.size2);
		for (int i=0; i<A.size1; i++){
			for (int j=0; j<B.size2; j++){
				C[i, j] = 0;
				for (int k=0; k<A.size2; k++){
					C[i, j] += A[i, k] * B[k, j];
				}
			}
		}
		return C;
	}

	/* Multiplication by vector */
	public static vector operator * (matrix A, vector v){
		var w = new vector(A.size1);
		for (int i=0; i<w.size; i++){
			w[i] = 0;
			for (int j=0; j<v.size; j++){
				w[i] += A[i, j] * v[j];
			}
		}
		return w;
	}	
	
	public void print(){
		for(int i=0; i<size1; i++){
			for(int j=0; j<size2; j++){
				Write($"{Round(data[i, j], 3)}\t");
			}
			WriteLine();
		}
			WriteLine();
	}
	
	public matrix transpose(){
		matrix A = new matrix(this.size2, this.size1);
		for (int i=0; i<this.size2; i++){
			for (int j=0; j<this.size1; j++){
				A[i, j] = this[j, i];
			}
		}
		return A;
	}

	public double[] diag(){
		double[] diagonal = new double[size1];
		for (int i=0; i<size1; i++){
			diagonal[i] = data[i, i];
		}
		return diagonal;
	}	

	
	
	public matrix round(){
		matrix A = new matrix(this.size1, this.size2);
		for (int i=0; i<this.size1; i++){
			for (int j=0; j<this.size2; j++){
				A[i, j] = Round(this[i, j], 3);
			}
		}
		return A;
	}

	public matrix copy(){
		matrix A = new matrix(this.size1, this.size2);
		for (int i=0; i<this.size1; i++){
			for (int j=0; j<this.size2; j++){
				A[i, j] = this[i, j];
			}
		}
		return A;
	}

}

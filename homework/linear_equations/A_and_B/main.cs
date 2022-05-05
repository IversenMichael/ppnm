using static System.Console;
using static System.Random;
using static System.Math;
public static class main{
	public static void Main(){
		var rand = new System.Random();
		int size1 = (int) 10;
		int size2 = (int) 5;
		var A = new matrix(size1, size2);
		for (int i=0; i<size1; i++){
			for (int j=0; j<size2; j++){
				A[i, j] = rand.NextDouble();
			}
		}

		QRGS QR = new QRGS(A);

		WriteLine("Is A = QR?\n");
		WriteLine("A - QR = ");
		(A - QR.Q * QR.R).round().print();
		WriteLine();

		WriteLine("Is R upper triangular?\n");
		WriteLine("R = ");
		QR.R.round().print();
		WriteLine();

		WriteLine("Is Q^T * Q = I?\n");
		WriteLine("Q^T * Q = ");
		(QR.Q.transpose() * QR.Q).round().print();
		WriteLine();
		A = new matrix(size1, size1);
		for (int i=0; i<size1; i++){
			for (int j=0; j<size1; j++){
				A[i, j] = rand.NextDouble();
			}
		}
		double[] rand_array = new double[size1];
		for (int i=0; i<size1; i++){
			rand_array[i] = rand.NextDouble();
		}
		vector b = new vector(rand_array);
		QR = new QRGS(A);
		vector x = QR.solve(b);
		WriteLine("Is Ax = b?");
		WriteLine("Ax - b = ");
		vector Ax_b = A*x - b;
		Ax_b.print();
		WriteLine();
		WriteLine("Is A^(-1) * A = 1?");
		matrix A_inv = QR.inverse();
		matrix AA_inv = A * A_inv;
		WriteLine("A^(-1) * A = ");
		AA_inv.round().print();
	}
}

using System;
using static System.Console;
public class LS{
	matrix A;
	vector b;
	public matrix Sigma;
	public vector c;
	public LS(double[] x, double[] y, double[] dy, Func<double, double>[] f){
		int n = x.Length;
		int m = f.Length;
		A = new matrix(n, m);
		for(int i=0; i<n; i++){
			for(int j=0; j<m; j++){
				A[i, j] = f[j](x[i])/dy[i];
			}
		}
		b = new vector(n);
		for(int i=0; i<n; i++){
			b[i] = y[i]/dy[i];
		}
		QRGS QR = new QRGS(A);
		c = QR.solve(b);

		matrix AA = (A.transpose() * A);
		QRGS QR_AA = new QRGS(AA);
		Sigma = QR_AA.inverse();
	}
}

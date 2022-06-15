using static System.Console;
using static System.Math;
public class jacobi{
	public matrix D;
	public matrix V;
	public jacobi(matrix A){
		D = A.copy();
		V = new matrix(D.size1, D.size2);
		for (int i=0; i<D.size1; i++){
			V[i, i] = 1;
		}
	}

	public void timesJ(matrix A, int p, int q, double theta){
		double ap, aq;
		double c = Cos(theta);
		double s = Sin(theta);
		for (int i=0; i<A.size1; i++){
			ap = A[i, p];
			aq = A[i, q];
			A[i, p] = c*ap - s*aq;
			A[i, q] = s*ap + c*aq;
		}
	}
	
	public void Jtimes(matrix A, int p, int q, double theta){
		double ap, aq;
		double c = Cos(theta);
		double s = Sin(theta);
		for (int j=0; j<A.size2; j++){
			ap = A[p, j];
			aq = A[q, j];
			A[p, j] = c*ap + s*aq;
			A[q, j] = -s*ap + c*aq;
		}
	}

	public void cyclic(){
		double[] eig0 = new double[D.size1];
		double[] eig1 = new double[D.size1];
		double diff = 1;
		while (diff > 1e-9){
			eig0 = D.diag();
			sweep();
			eig1 = D.diag();
		
			diff = 0;
			for (int i=0; i<D.size1; i++){
				diff += Abs(eig1[i] - eig0[i]);
			}
		}
	}
	public void sweep(){
		double theta;
		for (int i=0; i<D.size1; i++){
			for (int j=i+1; j<D.size2; j++){
				theta = 0.5*Atan2(2*D[i, j], D[j, j] - D[i, i]);
				Jtimes(D, i, j, -theta);
				timesJ(D, i, j, theta);
				timesJ(V, i, j, theta);
			}
		
		}
	}
}

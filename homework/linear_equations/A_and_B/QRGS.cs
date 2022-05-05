using static System.Math;
using static System.Console;
public class QRGS{
	public matrix A, Q, R;
	/* Constructor */
	public QRGS(matrix A_init){
		A = A_init.copy();
		matrix A_ = A_init.copy();
		Q = new matrix(A_.size1, A_.size2);
		R = new matrix(A_.size2, A_.size2);
		for(int i=0; i<Q.size2; i++){
			R[i, i] = Sqrt(A_.dot(i, i));
			for(int j=0; j<Q.size1; j++){
				Q[j, i] = A_[j, i] / R[i, i];
			}
			for(int j=i+1; j<Q.size2; j++){
				R[i, j] = Q.dot(A, i, j);
				for(int k=0; k<Q.size1; k++){
				A_[k, j] = A_[k, j] - Q[k, i] * R[i, j];
				}
			}
		}
		
	}

	/*  Solve system of linear equations */
	public vector solve(vector b){
		/* back-substitution */
		vector c = Q.transpose() * b;
		for (int i=c.size-1; i>=0; i--){
			double sum = 0;
			for (int j=i+1; j<c.size; j++){
				sum += R[i, j] * c[j];
			}
			c[i] = (c[i] - sum)/R[i, i];
		}
		return c;
	}

	/* Inverse of A */
	public matrix inverse(){
		matrix A_inv = new matrix(A.size1, A.size2);
		vector x = new vector(A.size2);
		vector ei = new vector(A.size2);
		QRGS QR = new QRGS(A);
		for (int i=0; i<A.size2; i++){
			ei = new vector(A.size2);
			for (int j=0; j<A.size2; j++){
				if (j==i){
					ei[j] = 1;
				}
				else{
					ei[j] = 0;
				}
			}
			x = QR.solve(ei);
			for (int j=0; j<A.size2; j++){
				A_inv[j, i] = x[j];
			}	
		}
		return A_inv;
	}	

}

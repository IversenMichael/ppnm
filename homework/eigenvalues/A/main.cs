using static System.Console;
public static class main{
	public static void Main(){
	WriteLine("PART A");
	WriteLine("Check that the eigenvalue decomposition works as intended");
	matrix B = new matrix(5, 5, mode:"random");
	matrix A = B*B.transpose();
	jacobi jac = new jacobi(A);
	jac.cyclic();

	/* The eigenvectors form a full orthonormal basis */
	matrix VTV = jac.V.transpose() * jac.V;
	matrix VVT = jac.V * jac.V.transpose();
	WriteLine("V^T * V = ");
	VTV.print();
	WriteLine("V * V^T = ");
	VVT.print();

	/* The columns of matrix V are eigenvectors of matrix A */
	WriteLine("Eigenvalues = ");
	jac.D.print();
	WriteLine("V^T * A * V = ");
	matrix M = jac.V.transpose() * A * jac.V;
	M.print();
	}
}

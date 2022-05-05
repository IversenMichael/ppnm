using static System.Console;
using static System.Math;
public static class main{
	public static void Main(){
		/* System parameters */
		int npoints = 50;
		double rmax = 30;
		double dr = rmax / (npoints + 1);

		/* Discretized space */
		vector r = new vector(npoints);
		for (int i=0; i<npoints; i++){
			r[i] = dr*(i+1);
		}

		/* Hamiltonian */
		matrix H = new matrix(npoints, npoints, mode:"zeros");
		/* Kinetic energy */
		for (int i=0; i<npoints-1; i++){
			H[i, i] = -2;
			H[i, i+1] = 1;
		     	H[i+1, i] = 1;
	        }
		H[npoints-1, npoints-1] = -2;
		H *= -0.5/(dr*dr);

		/* Potential energy */
		for(int i=0; i<npoints; i++){
			H[i, i] = -1/r[i];
		}
	
		/* Diagonalization */	
		jacobi jac = new jacobi(H);
		jac.cyclic();
		jac.V = jac.V * (1/Sqrt(dr));
		var out_numeric = new System.IO.StreamWriter("hydrogen_numeric.txt", append:true);
		for (int i=0; i<r.size; i++){
			out_numeric.WriteLine($"{r[i]} {Pow(jac.V[i, 0], 2)} {Pow(jac.V[i, 1], 2)} {Pow(jac.V[i, 2], 2)}");	
		}
		out_numeric.Close();

		int npoints_exact = 1000;
		double r_exact;
		var out_exact = new System.IO.StreamWriter("hydrogen_exact.txt", append:true);
		for (int i=0; i<npoints_exact; i++){
			r_exact = rmax * i/(npoints_exact+1);
			out_exact.WriteLine($"{r_exact} {Pow(H1s(r_exact), 2)} {Pow(H2s(r_exact), 2)} {Pow(H3s(r_exact), 2)}");
		}
		out_exact.Close();
	}
	
	public static double H1s(double r){
		double result = 2*r*Exp(-r);
		return result;
	}

	public static double H2s(double r){
		double result = 1/Sqrt(2) * r * (1 - r/2)  * Exp(-r/2);
		return result;
	}

	public static double H3s(double r){
		double result = 2/(3*Sqrt(3)) * r * (1 - 2*r/3 + 2*r*r/27) * Exp(-r/3);
		return result;
	}
	

}

// Part C
using static System.Math;
public partial class vec{
	// Approximation method for comparing two vectors
	public static bool approx(double a, double b, double tau=1e-9, double epsilon=1e-9){
		if(Abs(a - b) < tau){return true;}
		if(Abs(a - b)  /(Abs(a) + Abs(b)) < epsilon){return true;}
		return false;
	}
	public static bool approx(vec v, vec u){
		if(!approx(v.x, u.x)){return false;}
		if(!approx(v.y, u.y)){return false;}
	        if(!approx(v.z, u.z)){return false;}
	        return true;
	}	                                                                                                                                                                      public bool approx(vec other){return approx(this, other);} 
}

// PART A
using static System.Console;
using static System.Math;
public partial class vec{
	// Class variables
	public double x, y, z;

	// Constructors
	public vec(){x = y = z = 0;}	
	public vec(double x0, double y0, double z0){x = x0; y = y0; z = z0;}

	// Print methods
	public void print(string s){Write(s);WriteLine($"{x}, {y}, {z}");}
	public void print(){this.print("");}

	// Arithmetic
	public static vec operator*(double c, vec v){return new vec(c * v.x, c * v.y, c * v.z);}
	public static vec operator*(vec v, double c){return c * v;}
	public static vec operator+(vec v){return v;}
	public static vec operator-(vec v){return (-1) * v;}
	public static vec operator+(vec v, vec u){return new vec(v.x + u.x, v.y + u.y, v.z + u.z);}
	public static vec operator-(vec v, vec u){return new vec(v.x - u.x, v.y - u.y, v.z - u.z);}
}

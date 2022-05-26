// Part B
using static System.Math;
public partial class vec{
	// Dot product
	public static double dot(vec v, vec u){
		return v.x * u.x + v.y * u.y + v.z * u.z;
		}
	public double dot(vec other){
		return this.x * other.x + this.y * other.y + this.z + other.z;
		}

	// Cross product
	public static vec cross(vec v, vec u){
		return new vec(v.y * u.z - v.z * u.y, v.z * u.x - v.x * u.z, v.x * u.y - v.y * u.x);
		}
	public vec cross(vec other){
		return new vec(this.y * other.z - this.z * other.y, this.z * other.x - this.x * other.z, this.x * other.y - this.y * other.x);
		}

	// Norm
	public static double norm(vec v){
		return Sqrt(Pow(v.x, 2) + Pow(v.y, 2) + Pow(v.z, 2));
		}

	// Override ToString
        public override string ToString(){return $"vector = ({this.x}, {this.y}, {this.z})";}
}

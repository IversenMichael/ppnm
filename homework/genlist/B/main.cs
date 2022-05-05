using System;
using static System.Console;
public static class main{
	public static void Main(){

		/* Reading from a file using the new genlist class */
		WriteLine("Reading from a file using the new genlist class");
		genlist<double[]> list = new genlist<double[]>();
		char[] delimiters = {' ', '\t'};
		var options = StringSplitOptions.RemoveEmptyEntries;
		for(string line=ReadLine(); line!=null; line=ReadLine()){
			var words = line.Split(delimiters, options);
			int n = words.Length;
			double[] numbers = new double[n];
			for(int i=0; i<n; i++){
				numbers[i] = double.Parse(words[i]);
				}
			list.push(numbers);
		}
	
		for(int i=0; i<list.size; i++){
			var numbers = list.data[i];
			foreach(var number in numbers){
				Write($"{number:e} ");
			}
			WriteLine();
		}
	
		/* Removing third element */
		WriteLine();
		WriteLine("Removing third line and printing the result");
		list.rem(2);
		for(int i=0; i<list.size; i++){
			var numbers = list.data[i];
			foreach(var number in numbers){
				Write($"{number:e} ");
			}
			WriteLine();
		}

		/* Removing first element */
		WriteLine();
		WriteLine("Removing first line and printing the result");
		list.rem(0);
		for(int i=0; i<list.size; i++){
			var numbers = list.data[i];
			foreach(var number in numbers){
				Write($"{number:e} ");
			}
			WriteLine();
		}

	}
}

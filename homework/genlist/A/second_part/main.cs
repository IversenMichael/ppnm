using System;
using static System.Console;
public static class main{
	public static void Main(){

	genlist<double[]> list = new genlist<double[]>();
	char[] delimiters = {' ', '\t', '\n'};
	for(string line=ReadLine(); line!=null; line=ReadLine()){
		string[] x = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
		double[] numbers = new double[x.Length];
		for(int i=0; i<x.Length; i++){
			numbers[i] = double.Parse(x[i]);
			}
		list.push(numbers);
		}

	for(int i=0; i<list.size; i++){
		double[] numbers = list[i];
		for(int j=0; j<numbers.Length; j++){
			double number = numbers[j];
			Write($"{number:e} ");
			}
		WriteLine();
		}

	}
}

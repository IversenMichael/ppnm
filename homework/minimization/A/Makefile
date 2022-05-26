all: output.txt
	cat output.txt

output.txt: main.exe
	mono main.exe 1> output.txt

main.exe: main.cs vector.dll matrix.dll
	mcs -r:vector.dll -r:matrix.dll main.cs

matrix.dll: matrix.cs vector.dll
	mcs -t:library -r:vector.dll matrix.cs

vector.dll: vector.cs
	mcs -t:library vector.cs

clean:
	rm *.exe *.dll *.txt
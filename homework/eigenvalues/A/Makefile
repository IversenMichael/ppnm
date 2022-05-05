all: out.txt
	cat out.txt

out.txt: main.exe
	mono main.exe > out.txt

main.exe: main.cs jacobi.dll matrix.dll vector.dll
	mcs -reference:jacobi.dll -reference:matrix.dll -reference:vector.dll main.cs

jacobi.dll: jacobi.cs matrix.dll vector.dll
	mcs -target:library -reference:matrix.dll -reference:vector.dll jacobi.cs

matrix.dll: matrix.cs vector.dll
	mcs -target:library -reference:vector.dll matrix.cs

vector.dll: vector.cs
	mcs -target:library vector.cs

clean:
	rm *.txt *.dll *.exe

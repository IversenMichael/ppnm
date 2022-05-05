all: out.txt
	cat out.txt

out.txt: main.exe
	mono main.exe > out.txt

main.exe: main.cs matrix.dll vector.dll QRGS.dll
	mcs -reference:matrix.dll -reference:vector.dll -reference:QRGS.dll main.cs

QRGS.dll: QRGS.cs matrix.dll vector.dll
	mcs -target:library -reference:matrix.dll -reference:vector.dll QRGS.cs

vector.dll: vector.cs
	mcs -target:library vector.cs

matrix.dll: matrix.cs vector.dll
	mcs -target:library -reference:vector.dll matrix.cs

clean:
	rm *.txt *.dll *.exe

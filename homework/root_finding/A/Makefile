all: output.txt
	cat output.txt

output.txt: main.exe
	mono main.exe > output.txt

main.exe: main.cs vector.dll matrix.dll QRGS.dll
	mcs -r:vector.dll -r:matrix.dll -r:QRGS.dll main.cs

QRGS.dll: QRGS.cs matrix.dll vector.dll
	mcs -t:library -r:matrix.dll -r:vector.dll QRGS.cs

matrix.dll: matrix.cs vector.dll
	mcs -t:library -r:vector.dll matrix.cs

vector.dll: vector.cs
	mcs -target:library vector.cs

clean:
	rm *.exe *.txt *.dll
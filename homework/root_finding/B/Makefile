all: output.txt
	cat output.txt

output.txt: main.exe
	mono main.exe > output.txt

main.exe: main.cs vector.dll matrix.dll QRGS.dll genlist.dll runge_kutta_12.dll
	mcs -r:vector.dll -r:matrix.dll -r:QRGS.dll -r:genlist.dll -r:runge_kutta_12 main.cs

runge_kutta_12.dll: runge_kutta_12.cs vector.dll genlist.dll
	mcs -t:library -r:vector.dll -r:genlist.dll runge_kutta_12.cs

genlist.dll: genlist.cs
	mcs -t:library genlist.cs

QRGS.dll: QRGS.cs matrix.dll vector.dll
	mcs -t:library -r:matrix.dll -r:vector.dll QRGS.cs

matrix.dll: matrix.cs vector.dll
	mcs -t:library -r:vector.dll matrix.cs

vector.dll: vector.cs
	mcs -target:library vector.cs

clean:
	rm *.txt *.dll
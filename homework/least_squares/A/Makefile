all:	output.txt fit.png
	cat output.txt

fit.png: fit.txt Rutherford_Soddy.txt Makefile
	echo '\
	set terminal png;\
	set output "fit.png";\
	set grid;\
	set xlabel "$$t$$ [days]";\
	set ylabel "Per-cent activity";\
	set title "Original data of Rutherford and Sodder";\
	plot "fit.txt" with lines color black title "Exponential fit",\
	"Rutherford_Soddy.txt" with yerrorbars color red title "Data with errorbars"\
	' | pyxplot

output.txt fit.txt Rutherford_Soddy.txt: main.exe
	mono main.exe 1> output.txt

main.exe: main.cs LS.dll vector.dll
	mcs -reference:LS.dll -reference:vector.dll -reference:matrix.dll main.cs

LS.dll: LS.cs matrix.dll vector.dll QRGS.dll
	mcs -target:library -reference:QRGS.dll -reference:matrix.dll -reference:vector.dll LS.cs

QRGS.dll: QRGS.cs matrix.dll vector.dll
	mcs -reference:matrix.dll -reference:vector.dll -target:library QRGS.cs

matrix.dll: matrix.cs vector.dll
	mcs -reference:vector.dll -target:library matrix.cs

vector.dll: vector.cs
	mcs -target:library vector.cs

clean:
	rm *.txt *.dll *.png

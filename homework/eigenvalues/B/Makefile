hydrogen_radial.png: hydrogen_numeric.txt hydrogen_exact.txt Makefile
	echo '\
	set terminal png;\
	set output "hydrogen_radial.png";\
	set xlabel "$$r$$";\
	set ylabel "$$|rR(r)|^2$$";\
	set title "Numeric versus exact solution of hydrogen atom";\
	set grid;\
	set key outside;\
	plot "hydrogen_exact.txt" using 1:2 with lines color blue title "$$R_{1\mathrm s}^\mathrm{exact}(r)$$",\
	"hydrogen_exact.txt" using 1:3 with lines color red title "$$R_{2\mathrm s}^\mathrm{exact}(r)$$",\
	"hydrogen_exact.txt" using 1:4 with lines color green title "$$R_{3\mathrm s}^\mathrm{exact}(r)$$",\
	"hydrogen_numeric.txt" using 1:2 with points color blue pointtype 3 title "$$R_{1\mathrm s}^\mathrm{numeric}(r)$$",\
	"hydrogen_numeric.txt" using 1:3 with points color red pointtype 3 title "$$R_{2\mathrm s}^\mathrm{numeric}(r)$$",\
	"hydrogen_numeric.txt" using 1:4 with points color green pointtype 3 title "$$R_{3 \mathrm s}^\mathrm{numeric}(r)$$"\
	' | pyxplot

hydrogen_numeric.txt hydrogen_exact.txt: main.exe
	mono main.exe

main.exe: main.cs jacobi.dll matrix.dll vector.dll
	mcs -reference:jacobi.dll -reference:matrix.dll -reference:vector.dll main.cs

jacobi.dll: jacobi.cs matrix.dll vector.dll
	mcs -target:library -reference:matrix.dll -reference:vector.dll jacobi.cs

matrix.dll: matrix.cs vector.dll
	mcs -target:library -reference:vector.dll matrix.cs

vector.dll: vector.cs
	mcs -target:library vector.cs

clean:
	rm *.txt *.dll *.exe *.png

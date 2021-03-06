all: test_results.txt rank1_performance1.png rank1_performance2.png jacobi_performance1.png jacobi_performance2.png
	cat test_results.txt

rank1_performance1.png: rank1_times.txt Makefile
	echo '\
	set terminal png;\
	set output "$@";\
	set xlabel "Number of rows and columns of matrix";\
	set ylabel "time [s]";\
	set tics out;\
	set title "Time of diagonalization using rank-update-algorithm (we expect $$O(n^2)$$)";\
	set key left top;\
	set grid;\
	plot "rank1_times.txt" using 1:2 with points title "Data"\
	'| pyxplot

rank1_performance2.png: rank1_times.txt Makefile
	echo '\
	set terminal png;\
	set output "$@";\
	set xlabel "Number of rows and columns of matrix";\
	set ylabel "$$\mathrm{time}/\mathrm{size}^2$$ [s]";\
	set tics out;\
	set title "$$\mathrm{Time}/\mathrm{size}^2$$ using rank-update-algorithm (we expect $$O(1)$$)";\
	set key left top;\
	set grid;\
	plot [0:80] [0:0.0000005] "rank1_times.txt" using 1:3 with points title "Data"\
	'| pyxplot

jacobi_performance1.png: jac_times.txt Makefile
	echo '\
	set terminal png;\
	set output "$@";\
	set xlabel "Number of rows and columns of matrix";\
	set ylabel "time [s]";\
	set tics out;\
	set title "Time of diagonalization using Jacobi-algorithm (we expect $$O(n^3)$$)";\
	set key left top;\
	set grid;\
	plot "jac_times.txt" using 1:2 with points title "Data"\
	'| pyxplot

jacobi_performance2.png: jac_times.txt Makefile
	echo '\
	set terminal png;\
	set output "$@";\
	set xlabel "Number of rows and columns of matrix";\
	set ylabel "$$\mathrm{time}/\mathrm{size}^2$$ [s]";\
	set tics out;\
	set title "$$\mathrm{Time}/\mathrm{size}^2$$ using Jacobi algorithm (we expect $$O(n)$$)";\
	set key left top;\
	set grid;\
	plot "jac_times.txt" using 1:3 with points title "Data"\
	'| pyxplot

output.txt test_results.txt rank1_times.txt jac_times.txt: main.exe
	mono main.exe

main.exe: main.cs matrix.dll vector.dll jacobi.dll
	mcs -reference:matrix.dll -reference:vector.dll -reference:jacobi.dll main.cs

jacobi.dll: jacobi.cs matrix.dll
	mcs -target:library -reference:matrix.dll jacobi.cs

matrix.dll: matrix.cs vector.dll
	mcs -target:library -reference:vector.dll matrix.cs

vector.dll: vector.cs
	mcs -target:library vector.cs

clean:
	rm output.txt jac_times.txt rank1_times.txt test_results.txt
	rm *.exe *.dll *.png
plot.png: train_data.txt fit_data.txt correct_data.txt Makefile
	echo '\
	set terminal png ;\
	set output "$@" ;\
	set xlabel "$$x$$" ;\
	set ylabel "$$y$$" ;\
	set tics out;\
	set title "Interpolation of $$\\cos(5 x-1) \\exp(-x^2)$$" ;\
	set key ;\
	plot [-1:1] [-1:2] "train_data.txt" with points title "Training data", \
	"fit_data.txt" with lines title "Interpolation by ANN", \
	"correct_data.txt" with lines linetype 2 title "Exact function" \
	'| pyxplot

	echo ''
	cat output.txt

output.txt train_data.txt fit_data.txt correct_data.txt: main.exe
	mono main.exe 1> output.txt

main.exe: main.cs ann.dll
	mcs -r:ann.dll -r:vector.dll main.cs

ann.dll: ann.cs quasi_newton.dll vector.dll
	mcs -t:library -r:quasi_newton.dll -r:vector.dll ann.cs

quasi_newton.dll: quasi_newton.cs matrix.dll vector.dll
	mcs -t:library -r:vector.dll -r:matrix.dll quasi_newton.cs

matrix.dll: matrix.cs vector.dll
	mcs -t:library -r:vector.dll matrix.cs

vector.dll: vector.cs
	mcs -t:library vector.cs

clean: 
	rm *.png *.txt *.exe *.dll
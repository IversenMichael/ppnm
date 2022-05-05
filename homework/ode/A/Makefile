pendulum.png: out.txt Makefile
	echo '\
	set terminal png;\
	set out "pendulum.png";\
	set grid;\
	set xlabel "$$t$$";\
	plot "out.txt" using 1:2 with lines color blue title "$$\\theta(t)$$",\
	"out.txt" using 1:3 with lines color green title "$$\omega(t)$$"\
	' | pyxplot

out.txt: main.exe
	mono main.exe

main.exe: main.cs runge_kutta_12.dll vector.dll
	mcs -reference:runge_kutta_12.dll -reference:vector.dll main.cs

runge_kutta_12.dll: runge_kutta_12.cs vector.dll
	mcs -target:library -reference:vector.dll runge_kutta_12.cs

vector.dll: vector.cs
	mcs -target:library vector.cs

clean:
	rm *.txt *.dll *.exe *.png

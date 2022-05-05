all: bessel.data bessel.pyxplot.png

bessel.pyxplot.png: bessel.data Makefile
	echo 'set terminal png;\
       	set output "$@";\
	set xlabel "$$x$$";\
	set ylabel "$$y$$";\
	set title "The first four bessel functions";\
	set key right top;\
	set tics out;\
	set grid;\
	plot "$<" using 1:2 with lines title "$$J_0(x)$$",\
       	"$<" using 1:3 with lines title "$$J_1(x)$$",\
       	"$<" using 1:4 with lines title "$$J_2(x)$$",\
       	"$<" using 1:5 with lines title "$$J_3(x)$$";\
	' | pyxplot

bessel.data out.txt: main.exe
	mono main.exe 1> bessel.data

main.exe: main.cs integrate.dll
	mcs -reference:integrate.dll main.cs

integrate.dll: integrate.cs
	mcs -target:library integrate.cs

clean:
	rm *.exe *.txt *.png

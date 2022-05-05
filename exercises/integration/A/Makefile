all: out.txt erf.data erf.pyxplot.png
	cat out.txt
	cat erf.data

erf.pyxplot.png: erf.data Makefile
	echo 'set terminal png;\
       	set output "$@";\
	set xlabel "$$x$$";\
	set ylabel "$$y$$";\
	set title "Error function";\
	set key left top;\
	set tics out;\
	set grid;\
	plot "$<" index 0 with lines title "erf($$x$$)"\
	' | pyxplot

erf.data out.txt: main.exe
	mono main.exe 1> out.txt 2> erf.data

main.exe: main.cs integrate.dll
	mcs -reference:integrate.dll main.cs

integrate.dll: integrate.cs
	mcs -target:library integrate.cs

clean:
	rm *.exe *.txt *.png

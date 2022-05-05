all: ex.datapoints ex.png exp.pdf

ex.datapoints: Makefile
	echo "-2 0.1353\n-1 0.3679 \n0 1\n1 2.7183\n2 7.389\n" | tee ex.datapoints > ex.datapoints

ex.png: ex.data ex.datapoints Makefile 
	echo '\
	set terminal png; \
	set output "$@"; \
	set xlabel "$$x$$"; \
	set ylabel "$$y$$"; \
	set tics out; \
	set title "Exponential function"; \
	plot "$<" with lines title "$$\\exp(x)$$", "ex.datapoints" with points title"Tabulated values"\
	' | pyxplot

ex.data: main.exe
	mono main.exe > ex.data

main.exe: main.cs
	mcs main.cs

exp.pdf: exp.tex
	pdflatex exp.tex	
	pdflatex exp.tex	

clean:
	rm *.data *.pdf *.exe *.png

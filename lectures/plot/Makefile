all: erf.gnuplot.png erf.pyxplot.png erf.graph.png

erf.graph.png: erf.data.txt
	graph \
	-T png \
	$< > $@ \
	--x-label "x" \
	--y-label "y" \


erf.gnuplot.png: erf.pyxplot.png
	cat log.gpi | sed 's/pyxplot/gnuplot/' | tee log.gnu.gpi | gnuplot

erf.pyxplot.png: erf.data.txt
	echo "\
	set terminal png \n\
	set output '$@'\n\
	set xlabel 'x' \n\
	set ylabel 'y' \n\
	set title 'error function' \n\
       	set key left top \n\
	plot [-2:2] [-1:1] '$<' index 0 using 1:2 with lines title 'error function', 'erf.tab.txt' with points pointtype 2 title 'tab' \n\
	" | tee log.gpi | pyxplot

erf.data.txt: main.exe
	mono $< > $@

%.exe: %.cs
	mcs $<

test:
	echo \$x\$

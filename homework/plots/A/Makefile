erf.png: erf.plot erf.tab Makefile
	echo '\
	set terminal png; \
	set output "$@"; \
	set xlabel "$$x$$"; \
	set ylabel "$$y$$"; \
	set title "Error function"; \
	set key left top; \
	plot "erf.plot" with lines title "$$\mathrm{erf}(x)$$", "erf.tab" with points title "Table values"\
	' | pyxplot

erf.tab: Makefile
	echo '-5 -1\n-4 -1\n-3 -1\n-2 -0.995\n-1 -0.8427\n0 0\n1 0.8427\n2 0.995\n3 1\n4 1\n5 1' > $@

erf.plot: main.exe
	mono main.exe > erf.plot

main.exe: main.cs
	mcs main.cs


all: lngamma.png gamma.png

lngamma.png: lngamma.plot Makefile
	echo '\
	set terminal png; \
	set output "$@"; \
	set xlabel "$$x$$"; \
	set ylabel "$$y$$"; \
	set title "Log-gamma function"; \
	set key right bottom; \
	set xrange [-5:5]; \
	set yrange [-2:5]; \
	set grid; \
	plot "lngamma.plot" with lines linetype 1 title "$$\mathrm{Log}( \Gamma(x))$$" \
	' | pyxplot

gamma.png: gamma.plot gamma.tab Makefile
	echo '\
	set terminal png; \
	set output "$@"; \
	set xlabel "$$x$$"; \
	set ylabel "$$y$$"; \
	set title "Gamma function"; \
	set key right bottom; \
	set xrange [-5:5]; \
	set yrange [-5:5]; \
	set grid; \
	plot "gamma.plot" with lines linetype 1 title "$$\Gamma(x)$$", "gamma.tab" with points title "Table values"\
	' | pyxplot


gamma.tab: Makefile
	echo '1 1\n2 1\n3 2\n4 6\n5 24\n6 120' > $@

gamma.plot lngamma.plot: main.exe
	mono main.exe 1> gamma.plot 2> lngamma.plot

main.exe: main.cs
	mcs main.cs


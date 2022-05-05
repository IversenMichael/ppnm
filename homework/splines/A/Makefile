linter.png: out.linter.txt out.tab.txt Makefile
	echo '\
	set terminal png;\
	set output "$@"; \
	set grid;\
	set key outside;\
	set xlabel "$$x$$";\
	set ylabel "$$y$$";\
	set title "Linear interpolation of $$\mathrm{sin}(x)$$ and integral $$\int_0^x \mathrm{sin}(x) \mathrm dt$$";\
	plot [0:6.3] [-1.1:2.1] "out.linter.txt" using 1:4 with lines title "$$\mathrm{sin}(x)$$", \
	"out.linter.txt" using 1:5 with lines title "Exact integral $$\int_0^x \mathrm{sin}(t) \mathrm dt$$", \
	"out.linter.txt" using 1:2 with lines title "Linear interpolation", \
	"out.linter.txt" using 1:3 with lines title "Numeric integral $$\int_0^x \mathrm{sin}(t) \mathrm dt$$", \
	"out.tab.txt" using 1:2 with points title "Tabulated points" \
	' | pyxplot

out.tab.txt out.linter.txt: main.exe
	mono main.exe

main.exe: main.cs
	mcs main.cs

clean:
	rm *.txt *.exe *.png

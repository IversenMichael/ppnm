erf.png: data.txt Makefile
	echo '\
	set terminal png; \
	set output "$@"; \
	set xlabel "$$x$$"; \
	set ylabel "$$y$$"; \
	set title "Error function"; \
	set key left top; \
	plot "data.txt" with lines title "$$\mathrm{erf}(x)$$", "erf.tab" with points title "Table values"\
	' | pyxplot

output.txt data.txt: main.exe
	mono main.exe 1> output.txt 2> data.txt

main.exe: main.cs
	mcs main.cs

clean:
	rm *.exe *.txt
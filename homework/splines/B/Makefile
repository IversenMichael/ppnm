

spline.png: tab.txt spline.txt Makefile
	echo '\
	set terminal png;\
	set output "spline.png";\
	set title "Quadratic spline of $$\mathrm{sin}(2x) + \mathrm{cos}(x)$$";\
	set xlabel "$$x$$";\
	set ylabel "$$y$$";\
	set key outside;\
	plot [0:6.28] "spline.txt" using 1:2 with lines title "Spline",\
	"spline.txt" using 1:3 with lines title "Derivative",\
	"spline.txt" using 1:4 with lines title "Integral",\
	"tab.txt" using 1:2 with points title "Tabulated points"\
	' | pyxplot

tab.txt spline.txt spline_test.txt: main.exe
	mono $<

main.exe: main.cs
	mcs $<

clean:
	rm *.txt *.png

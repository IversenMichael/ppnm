odeint.png: odeint.data Makefile
	echo '\
	set output "$@";\
	set terminal png;\
	set xlabel "$$t$$";\
	set tics out;\
	set grid;\
	set title "Pendulum swinging under gravity and friction";\
	plot "odeint.data" using 1:2 with lines title "$$\\theta(t)$$", \
	"odeint.data" using 1:3 with lines title "$$\omega(t)$$"\
	' | pyxplot

odeint.data: main.exe
	mono main.exe > odeint.data

main.exe: main.cs odeint.dll
	mcs -reference:odeint.dll -reference:vector.dll main.cs

odeint.dll: odeint.cs vector.dll
	mcs -target:library -reference:vector.dll odeint.cs

vector.dll:
	mcs -target:library vector.cs

clean:
	rm *.exe *.dll *.png *.data

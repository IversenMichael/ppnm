all : output.txt
	cat $<

output.txt : main.exe
	mono $< > $@

main.exe : main.cs cmath.dll complex.dll
	mcs -reference:cmath.dll -reference:complex.dll main.cs

cmath.dll : cmath.cs complex.dll
		mcs -target:library -reference:complex.dll $<

complex.dll : complex.cs 
		mcs -target:library $<

clean:
	rm *.txt *.exe *.dll

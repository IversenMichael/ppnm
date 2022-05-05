all : Output.txt
	cat Output.txt

Output.txt : main.exe
	mono main.exe > Output.txt

main.exe : main.cs
	mcs -reference:cmath.dll main.cs

cmath.dll : cmath.cs complex.cs
		mcs -target:library -out:./cmath.dll $^

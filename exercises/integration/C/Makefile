all: out.txt
	cat out.txt


out.txt: main.exe
	mono main.exe > out.txt

main.exe: main.cs integrate.dll
	mcs -reference:integrate.dll main.cs

integrate.dll: integrate.cs
	mcs -target:library integrate.cs

clean:
	rm *.exe *.txt *.png

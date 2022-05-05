all: out.txt
	cat out.txt

out.txt: main.exe
	mono main.exe < input.txt > out.txt

main.exe: main.cs genlist.dll
	mcs -reference:genlist.dll main.cs

genlist.dll: genlist.cs
	mcs -target:library genlist.cs

clean:
	rm *.exe out.txt *.dll

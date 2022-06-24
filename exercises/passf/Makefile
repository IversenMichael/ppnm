all: output.txt
	cat output.txt

output.txt: main.exe
	mono $< 1> output.txt

main.exe: main.cs table.dll
	mcs -reference:table.dll main.cs

table.dll: table.cs
	mcs -target:library $<

clean:
	rm *.exe *.dll *.txt

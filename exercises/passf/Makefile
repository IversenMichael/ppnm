all: main.exe
	mono $<

main.exe: main.cs table.dll
	mcs -reference:table.dll main.cs

table.dll: table.cs
	mcs -target:library $<

clean:
	rm *.exe *.dll *.txt

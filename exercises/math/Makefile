all: Output.txt
	cat Output.txt

Output.txt: main.exe
	mono $< > $@

main.exe: main.cs
	mcs $<

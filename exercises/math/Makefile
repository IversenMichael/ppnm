all: output.txt
	cat $<

output.txt: main.exe
	mono $< > $@

main.exe: main.cs
	mcs $<

clean:
	rm *.exe *.txt
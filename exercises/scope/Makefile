all: Out.txt
	cat $<
Out.txt: main.exe
		mono $< > $@
main.exe: main.cs math.dll
	mcs -target:exe -reference:math.dll -out:$@ $<

math.dll: math.cs
	mcs -target:library -out:math.dll math.cs

out.txt: t2.exe
	mono $< > $@

t2.exe: t2.cs
	mcs -target:exe -out:$@ $<


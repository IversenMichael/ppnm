all: Out-stdin.txt outfile.txt out.cmdline.txt
	cat out.cmdline.txt

outfile.txt: fileio.exe
	mono $< < input.txt 1>$@

out.cmdline.txt: cmdline.exe Makefile
	mono $< cmdline.exe 1.23e-4	45	3e5 >$@

cmdline.exe: cmdline.cs 
	mcs -target:exe -out:$@ $<

cmdline.exe: cmdline.cs
	mcs -target:exe -out:$@ $<

Out-stdin.txt: stdin.exe input.txt
	mono $< < input.txt 1>$@ 2>log.txt

stdin.exe: stdin.cs
	mcs -target:exe -out:$@ $<

input.txt: Makefile
	echo "-1.234e-5 99.9e+5" > $@

.PHONEY:clean
clean:
	rm [Oo]ut* *.exe


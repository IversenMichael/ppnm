all: main.exe
	mono main.exe

matlib.dll: ../matlib/integrate/integrate.cs
	mcs -t:library -out:./%.dll integrate.cs

main.exe: main.cs
	mcs -target:exe -out:$@ $<


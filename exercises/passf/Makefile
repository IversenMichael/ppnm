all: main.exe
	mono main.exe

main.exe: main.cs table.dll
	mcs -reference:table.dll main.cs

table.dll: table.cs
	mcs -target:library table.cs

clean:
	rm main.exe table.dll

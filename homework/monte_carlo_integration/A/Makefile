all: output.txt
	cat output.txt

output.txt: main.exe
	mono main.exe > output.txt

main.exe: main.cs vector.dll
	mcs -r:vector.dll main.cs

vector.dll: vector.cs
	mcs -t:library vector.cs
all : Output.txt
	cat Output.txt

Output.txt : main.exe
	mono main.exe > Output.txt

main.exe : main.cs
	mcs main.cs

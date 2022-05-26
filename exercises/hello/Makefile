all: output.txt
	cat output.txt

output.txt : hello.exe
	mono hello.exe > output.txt

hello.exe : hello.cs
	mcs hello.cs

clean :
	rm *.txt *.exe

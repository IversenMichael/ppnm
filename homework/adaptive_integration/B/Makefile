all: output.txt output_python.txt
	cat output.txt
	cat output_python.txt

output_python.txt: integration.py
	python3 integration.py 1> output_python.txt

output.txt: main.exe
	mono main.exe 1> output.txt

main.exe: main.cs
	mcs main.cs

clean:
	rm *.exe *.txt
parts = part_A.cs part_B.cs part_C.cs

all: Out.txt
	cat Out.txt

Out.txt: main.exe
	mono main.exe > Out.txt

main.exe: main.cs vec.dll
	mcs -target:exe -out:main.exe -reference:vec.dll main.cs

vec.dll: $(parts)
	mcs -target:library -out:vec.dll $(parts)

clean : 
	rm Out.txt main.exe vec.dll

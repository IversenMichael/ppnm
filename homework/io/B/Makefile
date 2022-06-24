# We use the values 0, pi/4, pi/2, 3*pi/4 and pi
angles = 0 0.785 1.570 2.356 3.1415

all: out_B.txt
	cat out_B.txt

out_B.txt: part_B.exe
	mono part_B.exe $(angles) > out_B.txt

part_B.exe: part_B.cs
	mcs $< > $@

clean:
	rm part_*.exe out_*.txt

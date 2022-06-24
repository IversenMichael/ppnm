# We use the values 0, pi/4, pi/2, 3*pi/4 and pi
angles = 0 0.785 1.570 2.356 3.1415

all: out_A.txt
	cat out_A.txt

out_A.txt: part_A.exe
	# We use the values 0, pi/4, pi/2, 3*pi/4 and pi
	echo $(angles) | mono part_A.exe > out_A.txt

part_A.exe: part_A.cs
	mcs $< > $@

clean:
	rm part_*.exe out_*.txt

all: ms.pdf

ms.pdf: ms.tex
	pdflatex $<
	pdflatex $<
#	bibtex $<
#	pdflatex $<

out.gamma.txt: main.exe
	mono main.exe > out.gamma.txt

main.exe: main.cs sfuns.dll

%.exe: %.cs
	mcs -t:exe $(addprefix -r:,$(filter %.dll.$^)) $(filter %.cs.$^)

%.dll: %.cs
	mcs -t:library $(addprefix -r:,$(filter %.dll.$^)) $(filter %.cs.$^)


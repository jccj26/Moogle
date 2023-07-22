#!/usr/bin/env bash

if [ "$1" = "run" ]; then 
make dev

elif [ "$1" = "report" ]; then 
cd Informe
pdflatex document.tex

elif [ "$1" = "slides" ]; then 
cd Presentación
pdflatex Presentacion.tex

elif [ "$1" = "show_report" ]; then
cd Informe
pdflatex document.tex
echo "Introduzca visualizador"
read visu
  if [ "$visu" != "1" ]; then
   $visu document.pdf
  else
  xdg-open document.pdf
  fi

elif [ "$1" = "show_slides" ]; then
cd ..
cd Presentación
pdflatex Presentación.tex
echo "Introduzca visualizador"
read visu
  if [ "$visu" != "2" ]; then
   $visu Presentacion.pdf
  else
  xdg-open Presentación.pdf
  fi

elif [ "$1" = "clean" ]; then 
cd ..
cd Informe
  if [ -f document.log ]; then
  rm document.aux
  rm document.log
  rm document.pdf
  fi
cd ..
cd Presentación
  if [ -f Presentacion.log ]; then
  rm Presentacion.aux
  rm Presentacion.log
  rm Presentacion.nav
  rm Presentacion.out
  rm Presentacion.pdf
  rm Presentacion.snm
  rm Presentacion.toc
  fi
fi
En esta carpeta podrá encontrar todos los códigos necesarios
para la elaboración de dicho juego (Sokoban).
Para ello dicho juego se va elaborando por distitnos entregables, por lo que, podremos realizar ciertas pruebas.
Para realizar dichas pruebas, adjunto algun ejemplo  sobre entrada desde línea de comandos
para comprobar como funciona el control de errores y su funcionamiento correcto de sokoban.
----------------------------------------------------------------------------------------------------------------------------------------------------
TEST 1 *
--------
Mapa Perfecto: python3 Sokoban.py T1 -l '###########\n####  .#  #\n#### # $. #\n####  $#  #\n#@$.   ## #\n#   ###   #\n#   ###   #\n###########' 
Mapa sin jugador: python3 sokoban.py T1 -l '###########\n####  .#  #\n#### # $. #\n####  $#  #\n# $.   ## #\n#   ###   #\n#   ###   #\n###########'
Mapa sin cajas: python3 sokoban.py T1 -l '###########\n####  .#  #\n#### # .  #\n####   #  #\n#@ .   ## #\n#   ###   #\n#   ###   #\n###########'
Mapa sin objetivos: python3 sokoban.py T1 -l '###########\n####     #\n#### # $  #\n####  $#  #\n#@    ## #\n#   ###   #\n#   ###   #\n###########'

----------------------------------------------------------------------------------------------------------------------------------------------------
TEST 2 *
--------
Prueba T2T
    python3 src/sokoban.py T2S  -l '#####\n#.$@#\n#$. #\n#####'

Prueba T2S  
    python3  src/sokoban.py T2T -l '#####\n#*@ #\n# * #\n#####'

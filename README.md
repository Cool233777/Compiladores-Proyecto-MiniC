# Compiladores-Proyecto-MiniC


PRIMERA FASE ANALIZADOR LÉXICO
El funcionamiento del siguiente proyecto es bastante sencillo de compilar, cuando se da iniciar aparece un form con dos botones. Uno que dice cargar archivo se hace click y se elige el archivo que se quiere cargar.
Luego aparece en ruta el archivo cargado y después de eso se da en el botón de "Analizador léxico" y el archivo .out se debe de buscar en la carpeta del proyecto llamada Bin, luego se accede a la carpeta Debug y en esa carpeta se encuentra el archivo .out
en donde se puede encontrar el archivo resultante del análisis desplegando token por token (esto es solo si se descarga el proyecto completo).
Si cuenta solo con el ejecutable, el archivo .out se generará en el escritorio.

Se debe tener en cuenta que para cada vez que se quiere analizar el archivo se debe dejar de compilar, y volverlo a correr. No es que no funcione, pero no lo muestra estéticamente bien. Por razones de comprensión
es mejor seguir la instrucción anterior.


Las herramientas utilizadas para la programación de este proyecto fue REGEX. El proyecto funciona bien, lo único en lo que nos dio error fue en EOF, ya que creaba un conflicto con la ER de operadores. (En el archivo resultante los tokens para comentarios sí aparecen).


SEGUNDA FASE ANALIZADOR SINTÁCTICO
Para esta fase se hizo uso de la primera fase que lee los lexemas, por medio de la gramática dada. La gramática se tuvo que trabajar a papel, ampliarla, reducir sus conflictos. Se uso el algoritmo de analizador sintáctico ascendente SLR, se realizó la colección canónica,
tabla de análisis y su tabla de parser. Luego se procedió a hacer un archivo de texto en donde se escribe la tabla de análisis para luego leerla con el programa. Los conflictos se resolvieron siempre empezando por el primer camino, que en todos los casos es un desplazamiento
si tiene error se toma el camino de reducción y si este tamibién tiene error es porque la entrada es incorrecta. Luego se tiene que recuperar y seguir compilando y se recupera de manera que vacía la pila números y vaciando la pila de los símbolos que se han leído. Posteriormente, 
sigue leyendo normal. Cuando termina de compilar si encuentra un error muestra en un archivo llamado "Archivo de salida.out" en donde muestra la línea y el token que dio error, caso contrario muestra que la entrada está sintácticamente correcta.


Se adjunta la tabla de análisis para el funcionamiento del proyecto:




n;s19;s11;s15;s16;s17;s18;n;n;n;s10;n;s12;n;n;n;s13;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;1;2;3;8;5;n;9;14;n;4;n;n;6;n;n;n;n;n;7;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;acc;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;s19;s11;s15;s16;s17;s18;n;n;n;s10;n;s12;n;n;n;s13;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;r2;n;20;2;3;8;5;n;9;14;n;4;n;n;6;n;n;n;n;n;7;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;r3;r3;r3;r3;r3;r3;n;n;n;r3;n;r3;n;n;n;r3;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;r3;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;r4;r4;r4;r4;r4;r4;n;n;n;r4;n;r4;n;n;n;r4;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;r4;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;r5;r5;r5;r5;r5;r5;n;n;n;r5;n;r5;n;n;n;r5;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;r5;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;r6;r6;r6;r6;r6;r6;n;n;n;r6;n;r6;n;n;n;r6;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;r6;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;r7;r7;r7;r7;r7;r7;n;n;n;r7;n;r7;n;n;n;r7;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;r7;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
s21;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;s22;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;s23;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;n;n;s25;s26;s27;s28;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;24;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;s29;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;s30;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;r22;n;n;n;n;n;s32;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;31;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;r16;n;n;n;n;n;r16;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;r17;n;n;n;n;n;r17;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;r18;n;n;n;n;n;r18;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;r19;n;n;n;n;n;r19;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;r20;n;n;n;n;n;r20;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;r1;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
r8;r8;r8;r8;r8;r8;r8;n;r8;n;r8;n;r8;r8;r8;n;r8;r8;r8;r8;r8;r8;r8;r8;n;n;n;n;n;n;n;n;n;n;r8;r8;r8;r8;r8;r8;r8;r8;r8;r8;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
r9;n;n;n;n;n;n;n;s33;r9;n;r9;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;n;n;n;n;n;n;n;s34;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;s35;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;r11;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;r12;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;r13;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;r14;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;n;n;n;n;n;n;n;n;n;n;n;n;r30;n;s37;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;36;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;n;n;n;n;n;n;n;n;n;n;n;n;s38;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;r15;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;r21;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;s19;n;s15;s16;s17;s18;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;40;n;n;41;14;n;n;39;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;s19;n;s15;s16;s17;s18;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;40;n;n;41;14;n;n;42;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
s43;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;n;n;n;n;n;n;n;n;n;n;n;n;s44;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;s45;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;s19;n;s15;s16;s17;s18;n;n;n;s49;n;n;n;r42;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;48;14;n;n;n;n;n;n;n;n;n;n;n;46;47;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;n;n;n;n;n;n;n;n;s50;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;n;n;n;n;n;n;n;n;r27;n;s52;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;51;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;s53;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;n;n;n;n;n;n;n;n;s54;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
r10;r10;r10;r10;r10;r10;r10;n;r10;n;r10;n;r10;r10;r10;n;r10;r10;r10;r10;r10;r10;r10;r10;n;n;n;n;n;n;n;n;n;n;r10;r10;r10;r10;r10;r10;r10;r10;r10;r10;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;s19;s11;s15;s16;s17;s18;n;n;n;s10;n;n;n;r36;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;57;8;59;n;9;14;n;58;n;n;n;n;n;n;55;56;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;n;n;n;n;n;n;n;n;n;n;s62;n;r32;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;60;61;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;n;n;n;n;n;n;n;n;n;n;n;n;n;s63;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;s19;n;s15;s16;s17;s18;n;n;n;s49;n;n;n;r42;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;48;14;n;n;n;n;n;n;n;n;n;n;n;64;47;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;s65;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;s66;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;n;n;n;n;n;n;n;n;n;n;n;n;s68;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;67;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;n;n;n;n;n;n;n;n;r25;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;s19;n;s15;s16;s17;s18;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;40;n;n;41;14;n;n;69;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
r9;n;n;n;n;n;n;n;n;r9;n;r9;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;n;n;n;n;n;n;n;n;n;n;n;n;s68;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;70;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;n;n;n;n;n;n;n;n;n;n;n;n;n;s71;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;s19;s11;s15;s16;s17;s18;n;n;n;s10;n;n;n;r36;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;57;8;59;n;9;14;n;58;n;n;n;n;n;n;72;56;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;r37;r37;r37;r37;r37;r37;n;n;n;r37;n;n;n;r37;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;r38;r38;r38;r38;r38;r38;n;n;n;r38;n;n;n;r38;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;r39;r39;r39;r39;r39;r39;n;n;n;r39;n;n;n;r39;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;n;n;n;n;n;n;n;n;n;n;n;n;r29;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;n;n;n;n;n;n;n;n;n;n;n;n;r31;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;s73;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;r40;r40;r40;r40;r40;r40;n;n;n;r40;n;r40;n;n;n;r40;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;r40;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;n;n;n;n;n;n;n;n;n;n;n;n;n;r41;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;n;n;n;n;n;n;n;s74;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;n;n;n;n;n;n;n;s75;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;r23;r23;r23;r23;r23;r23;n;n;n;r23;n;r23;n;r23;n;r23;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;r23;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
r47;s19/r47;r47;s15/r47;s16/r47;s17/r47;s18/r47;n;r47;n;r47;n;r47;r47;r47;n;r47;r47;r47;r47;r47;r47;r47;r47;n;n;n;n;n;n;n;n;n;n;r47;r47;r47;r47;r47;r47;r47;r47;r47;r47;n;n;n;77;8;n;n;41;14;n;n;n;n;n;n;n;n;n;n;n;n;n;n;76;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;n;n;n;n;n;n;n;n;r26;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;r24;r24;r24;r24;r24;r24;n;n;n;r24;n;r24;n;r24;n;r24;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;r24;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;r28;r28;r28;r28;r28;r28;n;n;n;r28;n;r28;n;n;n;r28;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;r28;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;n;n;n;n;n;n;n;n;n;n;n;n;n;r35;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;n;n;n;n;n;n;n;n;n;n;s62;n;r33;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;78;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;s19;n;s15;s16;s17;s18;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;40;n;n;41;14;n;n;79;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;s19;n;s15;s16;s17;s18;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;40;n;n;41;14;n;n;80;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
r49;r49;s11/r49;r49;r49;r49;r49;n;r49;n;r49;n;r49;r49;r49;n;r49;r49;r49;r49;r49;r49;r49;r49;n;n;n;n;n;n;n;n;n;n;r49;r49;r49;r49;r49;r49;r49;r49;r49;r49;n;n;n;n;n;82;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;81;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
r47;s19/r47;r47;s15/r47;s16/r47;s17/r47;s18/r47;n;r47;n;r47;n;r47;r47;r47;n;r47;r47;r47;r47;r47;r47;r47;r47;n;n;n;n;n;n;n;n;n;n;r47;r47;r47;r47;r47;r47;r47;r47;r47;r47;n;n;n;77;8;n;n;41;14;n;n;n;n;n;n;n;n;n;n;n;n;n;n;83;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;n;n;n;n;n;n;n;n;n;n;n;n;r34;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;n;n;n;n;n;n;n;n;s84;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;n;n;n;n;n;n;n;n;s85;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
r61;s103;n;n;n;n;n;n;s114;n;n;n;n;s68;r51;n;n;s96;n;s97;s99;s101;s100;s102;n;n;n;n;n;n;n;n;n;n;s110;s111;s115;s116;s117;s118;s119;s120;s121;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;95;n;n;86;87;90;88;n;89;91;93;92;94;n;98;104;105;106;107;108;109;112;113
r49;r49;s11/r49;r49;r49;r49;r49;n;r49;n;r49;n;r49;r49;r49;n;r49;r49;r49;r49;r49;r49;r49;r49;n;n;n;n;n;n;n;n;n;n;r49;r49;r49;r49;r49;r49;r49;r49;r49;r49;n;n;n;n;n;82;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;122;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
r46;r46;r46;r46;r46;r46;r46;n;r46;n;r46;n;r46;r46;r46;n;r46;r46;r46;r46;r46;r46;r46;r46;n;n;n;n;n;n;n;n;n;n;r46;r46;r46;r46;r46;r46;r46;r46;r46;r46;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
s123;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
s124;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;n;n;n;n;n;n;n;n;n;n;n;n;n;s125;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
r61;s103;n;n;n;n;n;n;s114;n;n;n;n;s68;r51;n;n;s96;n;s97;s99;s101;s100;s102;n;n;n;n;n;n;n;n;n;n;s110;s111;s115;s116;s117;s118;s119;s120;s121;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;95;n;n;126;87;90;88;n;89;91;93;92;94;n;98;104;105;106;107;108;109;112;113
r52;r52;n;n;n;n;n;n;r52;n;n;n;n;r52;r52;n;n;r52;r52;r52;r52;r52;r52;r52;n;n;n;n;n;n;n;n;n;n;r52;r52;r52;r52;r52;r52;r52;r52;r52;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
r53;r53;n;n;n;n;n;n;r53;n;n;n;n;r53;r53;n;n;r53;r53;r53;r53;r53;r53;r53;n;n;n;n;n;n;n;n;n;n;r53;r53;r53;r53;r53;r53;r53;r53;r53;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
s127;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
r55;r55;n;n;n;n;n;n;r55;n;n;n;n;r55;r55;n;n;r55;r55;r55;r55;r55;r55;r55;n;n;n;n;n;n;n;n;n;n;r55;r55;r55;r55;r55;r55;r55;r55;r55;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
r56;r56;n;n;n;n;n;n;r56;n;n;n;n;r56;r56;n;n;r56;r56;r56;r56;r56;r56;r56;n;n;n;n;n;n;n;n;n;n;r56;r56;r56;r56;r56;r56;r56;r56;r56;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
r57;r57;n;n;n;n;n;n;r57;n;n;n;n;r57;r57;n;n;r57;r57;r57;r57;r57;r57;r57;n;n;n;n;n;n;n;n;n;n;r57;r57;r57;r57;r57;r57;r57;r57;r57;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
r58;r58;n;n;n;n;n;n;r58;n;n;n;n;r58;r58;n;n;r58;r58;r58;r58;r58;r58;r58;n;n;n;n;n;n;n;n;n;n;r58;r58;r58;r58;r58;r58;r58;r58;r58;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
r59;r59;n;n;n;n;n;n;r59;n;n;n;n;r59;r59;n;n;r59;r59;r59;r59;r59;r59;r59;n;n;n;n;n;n;n;n;n;n;r59;r59;r59;r59;r59;r59;r59;r59;r59;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;n;n;n;n;n;n;n;s128;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;n;n;n;n;n;n;n;s129;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
r60;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;n;n;n;n;n;n;n;s130;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
s131;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;s103;n;n;n;n;n;n;s114;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;s110;s111;s115;s116;s117;s118;s119;s120;s121;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;132;104;105;106;107;108;109;112;113
n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;s133;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
r94;r94;n;n;n;n;n;n;r94;r94;n;r94;n;r94;r94;n;n;r94;r94;r94;r94;r94;r94;r94;r94;n;s134;r94;r94;r94;r94;r94;r94;r94;r94;r94;r94;r94;r94;r94;r94;r94;r94;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
r73;r73;n;n;n;n;n;n;r73;r73;n;r73;n;r73;r73;n;n;r73;r73;r73;r73;r73;r73;r73;n;n;n;s135;n;n;n;n;n;n;r73;r73;r73;r73;r73;r73;r73;r73;r73;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
r75;r75;n;n;n;n;n;n;r75;r75;n;r75;n;r75;r75;n;n;r75;r75;r75;r75;r75;r75;r75;n;n;n;r75;s136;n;n;n;n;n;r75;r75;r75;r75;r75;r75;r75;r75;r75;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
r77;r77;n;n;n;n;n;n;r77;r77;n;r77;n;r77;r77;n;n;r77;r77;r77;r77;r77;r77;r77;n;n;n;r77;r77;s137;s138;n;n;n;r77;r77;r77;r77;r77;r77;r77;r77;r77;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
r80;r80;n;n;n;n;n;n;r80;r80;n;r80;n;r80;r80;n;n;r80;r80;r80;r80;r80;r80;r80;n;n;n;r80;r80;r80;r80;s139;n;n;r80;r80;r80;r80;r80;r80;r80;r80;r80;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
r82;r82;n;n;n;n;n;n;r82;r82;n;r82;n;r82;r82;n;n;r82;r82;r82;r82;r82;r82;r82;n;n;n;r82;r82;r82;r82;r82;s140;s141;r82;r82;r82;r82;r82;r82;r82;r82;r82;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
r85;r85;n;n;n;n;n;n;r85;r85;n;r85;n;r85;r85;n;n;r85;r85;r85;r85;r85;r85;r85;n;n;n;r85;r85;r85;r85;r85;r85;r85;r85;r85;r85;r85;r85;r85;r85;r85;r85;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;s143;n;n;n;n;n;n;s114;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;s115;s116;s117;s118;s119;s120;s121;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;142;113
n;s143;n;n;n;n;n;n;s114;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;s115;s116;s117;s118;s119;s120;s121;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;144;113
r88;r88;n;n;n;n;n;n;r88;r88;n;r88;n;r88;r88;n;n;r88;r88;r88;r88;r88;r88;r88;s145;n;n;r88;r88;r88;r88;r88;r88;r88;r88;r88;r88;r88;r88;r88;r88;r88;r88;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
r91;r91;n;n;n;n;n;n;r91;r91;n;r91;n;r91;r91;n;n;r91;r91;r91;r91;r91;r91;r91;r91;n;n;r91;r91;r91;r91;r91;r91;r91;r91;r91;r91;r91;r91;r91;r91;r91;r91;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;s103;n;n;n;n;n;n;s114;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;s110;s111;s115;s116;s117;s118;s119;s120;s121;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;146;104;105;106;107;108;109;112;113
r93;r93;n;n;n;n;n;n;r93;r93;n;r93;n;r93;r93;n;n;r93;r93;r93;r93;r93;r93;r93;r93;n;n;r93;r93;r93;r93;r93;r93;r93;r93;r93;r93;r93;r93;r93;r93;r93;r93;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;n;n;n;n;n;n;n;s147;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
r96;r96;n;n;n;n;n;n;r96;r96;n;r96;n;r96;r96;n;n;r96;r96;r96;r96;r96;r96;r96;r96;n;n;r96;r96;r96;r96;r96;r96;r96;r96;r96;r96;r96;r96;r96;r96;r96;r96;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
r97;r97;n;n;n;n;n;n;r97;r97;n;r97;n;r97;r97;n;n;r97;r97;r97;r97;r97;r97;r97;r97;n;n;r97;r97;r97;r97;r97;r97;r97;r97;r97;r97;r97;r97;r97;r97;r97;r97;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
r98;r98;n;n;n;n;n;n;r98;r98;n;r98;n;r98;r98;n;n;r98;r98;r98;r98;r98;r98;r98;r98;n;n;r98;r98;r98;r98;r98;r98;r98;r98;r98;r98;r98;r98;r98;r98;r98;r98;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
r99;r99;n;n;n;n;n;n;r99;r99;n;r99;n;r99;r99;n;n;r99;r99;r99;r99;r99;r99;r99;r99;n;n;r99;r99;r99;r99;r99;r99;r99;r99;r99;r99;r99;r99;r99;r99;r99;r99;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
r100;r100;n;n;n;n;n;n;r100;r100;n;r100;n;r100;r100;n;n;r100;r100;r100;r100;r100;r100;r100;r100;n;n;r100;r100;r100;r100;r100;r100;r100;r100;r100;r100;r100;r100;r100;r100;r100;r100;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
r48;r48;r48;r48;r48;r48;r48;n;r48;n;r48;n;r48;r48;r48;n;r48;r48;r48;r48;r48;r48;r48;r48;n;n;n;n;n;n;n;n;n;n;r48;r48;r48;r48;r48;r48;r48;r48;r48;r48;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;r43;n;r43;r43;r43;r43;n;n;n;r43;n;n;n;r43;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;r44;n;r44;r44;r44;r44;n;n;n;r44;n;n;n;r44;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
r45;r45;r45;r45;r45;r45;r45;n;r45;n;r45;n;r45;r45;r45;n;r45;r45;r45;r45;r45;r45;r45;r45;n;n;n;n;n;n;n;n;n;n;r45;r45;r45;r45;r45;r45;r45;r45;r45;r45;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;n;n;n;n;n;n;n;n;n;n;n;n;n;r50;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
r54;r54;n;n;n;n;n;n;r54;n;n;n;n;r54;r54;n;n;r54;r54;r54;r54;r54;r54;r54;n;n;n;n;n;n;n;n;n;n;r54;r54;r54;r54;r54;r54;r54;r54;r54;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;s103;n;n;n;n;n;n;s114;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;s110;s111;s115;s116;s117;s118;s119;s120;s121;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;148;104;105;106;107;108;109;112;113
n;s103;n;n;n;n;n;n;s114;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;s110;s111;s115;s116;s117;s118;s119;s120;s121;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;149;104;105;106;107;108;109;112;113
n;s103;n;n;n;n;n;n;s114;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;s110;s111;s115;s116;s117;s118;s119;s120;s121;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;150;104;105;106;107;108;109;112;113
r68;r68;n;n;n;n;n;n;r68;n;n;n;n;r68;r68;n;n;r68;r68;r68;r68;r68;r68;r68;n;n;n;n;n;n;n;n;n;n;r68;r68;r68;r68;r68;r68;r68;r68;r68;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
s151;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;s152;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;s143;n;n;n;n;n;n;s114;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;s110;s111;s115;s116;s117;s118;s119;s120;s121;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;153;105;106;107;108;109;112;113
n;s143;n;n;n;n;n;n;s114;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;s110;s111;s115;s116;s117;s118;s119;s120;s121;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;154;106;107;108;109;112;113
n;s143;n;n;n;n;n;n;s114;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;s110;s111;s115;s116;s117;s118;s119;s120;s121;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;155;107;108;109;112;113
n;s143;n;n;n;n;n;n;s114;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;s110;s111;s115;s116;s117;s118;s119;s120;s121;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;156;108;109;112;113
n;s143;n;n;n;n;n;n;s114;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;s110;s111;s115;s116;s117;s118;s119;s120;s121;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;157;108;109;112;113
n;s143;n;n;n;n;n;n;s114;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;s110;s111;s115;s116;s117;s118;s119;s120;s121;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;158;109;112;113
n;s143;n;n;n;n;n;n;s114;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;s110;s111;s115;s116;s117;s118;s119;s120;s121;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;159;112;113
n;s143;n;n;n;n;n;n;s114;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;s110;s111;s115;s116;s117;s118;s119;s120;s121;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;160;112;113
r86;r86;n;n;n;n;n;n;r86;r86;n;r86;n;r86;r86;n;n;r86;r86;r86;r86;r86;r86;r86;s145;n;n;r86;r86;r86;r86;r86;r86;r86;r86;r86;r86;r86;r86;r86;r86;r86;r86;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
r94;r94;n;n;n;n;n;n;r94;r94;n;r94;n;r94;r94;n;n;r94;r94;r94;r94;r94;r94;r94;r94;n;n;r94;r94;r94;r94;r94;r94;r94;r94;r94;r94;r94;r94;r94;r94;r94;r94;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
r87;r87;n;n;n;n;n;n;r87;r87;n;r87;n;r87;r87;n;n;r87;r87;r87;r87;r87;r87;r87;s145;n;n;r87;r87;r87;r87;r87;r87;r87;r87;r87;r87;r87;r87;r87;r87;r87;r87;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;s161;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;n;n;n;n;n;n;n;n;s162;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;s163;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;n;n;n;n;n;n;n;n;s164;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;n;n;n;n;n;n;n;n;s165;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
s166;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
r67;r67;n;n;n;n;n;n;r67;n;n;n;n;r67;r67;n;n;r67;r67;r67;r67;r67;r67;r67;n;n;n;n;n;n;n;n;n;n;r67;r67;r67;r67;r67;r67;r67;r67;r67;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;n;n;n;n;n;n;n;s167;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
r72;r72;n;n;n;n;n;n;r72;r72;n;r72;n;r72;r72;n;n;r72;r72;r72;r72;r72;r72;r72;n;n;n;s135;n;n;n;n;n;n;r72;r72;r72;r72;r72;r72;r72;r72;r72;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
r74;r74;n;n;n;n;n;n;r74;r74;n;r74;n;r74;r74;n;n;r74;r74;r74;r74;r74;r74;r74;n;n;n;r74;s136;n;n;n;n;n;r74;r74;r74;r74;r74;r74;r74;r74;r74;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
r76;r76;n;n;n;n;n;n;r76;r76;n;r76;n;r76;r76;n;n;r76;r76;r76;r76;r76;r76;r76;n;n;n;r76;r76;s137;s138;n;n;n;r76;r76;r76;r76;r76;r76;r76;r76;r76;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
r78;r78;n;n;n;n;n;n;r78;r78;n;r78;n;r78;r78;n;n;r78;r78;r78;r78;r78;r78;r78;n;n;n;r78;r78;r78;r78;s139;n;n;r78;r78;r78;r78;r78;r78;r78;r78;r78;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
r79;r79;n;n;n;n;n;n;r79;r79;n;r79;n;r79;r79;n;n;r79;r79;r79;r79;r79;r79;r79;n;n;n;r79;r79;r79;r79;s139;n;n;r79;r79;r79;r79;r79;r79;r79;r79;r79;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
r81;r81;n;n;n;n;n;n;r81;r81;n;r81;n;r81;r81;n;n;r81;r81;r81;r81;r81;r81;r81;n;n;n;r81;r81;r81;r81;r81;s140;s141;r81;r81;r81;r81;r81;r81;r81;r81;r81;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
r83;r83;n;n;n;n;n;n;r83;r83;n;r83;n;r83;r83;n;n;r83;r83;r83;r83;r83;r83;r83;n;n;n;r83;r83;r83;r83;r83;r83;r83;r83;r83;r83;r83;r83;r83;r83;r83;r83;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
r84;r84;n;n;n;n;n;n;r84;r84;n;r84;n;r84;r84;n;n;r84;r84;r84;r84;r84;r84;r84;n;n;n;r84;r84;r84;r84;r84;r84;r84;r84;r84;r84;r84;r84;r84;r84;r84;r84;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
r90;r90;n;n;n;n;n;n;r90;r90;n;r90;n;r90;r90;n;n;r90;r90;r90;r90;r90;r90;r90;r90;n;s168;r90;r90;r90;r90;r90;r90;r90;r90;r90;r90;r90;r90;r90;r90;r90;r90;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
r92;r92;n;n;n;n;n;n;r92;r92;n;r92;n;r92;r92;n;n;r92;r92;r92;r92;r92;r92;r92;r92;n;n;r92;r92;r92;r92;r92;r92;r92;r92;r92;r92;r92;r92;r92;r92;r92;r92;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;n;n;n;n;n;n;n;n;s169;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
r61;s103;n;n;n;n;n;n;s114;n;n;n;n;s68;n;n;n;s96;n;s97;s99;s101;s100;s102;n;n;n;n;n;n;n;n;n;n;s110;s111;s115;s116;s117;s118;s119;s120;s121;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;95;n;n;n;170;90;88;n;89;91;93;92;94;n;98;104;105;106;107;108;109;112;113
r61;s103;n;n;n;n;n;n;s114;n;n;n;n;s68;n;n;n;s96;n;s97;s99;s101;s100;s102;n;n;n;n;n;n;n;n;n;n;s110;s111;s115;s116;s117;s118;s119;s120;s121;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;95;n;n;n;171;90;88;n;89;91;93;92;94;n;98;104;105;106;107;108;109;112;113
n;s103;n;n;n;n;n;n;s114;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;s110;s111;s115;s116;s117;s118;s119;s120;s121;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;172;104;105;106;107;108;109;112;113
n;s103;n;n;n;n;n;n;s114;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;s110;s111;s115;s116;s117;s118;s119;s120;s121;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;173;104;105;106;107;108;109;112;113
n;s143;n;n;n;n;n;n;s114;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;s115;s116;s117;s118;s119;s120;s121;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;174
r95;r95;n;n;n;n;n;n;r95;r95;n;r95;n;r95;r95;n;n;r95;r95;r95;r95;r95;r95;r95;r95;n;n;r95;r95;r95;r95;r95;r95;r95;r95;r95;r95;r95;r95;r95;r95;r95;r95;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
r64;r64;n;n;n;n;n;n;r64;n;n;n;n;r64;r64;n;n;r64;s176/r64;r64;r64;r64;r64;r64;n;n;n;n;n;n;n;n;n;n;r64;r64;r64;r64;r64;r64;r64;r64;r64;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;175;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
r65;r65;n;n;n;n;n;n;r65;n;n;n;n;r65;r65;n;n;r65;r65;r65;r65;r65;r65;r65;n;n;n;n;n;n;n;n;n;n;r65;r65;r65;r65;r65;r65;r65;r65;r65;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
s177;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;n;n;n;n;n;n;n;n;r71;n;s179;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;178;n;n;n;n;n;n;n;n;n
r89;r89;n;n;n;n;n;n;r89;r89;n;r89;n;r89;r89;n;n;r89;r89;r89;r89;r89;r89;r89;r89;n;n;r89;r89;r89;r89;r89;r89;r89;r89;r89;r89;r89;r89;r89;r89;r89;r89;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
r62;r62;n;n;n;n;n;n;r62;n;n;n;n;r62;r62;n;n;r62;r62;r62;r62;r62;r62;r62;n;n;n;n;n;n;n;n;n;n;r62;r62;r62;r62;r62;r62;r62;r62;r62;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
r61;s103;n;n;n;n;n;n;s114;n;n;n;n;s68;n;n;n;s96;n;s97;s99;s101;s100;s102;n;n;n;n;n;n;n;n;n;n;s110;s111;s115;s116;s117;s118;s119;s120;s121;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;95;n;n;n;180;90;88;n;89;91;93;92;94;n;98;104;105;106;107;108;109;112;113
n;s103;n;n;n;n;n;n;s114;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;s110;s111;s115;s116;s117;s118;s119;s120;s121;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;181;104;105;106;107;108;109;112;113
n;n;n;n;n;n;n;n;n;s182;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;s103;n;n;n;n;n;n;s114;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;s110;s111;s115;s116;s117;s118;s119;s120;s121;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;183;104;105;106;107;108;109;112;113
r63;r63;n;n;n;n;n;n;r63;n;n;n;n;r63;r63;n;n;r63;r63;r63;r63;r63;r63;r63;n;n;n;n;n;n;n;n;n;n;r63;r63;r63;r63;r63;r63;r63;r63;r63;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;n;n;n;n;n;n;n;n;s184;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
s185;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;n;n;n;n;n;n;n;n;r71;n;s179;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;186;n;n;n;n;n;n;n;n;n
r61;s103;n;n;n;n;n;n;s114;n;n;n;n;s68;n;n;n;s96;n;s97;s99;s101;s100;s102;n;n;n;n;n;n;n;n;n;n;s110;s111;s115;s116;s117;s118;s119;s120;s121;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;95;n;n;n;187;90;88;n;89;91;93;92;94;n;98;104;105;106;107;108;109;112;113
r69;r69;n;n;n;n;n;n;r69;n;n;n;n;r69;r69;n;n;r69;r69;r69;r69;r69;r69;r69;n;n;n;n;n;n;n;n;n;n;r69;r69;r69;r69;r69;r69;r69;r69;r69;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
n;n;n;n;n;n;n;n;n;r70;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n
r66;r66;n;n;n;n;n;n;r66;n;n;n;n;r66;r66;n;n;r66;r66;r66;r66;r66;r66;r66;n;n;n;n;n;n;n;n;n;n;r66;r66;r66;r66;r66;r66;r66;r66;r66;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n;n


TERMINA TABLA DE ANÁLISIS, de igual manera se adjunta la tabla de análisis "SLR.txt" en los archivos del proyecto.


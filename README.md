# Compiladores-Proyecto-MiniC
El funcionamiento del siguiente proyecto es bastante sencillo de compilar, cuando se da iniciar aparece un form con dos botones. Uno que dice cargar archivo se hace click y se elige el archivo que se quiere cargar.
Luego aparece en ruta el archivo cargado y después de eso se da en el botón de "Analizador léxico" y el archivo .out se debe de buscar en la carpeta del proyecto llamada Bin, luego se accede a la carpeta Debug y en esa carpeta se encuentra el archivo .out
en donde se puede encontrar el archivo resultante del análisis desplegando token por token (esto es solo si se descarga el proyecto completo).
Si cuenta solo con el ejecutable, el archivo .out se generará en el escritorio.

Se debe tener en cuenta que para cada vez que se quiere analizar el archivo se debe dejar de compilar, y volverlo a correr. No es que no funcione, pero no lo muestra estéticamente bien. Por razones de comprensión
es mejor seguir la instrucción anterior.


Las herramientas utilizadas para la programación de este proyecto fue REGEX. El proyecto funciona bien, lo único en lo que nos dio error fue en EOF, ya que creaba un conflicto con la ER de operadores. (En el archivo resultante los tokens para comentarios sí aparecen).
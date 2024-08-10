# Star Wars

**Star Wars** es un juego basado en la exitosa saga de peliculas en la que elegiremos un personaje que nos acompañara en 10 combates a muerte en los cuales se disputa la gloria y la mayor conexion con la fuerza.

## Menu Principal

Comezamos el juego mostrando un menu simple conciso que consta de tres opciones:

- 1.Jugar.
- 2.Historial de Ganadores.
- 3.Salir.

### Jugar

- Comenzamos el juego.

- Se agregan los personajes de manera aletoria en una lista y acto seguido en un archivo JSON

- Se ilustra al jugador con una lista de 10 personajes  seleccionables los cuales cada uno posee unos atributos distintos sean "Fuerza","Velocidad","Destreza","Armadura" y "Nivel".

- Posteriormente a la eleccion del jugador de el personaje que lo acompañara en su aventura a traves de la API "Swapi" "https://swapi.dev/api/people/?page=" se obtienen y muestran caracteristicas personales del mismo como "Altura","Color de Piel", "Fecha de Nacimiento"(utilizando el formato de fecha que se utliza en Star Wars el cual consiste en tomar la memorable Batalla de Yavin como punto de partida), "Genero".

- Se elige aletoriamente el oponente del jugador de una lista sin incluir el personaje del jugador y comienza el combate por turnos.

- En cada turno el jugador o el oponente atacan e infligen una cantidad de daño calculado a traves de matematica basica para intentar balancear el mismo, y se finaliza el combate cuando la salud de uno de los participantes disminuye al valor 0.

- Se verifica la salud de ambos participantes, si la salud de el contricante llega a 0 quiere decir que el jugador ha ganado la ronda y si la salud del jugador llega a 0 quiere decir que el contricante gano la ronda.

- Si el ganador es el jugador se le otorga una bendicion en la cual se le permite elegir una bonificacion a sus atributos, ahora si el ganador es el oponente se muestra un mensaje de derrota y el programa muestra el menu principal para la siguiente accion.

- Despues de haber obtenido la bonificacion se verifica la cantidad de oponentes restantes, si ya no quedan oponentes se proclama ganador al jugador y se guarda el nombre del personaje en un archivo JSON, si todavia quedan oponentes avanza a la siguiente ronda contra un nuevo oponente.

### Historial de Ganadores

- Verifica que haya un historial de ganadores y lo muestra, si no es asi muestra el mensaje "No hay historial de ganadores".


### Salir

- Simplemente termina el programa en la instancia que se encuentre
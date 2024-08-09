using GeneradorAleatorioPjs;
using TextoAnimacion;

namespace Gameplay
{
    class Gameplay
    {
        //Metodo general que ejecutara todo el combate utilizando funciones mas particulares para cada situacion, empezando el turno siempre por el jugador y despues su oponente
        public static void Batalla(Player pj1, Player pj2, List<Player> Listapjs)
        {
            Random rnd = new Random();
            bool banderaCombate = true;
            pj1.Salud = 100;//Salud inicial
            pj2.Salud = 100;

            while (banderaCombate)
            {
                int daño = CalcularDaño(pj1, pj2, rnd);
                pj2.Salud -= daño;
                Console.WriteLine($"{pj1.Nombre} ha atacado a {pj2.Nombre} y ha infligido {daño} de daño, la salud de {pj2.Nombre} es de {pj2.Salud}\n");
                Console.ReadLine();
                if (pj2.Salud <= 0)
                {
                    Texto.Animacion(15,$"El vencedor es {pj1.Nombre}\n");
                    Texto.Animacion(15,"Fuiste bendecido, obtendras una mejora por tu triunfo!!!\n");
                    Listapjs.Remove(pj2);
                    MejorarPj(pj1);
                    banderaCombate = false;
                    break;

                }
                daño = CalcularDaño(pj1, pj2, rnd);
                pj1.Salud -= daño;
                Console.WriteLine($"{pj2.Nombre} ha atacado a {pj1.Nombre} y ha infligido {daño} de daño, la salud de {pj1.Nombre} es de {pj1.Salud}\n");
                Console.ReadLine();

                if (pj1.Salud <= 0)
                {
                    Texto.Animacion(15,$"El vencedor es {pj2.Nombre}\n");
                    Texto.Animacion(15,"-HAZ SIDO DERROTADO LA FUERZA NO ESTUVO DE TU LADO-\n");
                    banderaCombate = false;
                }
            }
        }
        //Metodo para calcular el daño utilizando calculos matematicos
        public static int CalcularDaño(Player atacante, Player defensor, Random rnd)
        {
            int ataque = atacante.Destreza * atacante.Fuerza * atacante.Nivel;
            int efectividad = rnd.Next(40, 100);
            int defensa = defensor.Armadura * defensor.Velocidad;
            const int constanteAjuste = 500;
            int daño = ((ataque * efectividad) - defensa) / constanteAjuste;
            return daño;
        }
        //Metodo para mejorar el personaje elegido despues de cada ronda si es que sale victorioso
        public static void MejorarPj(Player pj)
        {
            Console.WriteLine("1- Mejorar Nivel +5 ");
            Console.WriteLine("2- Mejorar Armadura +5 ");
            Console.WriteLine("3- Mejorar Destreza +5 ");
            Console.WriteLine("4- Mejorar Velocidad +5 ");
            Console.WriteLine("5- Mejorar Fuerza +5 ");
            int opcionMejora;
            Console.WriteLine("Eleccion Mejora:");
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out opcionMejora) && opcionMejora >= 1 && opcionMejora <= 5)
                {
                    switch (opcionMejora)
                    {
                        case 1:
                            pj.Nivel += 5;
                            Console.WriteLine($"El nivel de {pj.Nombre} se ha aumentado + 5\n");
                            break;
                        case 2:
                            pj.Armadura += 5;
                            Console.WriteLine($"La armadura de {pj.Nombre} se ha aumentado + 5\n");
                            break;
                        case 3:
                            pj.Destreza += 5;
                            Console.WriteLine($"La destreza de {pj.Nombre} se ha aumentado + 5\n");
                            break;
                        case 4:
                            pj.Velocidad += 5;
                            Console.WriteLine($"La velocidad de {pj.Nombre} se ha aumentado + 5\n");
                            break;
                        case 5:
                            pj.Fuerza += 5;
                            Console.WriteLine($"La fuerza de {pj.Nombre} se ha aumentado + 5\n");
                            break;
                        default:
                            break;
                    }
                    break;
                }
                Console.WriteLine("Ingrese un valor valido entre 1-5");
            }
        }
    }
}
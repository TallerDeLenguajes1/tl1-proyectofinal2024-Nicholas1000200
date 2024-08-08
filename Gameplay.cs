using GeneradorAleatorioPjs;

namespace Gameplay
{
    class Gameplay
    {
        //Funcion general que ejecutara todo el combate utilizando funciones mas particulares para cada situacion, empezando el turno siempre por el jugador y despues su oponente
        public static void Batalla(Player pj1, Player pj2, List<Player> Listapjs)
        {
            bool banderaCombate = true;
            pj1.Salud = 100;//Salud inicial
            pj2.Salud = 100;

            while (banderaCombate)
            {
                int daño = CalcularDaño(pj1, pj2);
                pj2.Salud -= daño;
                Console.WriteLine($"{pj1.Nombre} ha atacado a {pj2.Nombre} y ha infligido {daño}, su salud es de {pj2.Salud}");
                if (pj2.Salud <= 0)
                {
                    Console.WriteLine($"El vencedor es {pj1.Nombre}");
                    Console.WriteLine("Fuiste bendecido, obtendras una mejora por tu triunfo!!!");
                    MejorarPj(pj1);
                    Listapjs.Remove(pj2);
                    banderaCombate = false;
                }
                daño = CalcularDaño(pj1, pj2);
                pj2.Salud -= daño;
                Console.WriteLine($"{pj2.Nombre} ha atacado a {pj1.Nombre} y ha infligido {daño}, su salud es de {pj1.Salud}");

                if (pj1.Salud <= 0)
                {
                    Console.WriteLine($"El vencedor es {pj2.Nombre}");
                    Console.WriteLine("-HAZ SIDO DERROTADO LA FUERZA NO ESTUVO DE TU LADO-");
                    banderaCombate = false;
                }
            }
        }
        public static int CalcularDaño(Player atacante, Player defensor)
        {
            Random rnd = new Random();
            int ataque = atacante.Destreza * atacante.Fuerza * atacante.Nivel;
            int efectividad = rnd.Next(1, 100);
            int defensa = defensor.Armadura * defensor.Velocidad;
            const int constanteAjuste = 500;
            int daño = ((ataque * efectividad) - defensa) / constanteAjuste;
            return daño;
        }
        public static void MejorarPj(Player pj)
        {
            Console.WriteLine("1- Mejorar Salud +10 ");
            Console.WriteLine("2- Mejorar Armadura +5 ");
            Console.WriteLine("3- Mejorar Destreza +5 ");
            Console.WriteLine("4- Mejorar Velocidad +5 ");
            Console.WriteLine("5- Mejorar Fuerza +5 ");
            Console.WriteLine("Si desea no mejorar presione cualquier otra tecla");
            int opcionMejora = int.Parse(Console.ReadLine());
            switch (opcionMejora)
            {
                case 1:
                    pj.Salud += 10;
                    Console.WriteLine($"La salud de {pj.Nombre} se ha aumentado + 10");
                    break;
                case 2:
                    pj.Armadura += 5;
                    Console.WriteLine($"La armadura de {pj.Nombre} se ha aumentado + 5");
                    break;
                case 3:
                    pj.Destreza += 5;
                    Console.WriteLine($"La destreza de {pj.Nombre} se ha aumentado + 5");
                    break;
                case 4:
                    pj.Velocidad += 5;
                    Console.WriteLine($"La velocidad de {pj.Nombre} se ha aumentado + 5");
                    break;
                case 5:
                    pj.Fuerza += 5;
                    Console.WriteLine($"La fuerza de {pj.Nombre} se ha aumentado + 5");
                    break;
                default:
                    Console.WriteLine("Ha decidido no mejorar su personaje, que la fuerza te acompañe");
                    break;
            }
        }
    }
}


namespace Juego_de_rol
{
    //Esta clase se utiliza para generar personajes con atributos con valores aleatorios
    class Player
    {
        //Caracteristicas de los personajes
        public int Salud { get; set; }
        public int Fuerza { get; set; }
        public int Destreza { get; set; }
        public int Defensa { get; set; }
        public int Nivel { get; set; }
        public int Armadura { get; set; }
        public int Velocidad { get; set; }

        //Datos de los personajes
        public string Tipo { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaNac { get; set; }
        public int Edad { get; set; }

        private static List<string> nombresUsados = new List<string>();
        //Metodo para asignar valores a cada atributo de los personajes
        public static Player PlayerGenerator()
        {
            Random rnd = new Random();
            Player pj = new Player();
            string[] nombres = { "Luke Skywalker", "Anakin Skywalker", "Han Solo", "Boba Fett", "Obi-Wan Kenobi", "C-3PO", "Jar Jar Binks", "Chewbacca", "General Grevious", "Mace Windu" };
            string[] tipo = { "Jedi Knight", "Jedi Master", "Padawan", "Droid" };
            pj.Tipo = tipo[rnd.Next(tipo.Length)];
            pj.Nombre = GenerarNombreUnico(rnd, nombres);
            pj.FechaNac = FechaNacGenerador(rnd);
            pj.Edad = CalcularEdad(pj.FechaNac);
            pj.Velocidad = rnd.Next(1, 11);
            pj.Destreza = rnd.Next(1, 6);
            pj.Fuerza = rnd.Next(1, 11);
            pj.Nivel = rnd.Next(1, 11);
            pj.Armadura = rnd.Next(1, 11);
            pj.Salud = 100;

            return pj;
        }
        private static DateTime FechaNacGenerador(Random rnd)
        {
            int anio = rnd.Next(DateTime.Now.Year - 60, DateTime.Now.Year - 18);
            int mes = rnd.Next(1, 13);
            int dia = rnd.Next(1, DateTime.DaysInMonth(anio, mes) + 1);

            return new DateTime(anio, mes, dia);
        }
        private static int CalcularEdad(DateTime fechaNac)
        {
            int edad = DateTime.Now.Year - fechaNac.Year;
            return edad;
        }
        private static string GenerarNombreUnico(Random rnd, string[] nombres)
        {
            string nombre;
            do
            {
                nombre = nombres[rnd.Next(nombres.Length)];
            } while (nombresUsados.Contains(nombre));

            nombresUsados.Add(nombre);
            return nombre;
        }
        public void Mostrar()
        {
            Console.WriteLine($"Nombre:{Nombre}");
            Console.WriteLine($"Tipo:{Tipo}");
            Console.WriteLine($"Edad:{Edad}");
            Console.WriteLine($"Fecha Nacimiento:{FechaNac}");
            Console.WriteLine();
            Console.WriteLine("----CARACTERISTICAS----");
            Console.WriteLine($"Salud:{Salud}");
            Console.WriteLine($"Nivel:{Nivel}");
            Console.WriteLine($"Velocidad:{Velocidad}");
            Console.WriteLine($"Fuerza:{Fuerza}");
            Console.WriteLine($"Destreza:{Destreza}");
            Console.WriteLine($"Armadura:{Armadura}");
            Console.WriteLine($"Salud:{Salud}");
        }
    }


}





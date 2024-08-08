using System.Security.Cryptography;

namespace TextoAnimacion
{
    public class Texto
    {
        public static void Menu()
        {
            Console.WriteLine("\nMENU PRINCIPAL:");
            Animacion(15, "1. Jugar\n");
            Animacion(15, "2. Historial de ganadores\n");
            Animacion(15, "3. Salir\n");
            Animacion(15, "\nSeleccion: ");
        }
        public static void Animacion(int vel, string texto)
        {

            for (int i = 0; i < texto.Length; i++)
            {
                Console.Write(texto[i]);
                Thread.Sleep(vel);
            }
        }
    }

}
